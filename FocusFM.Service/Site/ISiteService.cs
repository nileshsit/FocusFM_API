using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Site;

namespace FocusFM.Service.Site
{
    public interface ISiteService
    {
        #region Sites
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
        #endregion
    }
}
