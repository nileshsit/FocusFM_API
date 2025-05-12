
using FocusFM.Common.CommonMethod;
using FocusFM.Common.EmailNotification;
using FocusFM.Common.Helpers;
using FocusFM.Model.Login;
using FocusFM.Model.Settings;
using FocusFM.Model.Token;
using FocusFM.Service.Account;
using FocusFM.Service.JWTAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Web;
using static FocusFM.Common.EmailNotification.EmailNotification;

namespace FocusFMAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Field
        private IConfiguration _config;
        private IAccountService _accountService;
        private readonly AppSettings _appSettings;
        private readonly SMTPSettings _smtpSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Constructor
        public AccountController
        (
            IConfiguration config,
            IAccountService loginService,
            IOptions<AppSettings> appSettings,
            IOptions<SMTPSettings> smtpSettings,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IJWTAuthenticationService jwtAuthenticationService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment
        )
        {
            _config = config;
            _accountService = loginService;
            _appSettings = appSettings.Value;
            _smtpSettings = smtpSettings.Value;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _jwtAuthenticationService = jwtAuthenticationService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Login With Email And Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<ApiPostResponse<LoginResponseModel>> LoginUser([FromBody] LoginRequestModel model)
        {
            ApiPostResponse<LoginResponseModel> response = new ApiPostResponse<LoginResponseModel>() { Data = new LoginResponseModel() };

            var UserStatus = await _accountService.UserActiveInActive(model.EmailId, true);
            if (UserStatus == 1)
            {
                model.Password = EncryptionDecryption.GetEncrypt(model.Password);
                var res = await _accountService.GetUserSalt(model.EmailId, true);
                if (res != null)
                {
                    string Hash = EncryptionDecryption.GetDecrypt(res.Password);
                    string Salt = EncryptionDecryption.GetDecrypt(res.PasswordSalt);
                    bool isPasswordMatched = EncryptionDecryption.Verify(model.Password, Hash, Salt);
                    if (isPasswordMatched)
                    {
                        model.Password = res.Password;
                        model.IsAdmin = true;
                        LoginResponseModel result = await _accountService.LoginUser(model);
                        if (result != null && result.AdminUserId > 0)
                        {
                            TokenModel objTokenData = new TokenModel();
                            objTokenData.EmailId = model.EmailId;
                            objTokenData.UserId = result.AdminUserId != null ? result.AdminUserId : 0;
                            objTokenData.FullName = result.FullName;
                            objTokenData.IsAdmin = true;
                            objTokenData.IsEaziBusinessPartner = true;
                            AccessTokenModel objAccessTokenData = _jwtAuthenticationService.GenerateToken(objTokenData, _appSettings.JWT_Secret, _appSettings.JWT_Validity_Mins);
                            result.JWTToken = objAccessTokenData.Token;
                            await _accountService.UpdateLoginToken(objAccessTokenData.Token, objAccessTokenData.UserId, true);
                            response.Message = ErrorMessages.LoginSuccess;
                            response.Success = true;
                            response.Data.JWTToken = result.JWTToken.ToString();
                            response.Data.AdminUserId = result.AdminUserId;
                            response.Data.FullName = result.FullName;
                            response.Data.EmailId = result.EmailId;
                            response.Data.IsFirstLogin = result.IsFirstLogin;
                            response.Data.IsEaziBusinessPartner = true;
                            string path = _hostingEnvironment.WebRootPath + _config["Data:UserProfilePhoto"] + result.Photo;
                            if (System.IO.File.Exists(path))
                            {
                                string urlPath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;

                                string imagePath = urlPath + _config["Data:UserProfilePhoto"] + result.Photo;
                                result.Photo = imagePath;
                            }
                            else
                            {
                                result.Photo = string.Empty;
                            }
                            response.Data.Photo = result.Photo;
                            return response;
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = ErrorMessages.InvalidCredential;
                            return response;
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = ErrorMessages.InvalidCredential;
                        return response;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = ErrorMessages.UserInActive;
                    return response;
                }
            }
            else if (UserStatus == 2)
            {
                response.Success = false;
                response.Message = ErrorMessages.UserInActive;
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = ErrorMessages.InvalidEmailId;
                return response;
            }
        }

        /// <summary>
        /// Logout The User
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<BaseApiResponse> Logout()
        {
            BaseApiResponse response = new BaseApiResponse();
            string jwtToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace(JwtBearerDefaults.AuthenticationScheme + " ", "");
            TokenModel tokenModel = _jwtAuthenticationService.GetUserTokenData(jwtToken);
            var data = await _accountService.LogoutUser(tokenModel.UserId, jwtToken);
            if (data > 0)
            {
                response.Message = ErrorMessages.LogoutSuccess;
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
        /// Forget Passowrd With Email To User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("forget-password")]
        public async Task<BaseApiResponse> ForgetPassword([FromBody] ForgetPasswordRequestModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            var result = await _accountService.ForgetPassword(model.EmailId, true);
            var url = _httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString() + "verification-code";
            var portalUrl = _httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString().TrimEnd('/');
            if (result.AdminUserId > 0)
            {
                string EncryptedUserId = HttpUtility.UrlEncode(EncryptionDecryption.GetEncrypt(result.AdminUserId.ToString()));
                string EncryptedEamilId = HttpUtility.UrlEncode(EncryptionDecryption.GetEncrypt(model.EmailId.ToString()));
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
                string RandomNumer = CommonMethods.GenerateNewRandom();
                int OtpRandom = Convert.ToInt32(RandomNumer);
                string emailBody = string.Empty;
                string BasePath = Path.Combine(Directory.GetCurrentDirectory(), Constants.ExceptionReportPath);
                if (!Directory.Exists(BasePath))
                {
                    Directory.CreateDirectory(BasePath);
                }
                bool isSuccess = false;
                string LastForgetPasswordSend = Convert.ToString(String.Format("{0:yyyy-MM-dd HH:mm:ss}", result.LastForgetPasswordSend));
                string LastForgetPasswordDateTime = HttpUtility.UrlEncode(EncryptionDecryption.GetEncrypt(LastForgetPasswordSend));
                using (StreamReader reader = new StreamReader(Path.Combine(BasePath, Constants.ForgetPasswordEmailtem)))
                {
                    emailBody = reader.ReadToEnd();
                }
                var path = _httpContextAccessor.HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;
                TokenModel tokenModel = new TokenModel();
                string jwtToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace(JwtBearerDefaults.AuthenticationScheme + " ", "");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    tokenModel = _jwtAuthenticationService.GetUserTokenData(jwtToken);
                }
                emailBody = emailBody.Replace("##userName##", result.FullName.ToString());
                if (result.IsEaziBusinessPartner == true)
                {
                    emailBody = emailBody.Replace("##LogoURL##", path + "/" + _config["Path:Logo"]);
                    emailBody = emailBody.Replace("##BrandName##", "EAZI-BUSINESS");
                }
                else
                {
                    emailBody = emailBody.Replace("##LogoURL##", path + "/" + _config["Path:NonBrandLogo"]);
                    emailBody = emailBody.Replace("##BrandName##", "Cost Calculator");
                }
                emailBody = emailBody.Replace("##Password##", RandomNumer);
                emailBody = emailBody.Replace("##currentYear##", DateTime.Now.Year.ToString());
                emailBody = emailBody.Replace("##Link##", (url + '/' + EncryptedEamilId).ToString());
                emailBody = emailBody.Replace("##portalUrl##", portalUrl.ToString());
                isSuccess = await Task.Run(() => SendMailMessage(model.EmailId, null, null, "Reset Password OTP", emailBody, setting, null));
                int issaveopt = await _accountService.SaveOTP(result.AdminUserId, OtpRandom, result.EmailId, true);
                if (isSuccess == true && issaveopt == 1)
                {
                    response.Message = ErrorMessages.ForgetPasswordSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = ErrorMessages.ForgetPasswordError;
                    response.Success = false;
                }
                return response;
            }
            else
            {
                response.Message = ErrorMessages.UserError;
                response.Success = false;
                return response;
            }
        }

        /// <summary>
        /// Validate Whether The Reset Passowrd OTP Is Correct Or Not
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("verification-code")]
        public async Task<BaseApiResponse> verificationCode([FromBody] VerificationOTPRequestModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            if (ModelState.IsValid)
            {
                int PasswrodValid = _appSettings.PasswordLinkValidityMins;
                long UserId = await _accountService.GetUserIDByEmail(model.EmailId, true);
                string result = await _accountService.VerificationCode(UserId, model.OTP, PasswrodValid, true);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = ErrorMessages.VerifyCode;
                    response.Success = true;
                }
                else
                {
                    response.Message = result;
                    response.Success = false;
                }
            }
            return response;
        }

        /// <summary>
        /// Reset Password Of User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<BaseApiResponse> ResetPassword([FromBody] ResetPasswordRequestModel model)
        {
            model.ConfirmPassword = model.ConfirmPassword.Trim();
            model.NewPassword = model.NewPassword.Trim();

            BaseApiResponse response = new BaseApiResponse();

            #region Validation 
            if (string.IsNullOrEmpty(model.EmailId))
            {
                response.Message = ErrorMessages.EmailIsRequired;
                response.Success = false;
                return response;
            }
            if (!CommonMethods.IsValidEmail(model.EmailId))
            {
                response.Message = ErrorMessages.EnterValidEmail;
                response.Success = false;
                return response;
            }
            if (string.IsNullOrEmpty(model.NewPassword))
            {
                response.Message = ErrorMessages.PasswordValidation;
                response.Success = false;
                return response;
            }
            if (string.IsNullOrEmpty(model.ConfirmPassword))
            {
                response.Message = ErrorMessages.PasswordValidationConfirm;
                response.Success = false;
                return response;
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                response.Message = ErrorMessages.ConfirmPassword;
                response.Success = false;
                return response;
            }
            if (!CommonMethods.IsPasswordStrong(model.NewPassword))
            {
                response.Message = ErrorMessages.StrongPassword;
                response.Success = false;
                return response;
            }
            #endregion

            string hashed = EncryptionDecryption.Hash(EncryptionDecryption.GetEncrypt(model.NewPassword));
            string[] segments = hashed.Split(":");
            string EncryptedHash = EncryptionDecryption.GetEncrypt(segments[0]);
            string EncryptedSalt = EncryptionDecryption.GetEncrypt(segments[1]);
            long UserId = await _accountService.GetUserIDByEmail(model.EmailId, true);
            var result = await _accountService.ResetPassword(UserId, model.EmailId, EncryptedHash, EncryptedSalt, true);
            if (string.IsNullOrEmpty(result))
            {
                response.Message = ErrorMessages.ResetPasswordSuccess;
                response.Success = true;
            }
            else
            {
                response.Message = ErrorMessages.SomethingWentWrong;
                response.Success = false;
                Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            return response;
        }

        /// <summary>
        /// Change Password Of User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("change-password")]
        public async Task<BaseApiResponse> UserChangePassword([FromBody] ChangePasswordRequestModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            TokenModel tokenModel = new TokenModel();
            string jwtToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace(JwtBearerDefaults.AuthenticationScheme + " ", "");
            if (!string.IsNullOrEmpty(jwtToken))
            {
                tokenModel = _jwtAuthenticationService.GetUserTokenData(jwtToken);
            }
            model.OldPassword = EncryptionDecryption.GetEncrypt(model.OldPassword.Trim());
            model.CreatePassword = EncryptionDecryption.GetEncrypt(model.CreatePassword.Trim());
            model.ConfirmPassword = EncryptionDecryption.GetEncrypt(model.ConfirmPassword.Trim());
            var res = await _accountService.GetUserSalt(tokenModel.EmailId, true);
            bool isPasswordSame = true;
            bool isPasswordMatched = true;
            if (tokenModel.UserId == 0 || string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.CreatePassword) || string.IsNullOrEmpty(model.ConfirmPassword))
            {
                response.Message = ErrorMessages.PasswordFieldValidation;
                response.Success = false;
                return response;
            }
            if (res != null)
            {
                string Hash = EncryptionDecryption.GetDecrypt(res.Password);
                string Salt = EncryptionDecryption.GetDecrypt(res.PasswordSalt);

                isPasswordMatched = EncryptionDecryption.Verify(model.OldPassword, Hash, Salt);
                isPasswordSame = EncryptionDecryption.Verify(model.CreatePassword, Hash, Salt);
            }

            #region Validation 
            if (!isPasswordMatched)
            {
                response.Message = ErrorMessages.PasswordCheck;
                response.Success = false;
                return response;
            }
            if (isPasswordSame)
            {
                response.Message = ErrorMessages.PasswordMatch;
                response.Success = false;
                return response;
            }
            if (string.IsNullOrEmpty(model.CreatePassword) || string.IsNullOrEmpty(model.ConfirmPassword))
            {
                response.Message = ErrorMessages.PasswordValidation;
                response.Success = false;
                return response;
            }
            if (model.CreatePassword != model.ConfirmPassword)
            {
                response.Message = ErrorMessages.ConfirmPassword;
                response.Success = false;
                return response;
            }
            //if (!CommonMethods.IsPasswordStrong(EncryptionDecryption.GetDecrypt(model.CreatePassword)))
            //{
            //    response.Message = ErrorMessages.StrongPassword;
            //    response.Success = false;
            //    return response;
            //}
            #endregion

            string hashed = EncryptionDecryption.Hash(model.CreatePassword);
            string[] segments = hashed.Split(":");
            string EncryptedHash = EncryptionDecryption.GetEncrypt(segments[0]);
            string EncryptedSalt = EncryptionDecryption.GetEncrypt(segments[1]);
            var result = await _accountService.ChangePassword(tokenModel.UserId, EncryptedHash, EncryptedSalt, true);
            if (string.IsNullOrEmpty(result))
            {
                response.Message = ErrorMessages.ResetPasswordSuccess;
                response.Success = true;
            }
            else
            {
                response.Message = ErrorMessages.SomethingWentWrong;
                response.Success = false;
                Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            return response;
        }
        #endregion
    }
}