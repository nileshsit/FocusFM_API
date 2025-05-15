using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Providers;
using FocusFM.Model.User;

namespace FocusFM.Service.Providers
{
    public interface IProviderService
    {
        Task<List<ProviderResponseModel>> GetProviderList(CommonPaginationModel model);
        Task<int> SaveProvider(ProviderRequestModel model, long CurrentUserId,string? fileName);
        Task<int> DeleteProvider(int ProviderId, long CurrentUserId);
        Task<int> ActiveInActiveProvider(int ProviderId, long CurrentUserId);
        Task<List<ProviderDropdownResponseModel>> GetProviderDropdown();
    }
}
