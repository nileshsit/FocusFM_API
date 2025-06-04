using System.IdentityModel.Tokens.Jwt;
using FocusFM.Common.CommonMethod;
using FocusFM.Common.EmailNotification;
using FocusFM.Common.Enum;
using FocusFM.Common.Helpers;
using FocusFM.Model.AdminUser;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Settings;
using FocusFM.Model.Token;
using FocusFM.Service.AdminUser;
using FocusFM.Service.JWTAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using static FocusFM.Common.EmailNotification.EmailNotification;

namespace FocusFMAPI.Controllers
{
    [Route("api/user")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly AppSettings _appSettings;
        private readonly IAdminUserService _userService;
        private readonly SMTPSettings _smtpSettings;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region Constructor
        public UserController
        (
            IAdminUserService UserService,
            IHttpContextAccessor httpContextAccessor,
            IJWTAuthenticationService jwtAuthenticationService,
            IOptions<SMTPSettings> smtpSettings,
            IOptions<AppSettings> appSettings,
            IConfiguration config,
            IWebHostEnvironment hostingEnvironment
        )
        {
            _userService = UserService;
            _appSettings = appSettings.Value;
            _smtpSettings = smtpSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _jwtAuthenticationService = jwtAuthenticationService;
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Save User
        /// </summary>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        [HttpPost("save")]
         public async Task<BaseApiResponse> SaveUser([FromForm] AdminUserRequestModel model)
        {
            IFormFile file = model.File;
            BaseApiResponse response = new BaseApiResponse();

            model.FirstName = model.FirstName.Trim();
            model.LastName = model.LastName.Trim();
            var password = "";
            var passSalt = "";
            var Generatepassword = "";
            if (model.UserId == 0)
            {
                Generatepassword = Utility.GeneratePassword();
                var EncryptedPass = Utility.GetEncryptPassword(Generatepassword);
                string[] segments = EncryptedPass.Split("||");
                password = segments[0];
                passSalt = segments[1];

                //if (file == null || file.Length == 0)
                //{
                //    response.Data = null;
                //    response.Success = false;
                //    response.Message = ErrorMessages.NoFile;
                //    return response;
                //}
            }
            if (!CommonMethods.IsValidEmail(model.EmailId))
            {
                response.Message = ErrorMessages.InvalidEmailId;
                response.Success = false;
                return response;
            }
            TokenModel tokenModel = new TokenModel();
            string jwtToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace(JwtBearerDefaults.AuthenticationScheme + " ", "");
            if (!string.IsNullOrEmpty(jwtToken))
            {
                tokenModel = _jwtAuthenticationService.GetUserTokenData(jwtToken);
            }
            string fileName = null;
            if (file != null)
            {
                string[] arrExtension = _config["FileConfiguration:AllowedProfileFileFormat"].Split(",");
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!arrExtension.Contains(extension))
                {
                    response.Success = false;
                    response.Message = ErrorMessages.InvalidImageFile;
                    return response;
                }

                long allowedSize = long.Parse(_config["FileConfiguration:AllowedProfileFileSize"]);

                if (file.Length>allowedSize)
                {
                    response.Success = false;
                    response.Message = ErrorMessages.FileSizeExceeds;
                    return response;
                }

                fileName = await CommonMethods.UploadDocument(file, Path.Combine(_hostingEnvironment.WebRootPath, "/" + _config["FileConfiguration:UserProfileFilePath"]));
            }
            var result = await _userService.SaveUser(model, tokenModel.UserId, password, passSalt,fileName);
            var issend = false;
            if (result == Status.Success)
            {
                if (model.UserId > 0)
                {
                    response.Message = ErrorMessages.UpdateUserSuccess;
                    response.Success = true;
                }
                else
                {
                    EmailNotification.EmailSetting setting = new EmailSetting
                    {
                        EmailEnableSsl = Convert.ToBoolean(_smtpSettings.EmailEnableSsl),
                        EmailHostName = _smtpSettings.EmailHostName,
                        EmailPassword = _smtpSettings.EmailPassword,
                        EmailAppPassword = _smtpSettings.EmailAppPassword,
                        EmailPort = Convert.ToInt32(_smtpSettings.EmailPort),
                        FromEmail = _smtpSettings.FromEmail,
                        FromName = _smtpSettings.FromName,
                        EmailUsername = _smtpSettings.EmailUsername,
                    };

                    string emailBody = string.Empty;
                    string adminEmailBody = string.Empty;
                    string adminEmail = _config["AppSettings:AdminEmail"];

                    string BasePath = Path.Combine(Directory.GetCurrentDirectory(), Constants.ExceptionReportPath);
                    var path = _httpContextAccessor.HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;

                    using (StreamReader reader = new StreamReader(Path.Combine(BasePath, Constants.UserPasswordEmail)))
                    {
                        emailBody = reader.ReadToEnd();
                    }

                    using (StreamReader reader = new StreamReader(Path.Combine(BasePath, Constants.UserRegisterEmailToAdmin)))
                    {
                        adminEmailBody = reader.ReadToEnd();
                    }

                    #region Send Mail to User
                    emailBody = emailBody.Replace("##userName##", model.FirstName);
                    emailBody = emailBody.Replace("##LogoURL##", path + "/" + _config["FileConfiguration:LogoPath"]);
                    emailBody = emailBody.Replace("##BrandName##", "Focus FM");
                    emailBody = emailBody.Replace("##Password##", Generatepassword);
                    emailBody = emailBody.Replace("##currentYear##", DateTime.Now.Year.ToString());
                    emailBody = emailBody.Replace("##PortalURL##", _config["AppSettings:AdminPortalUrl"]);
                    var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
                    logger.Info("Logo URL:" + path + "/" + _config["FileConfiguration:LogoPath"]);
                    logger.Info("Full Template:" + emailBody);
                    issend = await Task.Run(() => SendMailMessage(model.EmailId, null, null, "User Password", emailBody, setting, null));
                    #endregion

                    //#region Send Mail to Admin
                    //adminEmailBody = adminEmailBody.Replace("##userName##", model.FirstName + " " + model.LastName);
                    //adminEmailBody = adminEmailBody.Replace("##LogoURL##", path + "/" + _config["FileConfiguration:LogoPath"]);
                    //adminEmailBody = adminEmailBody.Replace("##BrandName##", "Focus FM");
                    //adminEmailBody = adminEmailBody.Replace("##Email##", model.EmailId);
                    //adminEmailBody = adminEmailBody.Replace("##RegisteredOn##", DateTime.Now.ToString("dd/MM/yyyy"));
                    //adminEmailBody = adminEmailBody.Replace("##currentYear##", DateTime.Now.Year.ToString());
                    //adminEmailBody = adminEmailBody.Replace("##PortalURL##", _config["AppSettings:AdminPortalUrl"]);
                    //var logger1 = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
                    //logger1.Info("Logo URL:" + path + "/" + _config["FileConfiguration:LogoPath"]);
                    //logger1.Info("Full Template:" + adminEmailBody);
                    //bool isAdminMailSent = await Task.Run(() => SendMailMessage(adminEmail, null, null, "New User Registration", adminEmailBody, setting, null));
                    //#endregion

                    response.Message = ErrorMessages.SaveUserSuccess;
                    response.Success = true;
                }
            }

            else if (result == Status.AlredyExists)
            {
                response.Message = ErrorMessages.EmailExists;
                response.Success = false;
            }
            else
            {
                response.Message = ErrorMessages.SomethingWentWrong;
                response.Success = false;
            }
            return response;
        }

        /// <summary>
        /// User List With Pagination
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("list")]
        public async Task<ApiResponse<AdminUserResponseModel>> GetUserListAdmin(CommonPaginationModel model)
        {
            ApiResponse<AdminUserResponseModel> response = new ApiResponse<AdminUserResponseModel>() { Data = new List<AdminUserResponseModel>() };
            var result = await _userService.GetUserListAdmin(model);
            foreach (var record in result)
            {
                // Example: Update file path if it exists
                if (record.Photo != null)
                {
                    string originalPath = record.Photo.ToString();

                    string path = _httpContextAccessor.HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;
                    // Example: Replace part of the path or add prefix

                    //record.Photo = originalPath.Replace(Directory.GetCurrentDirectory(), _config["AppSettings:APIURL"]);
                    record.Photo = Path.Combine(_config["AppSettings:APIURL"], _config["FileConfiguration:UserProfileFilePath"], record.Photo);
                    record.Photo = record.Photo.Replace("\\", "/");
                }
            }
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        /// <summary>
        /// Get User Details By ID
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ApiResponse<AdminUserResponseModel>> GetUserById(long id)
        {
            ApiResponse<AdminUserResponseModel> response = new ApiResponse<AdminUserResponseModel>() { Data = new List<AdminUserResponseModel>() };
            var result = await _userService.GetUserById(id);
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<BaseApiResponse> DeleteUser(long id)
        {
            BaseApiResponse response = new BaseApiResponse();
            long UserId = 0;
            if (Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                // Parse the JWT token
                var token = authHeader.ToString().Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var j = JsonConvert.DeserializeObject<Dictionary<string, string>>(jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value);

                UserId = long.TryParse(j["UserId"], out var val) ? val : 0;
            }
            var result = await _userService.DeleteUser(id,UserId);
            if (result == 0)
            {
                response.Message = ErrorMessages.DeleteUserSuccess;
                response.Success = true;
            }
            else
            {
                response.Message = ErrorMessages.SomethingWentWrong;
                response.Success = false;
            }
            return response;
        }

        /// <summary>
        /// Active / InActive User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("active-inactive/{id}")]
        public async Task<BaseApiResponse> InActiveUser(long id)
        {
            BaseApiResponse response = new BaseApiResponse();
            long UserId = 0;
            if (Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                // Parse the JWT token
                var token = authHeader.ToString().Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var j = JsonConvert.DeserializeObject<Dictionary<string, string>>(jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value);

                UserId = long.TryParse(j["UserId"], out var val) ? val : 0;
            }
            var result = await _userService.InActiveUser(id,UserId);
            if (result == ActiveStatus.Inactive)
            {
                response.Message = ErrorMessages.UserInactive;
                response.Success = true;
            }
            else if (result == ActiveStatus.Active)
            {
                response.Message = ErrorMessages.UserActive;
                response.Success = true;
            }
            else
            {
                response.Message = ErrorMessages.SomethingWentWrong;
                response.Success = false;
            }
            return response;
        }
        #endregion
    }
}
