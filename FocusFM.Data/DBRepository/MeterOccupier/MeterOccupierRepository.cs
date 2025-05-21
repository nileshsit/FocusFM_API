using Dapper;
using FocusFM.Common.Helpers;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Config;
using FocusFM.Model.MeterOccupier;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;

namespace FocusFM.Data.DBRepository.MeterOccupier
{
    public class MeterOccupierRepository : BaseRepository, IMeterOccupierRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public MeterOccupierRepository
        (
            IConfiguration config,
            IOptions<DataConfig> dataConfig
        ) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Methods
        public async Task<List<MeterOccupierTypeResponseModel>> GetMeterOccupierTypeDropdown()
        {
            var data = await QueryAsync<MeterOccupierTypeResponseModel>(StoredProcedures.GetMeterOccupierTyeDropdown, null, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<int> SaveMeterOccupier(MeterOccupierRequestModel model, long CurrentMeterOccupierId)
        {
            var param = new DynamicParameters();
            param.Add("@MeterOccupierId", model.MeterOccupierId);
            param.Add("@MeterOccupierTypeId", model.MeterOccupierTypeId);
            param.Add("@FirstName", model.FirstName);
            param.Add("@PinCode", model.PinCode);
            param.Add("@EmailId", model.EmailId);
            param.Add("@MobileNo", model.MobileNo);
            param.Add("@Address", model.Address);
            param.Add("@City", model.City);
            param.Add("@Country", model.Country);
            param.Add("@CreatedBy", CurrentMeterOccupierId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.SaveMeterOccupier, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<MeterOccupierResponseModel>> GetMeterOccupierList(GetMeterOccupierListRequestModel model)
        {
            var param = new DynamicParameters();
            param.Add("@MeterOccupierTypeIds", model.MeterOccupierTypeIds);
            param.Add("@pageIndex", model.PageNumber);
            param.Add("@pageSize", model.PageSize);
            param.Add("@orderBy", model.SortColumn);
            param.Add("@sortOrder", model.SortOrder);
            param.Add("@strSearch", model.StrSearch);
            var data = await QueryAsync<MeterOccupierResponseModel>(StoredProcedures.GetMeterOccupierList, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
        public async Task<int> DeleteMeterOccupier(long MeterOccupierId, long CurrentMeterOccupierId)
        {
            var param = new DynamicParameters();
            param.Add("@MeterOccupierId", MeterOccupierId);
            param.Add("@ModifiedBy", CurrentMeterOccupierId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.DeleteMeterOccupier, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ActiveInActiveMeterOccupier(long MeterOccupierId, long CurrentMeterOccupierId)
        {
            var param = new DynamicParameters();
            param.Add("@MeterOccupierId", MeterOccupierId);
            param.Add("@ModifiedBy", CurrentMeterOccupierId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.ActiveInActiveMeterOccupier, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<MeterOccupierDropdownResponseModel>> GetMeterOccupierDropdown(int typeId)
        {
            var param = new DynamicParameters();
            param.Add("@MeterOccupierTypeId", typeId);
            var data = await QueryAsync<MeterOccupierDropdownResponseModel>(StoredProcedures.GetMeterOccupierDropdown, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<MeterOccupierExportResponseModel>> GetMeterOccupierExportData(MeterOccupierExportRequestModel model)
        {
            var param = new DynamicParameters();
            param.Add("@MeterOccupierTypeIds", model.MeterOccupierTypeIds);
            param.Add("@strSearch", model.StrSearch);
            var data = await QueryAsync<MeterOccupierExportResponseModel>(StoredProcedures.GetMeterExportData, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
        #endregion
    }
}
