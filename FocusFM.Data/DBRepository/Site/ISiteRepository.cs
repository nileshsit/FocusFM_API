using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Site;
using FocusFM.Model.Site.Floor;
using FocusFM.Model.Site.Meter;

namespace FocusFM.Data.DBRepository.Site
{
    public interface ISiteRepository
    {
        #region Site
        Task<List<SiteResponseModel>> GetSiteList(CommonPaginationModel model);
        Task<int> SaveSite(SiteRequestModel model, long CurrentUserId, string? fileName);
        Task<int> DeleteSite(long SiteId, long CurrentUserId);
        Task<int> ActiveInActiveSite(long SiteId, long CurrentUserId);
        #endregion

        #region Sites Floor
        Task<List<SiteFloorResponseModel>> GetFloorList(GetSiteFloorModel model);
        Task<int> SaveFloor(SiteFloorRequestModel model, long CurrentUserId);
        Task<int> DeleteFloor(long FloorId, long CurrentUserId);
        Task<int> ActiveInActiveFloor(long FloorId, long CurrentUserId);
        Task<List<FloorDropdownResponseModel>> GetFloorDropdown(long SiteId);
        #endregion


        #region Meter
        Task<List<MeterReadingTypeResponseModel>> GetMeterReadingTypeDropdown();
        Task<List<MeterTypeResponseModel>> GetMeterTypeDropdown();
        public Task<int> SaveMeter(MeterRequestModel model, long id);
        public Task<List<MeterResponseModel>> GetMeterList(GetMeterListRequestModel model);
        Task<int> DeleteMeter(long MeterId, long CurrentUserId);
        Task<int> ActiveInActiveMeter(long MeterId, long CurrentUserId);
        #endregion

    }
}
