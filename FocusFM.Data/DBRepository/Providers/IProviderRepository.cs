using FocusFM.Model.CommonPagination;
using FocusFM.Model.Providers;

namespace FocusFM.Data.DBRepository.Providers
{
    public interface IProviderRepository
    {
        Task<List<ProviderResponseModel>> GetProviderList(CommonPaginationModel model);
        Task<int> SaveProvider(ProviderRequestModel model, long CurrentProviderId, string? fileName);
        Task<int> DeleteProvider(int ProviderId);
        Task<int> ActiveInActiveProvider(int ProviderId);
    }
}
