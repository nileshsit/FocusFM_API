using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Data.DBRepository.Site;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Site;
using FocusFM.Model.Site.Floor;
using FocusFM.Model.Site.Meter;

namespace FocusFM.Service.Site
{
    public class SiteService:ISiteService
    {
        #region Fields
        private readonly ISiteRepository _repository;
        #endregion

        #region Construtor
        public SiteService(ISiteRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Site Methods

        public Task<int> SaveSite(SiteRequestModel model, long id, string? fileName)
        {
            return _repository.SaveSite(model, id, fileName);
        }

        public Task<List<SiteResponseModel>> GetSiteList(CommonPaginationModel model)
        {
            return _repository.GetSiteList(model);
        }

        public Task<int> DeleteSite(long SiteId, long CurrentUserId)
        {
            return _repository.DeleteSite(SiteId,CurrentUserId);
        }

        public Task<int> ActiveInActiveSite(long SiteId, long CurrentUserId)
        {
            return _repository.ActiveInActiveSite(SiteId,CurrentUserId);
        }
        #endregion

        #region Site Floor Methods

        public Task<int> SaveFloor(SiteFloorRequestModel model, long id)
        {
            return _repository.SaveFloor(model, id);
        }

        public Task<List<SiteFloorResponseModel>> GetFloorList(GetSiteFloorModel model)
        {
            return _repository.GetFloorList(model);
        }

        public Task<int> DeleteFloor(long FloorId, long CurrentUserId)
        {
            return _repository.DeleteFloor(FloorId, CurrentUserId);
        }

        public Task<int> ActiveInActiveFloor(long FloorId, long CurrentUserId)
        {
            return _repository.ActiveInActiveFloor(FloorId, CurrentUserId);
        }
        public Task<List<FloorDropdownResponseModel>> GetFloorDropdown(long SiteId)
        {
            return _repository.GetFloorDropdown(SiteId);
        }

        #endregion

        #region Meter
        public Task<List<MeterReadingTypeResponseModel>> GetMeterReadingTypeDropdown()
        {
            return _repository.GetMeterReadingTypeDropdown();
        }
        public Task<List<MeterTypeResponseModel>> GetMeterTypeDropdown()
        {
            return _repository.GetMeterTypeDropdown();
        }
        #endregion
    }
}
