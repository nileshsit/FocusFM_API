using System.Data;
using System.IdentityModel.Tokens.Jwt;
using FocusFM.Common.CommonMethod;
using FocusFM.Common.EmailNotification;
using FocusFM.Common.Enum;
using FocusFM.Common.Helpers;
using FocusFM.Model.MeterOccupier;
using FocusFM.Service.JWTAuthentication;
using FocusFM.Service.MeterOccupier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static FocusFM.Common.EmailNotification.EmailNotification;

namespace FocusFMAPI.Controllers
{
    [Route("api/meteroccupier")]
    [ApiController]
    [Authorize]
    public class MeterOccupierController : ControllerBase
    {
        #region Fields
        private readonly IMeterOccupierService _MeterOccupierService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        #endregion

        #region Constructor
        public MeterOccupierController
        (
            IMeterOccupierService MeterOccupierService,
            IHttpContextAccessor httpContextAccessor,
            IJWTAuthenticationService jwtAuthenticationService
        )
        {
            _MeterOccupierService = MeterOccupierService;
            _httpContextAccessor = httpContextAccessor;
            _jwtAuthenticationService = jwtAuthenticationService;
        }
        #endregion

        [HttpPost("save")]
        public async Task<BaseApiResponse> SaveMeterOccupier([FromBody] MeterOccupierRequestModel model)
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
            var result = await _MeterOccupierService.SaveMeterOccupier(model, UserId);
            var issend = false;
            if (result == Status.Success)
            {
                if (model.MeterOccupierId > 0)
                {
                    response.Message = ErrorMessages.UpdateMeterOccupierSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = ErrorMessages.SaveMeterOccupierSuccess;
                    response.Success = true;
                }
            }

            else if (result == Status.AlredyExists)
            {
                response.Message = ErrorMessages.MeterOccupierExists;
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
        public async Task<ApiResponse<MeterOccupierResponseModel>> GetMeterOccupierList(GetMeterOccupierListRequestModel model)
        {
            ApiResponse<MeterOccupierResponseModel> response = new ApiResponse<MeterOccupierResponseModel>() { Data = new List<MeterOccupierResponseModel>() };
            var result = await _MeterOccupierService.GetMeterOccupierList(model);
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        [HttpDelete("delete/{id}")]
        public async Task<BaseApiResponse> DeleteMeterOccupier(long id)
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
            var result = await _MeterOccupierService.DeleteMeterOccupier(id,UserId);
            if (result == 0)
            {
                response.Message = ErrorMessages.DeleteMeterOccupierSuccess;
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
        public async Task<BaseApiResponse> ActiveInActiveMeterOccupier(long id)
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
            var result = await _MeterOccupierService.ActiveInActiveMeterOccupier(id,UserId);
            if (result == ActiveStatus.Inactive)
            {
                response.Message = ErrorMessages.MeterOccupierInactive;
                response.Success = true;
            }
            else if (result == ActiveStatus.Active)
            {
                response.Message = ErrorMessages.MeterOccupierActive;
                response.Success = true;
            }
            else
            {
                response.Message = ErrorMessages.SomethingWentWrong;
                response.Success = false;
            }
            return response;
        }

        [HttpPost("meteroccupier-type-dropdown")]
        public async Task<ApiResponse<MeterOccupierTypeResponseModel>> GetMeterOccupierTypeDropdown()
        {
            ApiResponse<MeterOccupierTypeResponseModel> response = new ApiResponse<MeterOccupierTypeResponseModel>() { Data = new List<MeterOccupierTypeResponseModel>() };
            var result = await _MeterOccupierService.GetMeterOccupierTypeDropdown();
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        [HttpPost("landlord-dropdown")]
        public async Task<ApiResponse<MeterOccupierDropdownResponseModel>> GetLandlordDropdown()
        {
            ApiResponse<MeterOccupierDropdownResponseModel> response = new ApiResponse<MeterOccupierDropdownResponseModel>() { Data = new List<MeterOccupierDropdownResponseModel>() };
            var result = await _MeterOccupierService.GetMeterOccupierDropdown(MeterOccupierType.Landlord);
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        [HttpPost("tenant-dropdown")]
        public async Task<ApiResponse<MeterOccupierDropdownResponseModel>> GetTenantDropdown()
        {
            ApiResponse<MeterOccupierDropdownResponseModel> response = new ApiResponse<MeterOccupierDropdownResponseModel>() { Data = new List<MeterOccupierDropdownResponseModel>() };
            var result = await _MeterOccupierService.GetMeterOccupierDropdown(MeterOccupierType.Tenant);
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportMeterOccupierData(MeterOccupierExportRequestModel model)
        {
            ApiResponse<MeterOccupierExportResponseModel> response = new ApiResponse<MeterOccupierExportResponseModel>() { Data = new List<MeterOccupierExportResponseModel>() };
            var result = await _MeterOccupierService.GetMeterOccupierExportData(model);
            if (result != null)
            {
                DataTable dt = Utility.ToDataTable(result);
                dt.Columns["FirstName"].ColumnName = "Name";
                dt.Columns["MeterOccupierType"].ColumnName = "Meter Occupier Type";
                dt.Columns["EmailId"].ColumnName = "Email";
                dt.Columns["MobileNo"].ColumnName = "Phone No.";
                // Get Excel as byte array
                var fileBytes = ExportHelper.ExportToExcel(dt,"Tenant(s) / LandLord(s)", "MeterOccupiers");

                // Return the file directly without stream
                return File(fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "MeterOccupiers.xlsx");
            }
            return BadRequest();
        }

        [HttpPost("meter-list")]
        public async Task<ApiResponse<GetMeterUsingIdResponseModel>> GetMeterUsingId(GetMeterUsingIdRequestModel model)
        {
            ApiResponse<GetMeterUsingIdResponseModel> response = new ApiResponse<GetMeterUsingIdResponseModel>() { Data = new List<GetMeterUsingIdResponseModel>() };
            var result = await _MeterOccupierService.GetMeterUsingId(model);
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }        
       
    }
}
