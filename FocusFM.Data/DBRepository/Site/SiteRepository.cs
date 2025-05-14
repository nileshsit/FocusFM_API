

using System.Data;
using Dapper;
using FocusFM.Common.Helpers;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Config;
using FocusFM.Model.Site;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FocusFM.Data.DBRepository.Site
{
    public class SiteRepository: BaseRepository,ISiteRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public SiteRepository
        (
            IConfiguration config,
            IOptions<DataConfig> dataConfig
        ) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Methods
        public async Task<int> SaveSite(SiteRequestModel model, long id, string? fileName)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", model.SiteId);
            param.Add("@SiteName", model.SiteName);
            param.Add("@ContactPersonName", model.ContactPersonName);
            param.Add("@ContactPersonEmailId", model.ContactPersonEmailId);
            param.Add("@EmailId", model.EmailId);
            param.Add("@ContactPersonMobNo", model.ContactPersonMobNo);
            param.Add("@Address", model.Address);
            param.Add("@City", model.City);
            param.Add("@Country", model.Country);
            param.Add("@PinCode", model.PinCode);
            param.Add("@SiteImage", "");
            param.Add("@CreatedBy", id);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.SaveSite, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<SiteResponseModel>> GetSiteList(CommonPaginationModel model)
        {
            var param = new DynamicParameters();
            param.Add("@pageIndex", model.PageNumber);
            param.Add("@pageSize", model.PageSize);
            param.Add("@orderBy", model.SortColumn);
            param.Add("@sortOrder", model.SortOrder);
            param.Add("@strSearch", model.StrSearch);
            var data = await QueryAsync<SiteResponseModel>(StoredProcedures.GetSiteList, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
        public async Task<int> DeleteSite(int SiteId)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", SiteId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.DeleteSite, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ActiveInActiveSite(int SiteId)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", SiteId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.ActiveInActiveSite, param, commandType: CommandType.StoredProcedure);
            return result;
        }
        #endregion
}
}
