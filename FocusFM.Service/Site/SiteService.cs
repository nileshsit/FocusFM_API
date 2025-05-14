using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Data.DBRepository.Site;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Site;

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

        #region Methods

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
    }
}
