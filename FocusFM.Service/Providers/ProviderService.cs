using FocusFM.Data.DBRepository.Providers;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Providers;

namespace FocusFM.Service.Providers
{
    public class ProviderService : IProviderService
    {
        #region Fields
        private readonly IProviderRepository _repository;
        #endregion

        #region Construtor
        public ProviderService(IProviderRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods

        public Task<int> SaveProvider(ProviderRequestModel model, long id, string? fileName)
        {
            return _repository.SaveProvider(model, id, fileName);
        }

        public Task<List<ProviderResponseModel>> GetProviderList(CommonPaginationModel model)
        {
            return _repository.GetProviderList(model);
        }

        public Task<int> DeleteProvider(int ProviderId)
        {
            return _repository.DeleteProvider(ProviderId);
        }

        public Task<int> ActiveInActiveProvider(int ProviderId)
        {
            return _repository.ActiveInActiveProvider(ProviderId);
        }
        #endregion
    }
}
