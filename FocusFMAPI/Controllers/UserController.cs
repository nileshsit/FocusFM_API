using System.Data;
using System.IdentityModel.Tokens.Jwt;
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
using Newtonsoft.Json;
using static FocusFM.Common.EmailNotification.EmailNotification;

namespace FocusFMAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        #endregion

        #region Constructor
        public UserController
        (
            IUserService UserService,
            IHttpContextAccessor httpContextAccessor,
            IJWTAuthenticationService jwtAuthenticationService
        )
        {
            _userService = UserService;
            _httpContextAccessor = httpContextAccessor;
            _jwtAuthenticationService = jwtAuthenticationService;
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
            long UserId = 0;
            if (Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                // Parse the JWT token
                var token = authHeader.ToString().Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Extract user ID from the token claims
                var j = JsonConvert.DeserializeObject<Dictionary<string, string>>(jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value);

                UserId = long.TryParse(j["UserId"], out var val) ? val : 0;
            }
            var result = await _userService.SaveUser(model, UserId);
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
                response.Message = ErrorMessages.UserExists;
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
        public async Task<ApiResponse<UserResponseModel>> GetUserList(GetUserListRequestModel model)
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

        [HttpGet("active-inactive/{id}")]
        public async Task<BaseApiResponse> ActiveInActiveUser(long id)
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
            var result = await _userService.ActiveInActiveUser(id,UserId);
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

        [HttpPost("landlord-dropdown")]
        public async Task<ApiResponse<UserDropdownResponseModel>> GetLandlordDropdown()
        {
            ApiResponse<UserDropdownResponseModel> response = new ApiResponse<UserDropdownResponseModel>() { Data = new List<UserDropdownResponseModel>() };
            var result = await _userService.GetUserDropdown(UserType.Landlord);
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        [HttpPost("tenant-dropdown")]
        public async Task<ApiResponse<UserDropdownResponseModel>> GetTenantDropdown()
        {
            ApiResponse<UserDropdownResponseModel> response = new ApiResponse<UserDropdownResponseModel>() { Data = new List<UserDropdownResponseModel>() };
            var result = await _userService.GetUserDropdown(UserType.Tenant);
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportUserData(UserExportRequestModel model)
        {
            ApiResponse<UserExportResponseModel> response = new ApiResponse<UserExportResponseModel>() { Data = new List<UserExportResponseModel>() };
            var result = await _userService.GetUserExportData(model);
            if (result != null)
            {
                DataTable dt = Utility.ToDataTable(result);
                dt.Columns["FirstName"].ColumnName = "Name";
                dt.Columns["UserType"].ColumnName = "User Type";
                dt.Columns["EmailId"].ColumnName = "Email";
                dt.Columns["MobileNo"].ColumnName = "Phone No.";
                // Get Excel as byte array
                var fileBytes = ExportHelper.ExportToExcel(dt,"Tenant(s) / LandLord(s)", "Users");

                // Return the file directly without stream
                return File(fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Users.xlsx");
            }
            return BadRequest();
        }
    }
}
