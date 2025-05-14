using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Site;

namespace FocusFM.Data.DBRepository.Site
{
    public interface ISiteRepository
    {
        Task<List<SiteResponseModel>> GetSiteList(CommonPaginationModel model);
        Task<int> SaveSite(SiteRequestModel model, long CurrentUserId, string? fileName);
        Task<int> DeleteSite(long SiteId, long CurrentUserId);
        Task<int> ActiveInActiveSite(long SiteId, long CurrentUserId);
    }
}
