using FocusFM.Common.CommonMethod;
using FocusFM.Common.EmailNotification;
using FocusFM.Common.Enum;
using FocusFM.Common.Helpers;
using FocusFM.Model.AdminUser;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Settings;
using FocusFM.Model.Token;
using FocusFM.Model.User;
using FocusFM.Service.JWTAuthentication;
using FocusFM.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using static FocusFM.Common.EmailNotification.EmailNotification;

namespace FocusFMAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly SMTPSettings _smtpSettings;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        #endregion

        #region Constructor
        public UserController
        (
            IUserService UserService,
            IHttpContextAccessor httpContextAccessor,
            IJWTAuthenticationService jwtAuthenticationService,
            IOptions<SMTPSettings> smtpSettings,
            IOptions<AppSettings> appSettings,
            IConfiguration config
        )
        {
            _userService = UserService;
            _appSettings = appSettings.Value;
            _smtpSettings = smtpSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _jwtAuthenticationService = jwtAuthenticationService;
            _config = config;
        }
        #endregion

        [HttpPost("save")]
        public async Task<BaseApiResponse> SaveUser([FromBody] UserRequestModel model)
        {
            BaseApiResponse response = new BaseApiResponse();

            model.FirstName = model.FirstName.Trim();
            model.PinCode = model.PinCode.Trim();
            model.Address = model.Address.Trim();
            model.City = model.City.Trim();
            model.Country = model.Country.Trim();
            var password = "";
            var passSalt = "";
            var Generatepassword = "";
            if (!CommonMethods.IsValidEmail(model.EmailId))
            {
                response.Message = ErrorMessages.InvalidEmailId;
                response.Success = false;
                return response;
            }
            TokenModel tokenModel = new TokenModel();
            string jwtToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace(JwtBearerDefaults.AuthenticationScheme + " ", "");
            var result = await _userService.SaveUser(model, tokenModel.UserId);
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

        [HttpPost("list")]
        public async Task<ApiResponse<UserResponseModel>> GetUserList(CommonPaginationModel model)
        {
            ApiResponse<UserResponseModel> response = new ApiResponse<UserResponseModel>() { Data = new List<UserResponseModel>() };
            var result = await _userService.GetUserList(model);
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        [HttpDelete("delete/{id}")]
        public async Task<BaseApiResponse> DeleteUser(long id)
        {
            BaseApiResponse response = new BaseApiResponse();
            var result = await _userService.DeleteUser(id);
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

        [HttpGet("active-inactive/{id}")]
        public async Task<BaseApiResponse> ActiveInActiveUser(long id)
        {
            BaseApiResponse response = new BaseApiResponse();
            var result = await _userService.ActiveInActiveUser(id);
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

        [HttpPost("user-type-dropdown")]
        public async Task<ApiResponse<UserTypeResponseModel>> GetUserTypeDropdown()
        {
            ApiResponse<UserTypeResponseModel> response = new ApiResponse<UserTypeResponseModel>() { Data = new List<UserTypeResponseModel>() };
            var result = await _userService.GetUserTypeDropdown();
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }
    }
}
