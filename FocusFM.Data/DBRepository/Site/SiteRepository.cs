

using System;
using System.Data;
using System.Diagnostics.Metrics;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using FocusFM.Common.Helpers;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Config;
using FocusFM.Model.Site;
using FocusFM.Model.Site.Floor;
using FocusFM.Model.Site.Meter;
using FocusFM.Model.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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

        #region Site Methods
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
            param.Add("@PinCode", model.PinCode);
            param.Add("@SiteImage", fileName);
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
        public async Task<int> DeleteSite(long SiteId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", SiteId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.DeleteSite, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ActiveInActiveSite(long SiteId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", SiteId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.ActiveInActiveSite, param, commandType: CommandType.StoredProcedure);
            return result;
        }
        #endregion

        #region Site Floor Methods
        public async Task<int> SaveFloor(SiteFloorRequestModel model, long id)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", model.SiteId);
            param.Add("@FloorName", model.FloorName);
            param.Add("@FloorId", model.FloorId);
            param.Add("@CreatedBy", id);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.SaveFloor, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<SiteFloorResponseModel>> GetFloorList(GetSiteFloorModel model)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", model.SiteId);
            param.Add("@pageIndex", model.PageNumber);
            param.Add("@pageSize", model.PageSize);
            param.Add("@orderBy", model.SortColumn);
            param.Add("@sortOrder", model.SortOrder);
            param.Add("@strSearch", model.StrSearch);
            var data = await QueryAsync<SiteFloorResponseModel>(StoredProcedures.GetFloorList, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
        public async Task<int> DeleteFloor(long FloorId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@FloorId", FloorId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.DeleteFloor, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ActiveInActiveFloor(long FloorId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@FloorId", FloorId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.ActiveInActiveFloor, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<FloorDropdownResponseModel>> GetFloorDropdown(long SiteId)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", SiteId);
            var data = await QueryAsync<FloorDropdownResponseModel>(StoredProcedures.GetFloorDropdown, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        #endregion

        #region Meter
        public async Task<List<MeterReadingTypeResponseModel>> GetMeterReadingTypeDropdown()
        {
            var data = await QueryAsync<MeterReadingTypeResponseModel>(StoredProcedures.GetMeterReadingTypeDropdown, null, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<MeterTypeResponseModel>> GetMeterTypeDropdown()
        {
            var data = await QueryAsync<MeterTypeResponseModel>(StoredProcedures.GetMeterTypeDropdown, null, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<int> SaveMeter(MeterRequestModel model, long id)
        {
            var param = new DynamicParameters();
            param.Add("@MeterId", model.MeterId);
            param.Add("@SiteId", model.SiteId);
            param.Add("@MeterName", model.MeterName);
            param.Add("@MeterSerialNo", model.MeterSerialNo);
            param.Add("@MeterReadingTypeId", model.MeterReadingTypeId);
            param.Add("@MeterTypeId", model.MeterTypeId);
            param.Add("@UserTypeId", model.UserTypeId);
            param.Add("@ProviderId", model.ProviderId);
            param.Add("@LandlordId", model.LandlordId);
            param.Add("@TenantId", model.TenantId);
            param.Add("@FloorId", model.FloorId);
            param.Add("@CreatedBy", id);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.SaveMeter, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<MeterResponseModel>> GetMeterList(GetMeterListRequestModel model)
        {
            var param = new DynamicParameters();
            param.Add("@MeterTypeId", model.MeterTypeId);
            param.Add("@SiteId", model.SiteId);
            param.Add("@pageIndex", model.PageNumber);
            param.Add("@pageSize", model.PageSize);
            param.Add("@orderBy", model.SortColumn);
            param.Add("@sortOrder", model.SortOrder);
            param.Add("@strSearch", model.StrSearch);
            var data = await QueryAsync<MeterResponseModel>(StoredProcedures.GetMeterList, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
        public async Task<int> DeleteMeter(long MeterId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@MeterId", MeterId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.DeleteMeter, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ActiveInActiveMeter(long MeterId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@MeterId", MeterId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.ActiveInActiveMeter, param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<List<MeterExportResponseModel>> GetMeterExportData(MeterExportRequestModel model)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", model.SiteId);
            param.Add("@MeterTypeId", model.MeterTypeId);
            param.Add("@strSearch", model.StrSearch);
            var data = await QueryAsync<MeterExportResponseModel>(StoredProcedures.GetMeterExportData, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<MeterDropdownResponseModel>> GetMeterDropdown(MeterDropdownRequestModel model)
        {
            var param = new DynamicParameters();
            param.Add("@SiteId", model.SiteId);
            param.Add("@MeterTypeId", model.MeterTypeId);
            param.Add("@ProviderId", model.ProviderId);
            var data = await QueryAsync<MeterDropdownResponseModel>(StoredProcedures.GetMeterDropdown, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<MeterBulkImportResponseModel>> ValidateInsertMeterBulkImport(List<MeterBulkImportRequestModel> model, long id)
        {
            var param = new DynamicParameters();

            // Create a parameter for the table-valued parameter (TVP)
            param.Add("@MeterData", JsonConvert.SerializeObject(model));
            param.Add("@CreatedBy", id);


            var result = await QueryAsync<MeterBulkImportResponseModel>(StoredProcedures.GetMeterBulkImport, param, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        #endregion
    }
}
