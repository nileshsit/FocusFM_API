using System.Configuration;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using FocusFM.Common.CommonMethod;
using FocusFM.Common.Enum;
using FocusFM.Common.Helpers;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Providers;
using FocusFM.Model.Token;
using FocusFM.Service.JWTAuthentication;
using FocusFM.Service.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace FocusFMAPI.Controllers
{
    [Route("api/provider")]
    [ApiController]
    [Authorize]
    public class ProviderController : ControllerBase
    {
        #region Fields
        private readonly IProviderService _ProviderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        private readonly IConfiguration _config;
        #endregion

        #region Constructor
        public ProviderController
        (
            IProviderService ProviderService,
            IHttpContextAccessor httpContextAccessor,
            IJWTAuthenticationService jwtAuthenticationService,
            IConfiguration config
        )
        {
            _ProviderService = ProviderService;
            _httpContextAccessor = httpContextAccessor;
            _jwtAuthenticationService = jwtAuthenticationService;
            _config = config;
        }
        #endregion

        [HttpPost("save")]
        public async Task<BaseApiResponse> SaveProvider([FromForm] ProviderRequestModel model, IFormFile? file)
        {
            ApiResponse<ProviderResponseModel> response = new ApiResponse<ProviderResponseModel>();
            if (model.ProviderId == null)
            {
                if (file == null || file.Length == 0)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = ErrorMessages.NoFile;
                    return response;
                }
            }
            long UserId=0;
            string fileName=null;
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
            if (file != null)
            {
                string[] arrExtension = _config["FileConfiguration:AllowedProviderFileFormat"].Split(",");
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!arrExtension.Contains(extension))
                {
                    response.Success = false;
                    response.Message = ErrorMessages.InvalidExcelFile;
                    return response;
                }

                long allowedSize = long.Parse(_config["FileConfiguration:AllowedProviderFileSize"]);

                if (file.Length > allowedSize)
                {
                    response.Success = false;
                    response.Message = ErrorMessages.FileSizeExceeds;
                    return response;
                }

                fileName = await CommonMethods.UploadDocument(file, _config["FileConfiguration:ProviderTemplateFilePath"] +"/" + model.ProviderName + "/");
            }
            var result = await _ProviderService.SaveProvider(model, UserId, fileName);
            var issend = false;
            if (result == Status.Success)
            {
                if (model.ProviderId > 0)
                {
                    response.Message = ErrorMessages.UpdateProviderSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = ErrorMessages.SaveProviderSuccess;
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
            return response;
        }

        [HttpPost("list")]
        public async Task<ApiResponse<ProviderResponseModel>> GetProviderList(CommonPaginationModel model)
        {
            ApiResponse<ProviderResponseModel> response = new ApiResponse<ProviderResponseModel>() { Data = new List<ProviderResponseModel>() };
            var result = await _ProviderService.GetProviderList(model);
            foreach (var record in result)
            {
                // Example: Update file path if it exists
                if (record.ProviderTemplate != null)
                {
                    string originalPath = record.ProviderTemplate.ToString();


                    // Example: Replace part of the path or add prefix
                    record.ProviderTemplate = originalPath.Replace(Directory.GetCurrentDirectory(), _config["AppSettings:APIURL"]);
                    record.ProviderTemplate = record.ProviderTemplate.Replace("\\","/");
                }
            }
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        [HttpDelete("delete/{id}")]
        public async Task<BaseApiResponse> DeleteProvider(int id)
        {
            BaseApiResponse response = new BaseApiResponse();
            var result = await _ProviderService.DeleteProvider(id);
            if (result == 0)
            {
                response.Message = ErrorMessages.DeleteProviderSuccess;
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
        public async Task<BaseApiResponse> ActiveInActiveProvider(int id)
        {
            BaseApiResponse response = new BaseApiResponse();
            var result = await _ProviderService.ActiveInActiveProvider(id);
            if (result == ActiveStatus.Inactive)
            {
                response.Message = ErrorMessages.ProviderInactive;
                response.Success = true;
            }
            else if (result == ActiveStatus.Active)
            {
                response.Message = ErrorMessages.ProviderActive;
                response.Success = true;
            }
            else
            {
                response.Message = ErrorMessages.SomethingWentWrong;
                response.Success = false;
            }
            return response;
        }
    }
}
