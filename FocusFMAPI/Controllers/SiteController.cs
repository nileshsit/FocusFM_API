using System.IdentityModel.Tokens.Jwt;
using FocusFM.Common.CommonMethod;
using FocusFM.Common.Enum;
using FocusFM.Common.Helpers;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Site;
using FocusFM.Service.JWTAuthentication;
using FocusFM.Service.Site;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FocusFMAPI.Controllers
{
    [Route("api/site")]
    [ApiController]
    [Authorize]
    public class SiteController : ControllerBase
    {
        #region Fields
        private readonly ISiteService _SiteService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        private readonly IConfiguration _config;
        #endregion

        #region Constructor
        public SiteController
        (
            ISiteService SiteService,
            IHttpContextAccessor httpContextAccessor,
            IJWTAuthenticationService jwtAuthenticationService,
            IConfiguration config
        )
        {
            _SiteService = SiteService;
            _httpContextAccessor = httpContextAccessor;
            _jwtAuthenticationService = jwtAuthenticationService;
            _config = config;
        }
        #endregion

        [HttpPost("save")]
        public async Task<BaseApiResponse> SaveSite([FromForm] SiteRequestModel model)
        {
            IFormFile file = model.File;
            ApiResponse<SiteResponseModel> response = new ApiResponse<SiteResponseModel>();
            if (model.SiteId == null)
            {
                if (file == null || file.Length == 0)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = ErrorMessages.NoFile;
                    return response;
                }
            }
            long UserId = 0;
            string fileName = null;
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
                string[] arrExtension = _config["FileConfiguration:AllowedProfileFileFormat"].Split(",");
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!arrExtension.Contains(extension))
                {
                    response.Success = false;
                    response.Message = ErrorMessages.InvalidExcelFile;
                    return response;
                }

                long allowedSize = long.Parse(_config["FileConfiguration:AllowedProfileFileSize"]);

                if (file.Length > allowedSize)
                {
                    response.Success = false;
                    response.Message = ErrorMessages.FileSizeExceeds;
                    return response;
                }

                fileName = await CommonMethods.UploadDocument(file, _config["FileConfiguration:SiteImageFilePath"]+"/");
            }
            var result = await _SiteService.SaveSite(model, UserId, fileName);
            var issend = false;
            if (result == Status.Success)
            {
                if (model.SiteId > 0)
                {
                    response.Message = ErrorMessages.UpdateSiteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = ErrorMessages.SaveSiteSuccess;
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
        public async Task<ApiResponse<SiteResponseModel>> GetSiteList(CommonPaginationModel model)
        {
            ApiResponse<SiteResponseModel> response = new ApiResponse<SiteResponseModel>() { Data = new List<SiteResponseModel>() };
            var result = await _SiteService.GetSiteList(model);
            foreach (var record in result)
            {
                // Example: Update file path if it exists
                if (record.SiteImage != null)
                {
                    string originalPath = record.SiteImage.ToString();

                    var path = _httpContextAccessor.HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;

                    // Example: Replace part of the path or add prefix
                    record.SiteImage = originalPath.Replace("/wwwroot", "");
                    record.SiteImage = originalPath.Replace(Directory.GetCurrentDirectory(), _config["AppSettings:APIURL"]);
                    record.SiteImage = record.SiteImage.Replace("\\", "/");
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
        public async Task<BaseApiResponse> DeleteSite(int id)
        {
            BaseApiResponse response = new BaseApiResponse();
            var result = await _SiteService.DeleteSite(id);
            if (result == 0)
            {
                response.Message = ErrorMessages.DeleteSiteSuccess;
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
        public async Task<BaseApiResponse> ActiveInActiveSite(int id)
        {
            BaseApiResponse response = new BaseApiResponse();
            var result = await _SiteService.ActiveInActiveSite(id);
            if (result == ActiveStatus.Inactive)
            {
                response.Message = ErrorMessages.SiteInactive;
                response.Success = true;
            }
            else if (result == ActiveStatus.Active)
            {
                response.Message = ErrorMessages.SiteActive;
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
