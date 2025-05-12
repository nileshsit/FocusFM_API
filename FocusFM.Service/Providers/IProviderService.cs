using FocusFM.Model.CommonPagination;
using FocusFM.Model.Providers;

namespace FocusFM.Service.Providers
{
    public interface IProviderService
    {
        Task<List<ProviderResponseModel>> GetProviderList(CommonPaginationModel model);
        Task<int> SaveProvider(ProviderRequestModel model, long CurrentUserId, string? fileName);
        Task<int> DeleteProvider(int ProviderId);
        Task<int> ActiveInActiveProvider(int ProviderId);
    }
}
