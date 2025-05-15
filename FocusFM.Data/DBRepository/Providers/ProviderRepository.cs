using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FocusFM.Common.Helpers;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Config;
using FocusFM.Model.Providers;
using FocusFM.Model.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FocusFM.Data.DBRepository.Providers
{
    public class ProviderRepository : BaseRepository, IProviderRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public ProviderRepository
        (
            IConfiguration config,
            IOptions<DataConfig> dataConfig
        ) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Methods
        public async Task<int> SaveProvider(ProviderRequestModel model, long CurrentProviderId,string? fileName)
        {
            var param = new DynamicParameters();
            param.Add("@ProviderId", model.ProviderId);
            param.Add("@ProviderName", model.ProviderName);
            param.Add("@ProviderTemplate", fileName);
            param.Add("@CreatedBy", CurrentProviderId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.SaveProvider, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<ProviderResponseModel>> GetProviderList(CommonPaginationModel model)
        {
            var param = new DynamicParameters();
            param.Add("@pageIndex", model.PageNumber);
            param.Add("@pageSize", model.PageSize);
            param.Add("@orderBy", model.SortColumn);
            param.Add("@sortOrder", model.SortOrder);
            param.Add("@strSearch", model.StrSearch);
            var data = await QueryAsync<ProviderResponseModel>(StoredProcedures.GetProviderList, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
        public async Task<int> DeleteProvider(int ProviderId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@ProviderId", ProviderId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.DeleteProvider, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ActiveInActiveProvider(int ProviderId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@ProviderId", ProviderId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.ActiveInActiveProvider, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<ProviderDropdownResponseModel>> GetProviderDropdown()
        {
            var data = await QueryAsync<ProviderDropdownResponseModel>(StoredProcedures.GetProviderDropdown, null, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        #endregion
    }
}
