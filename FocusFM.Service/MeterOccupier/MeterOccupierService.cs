

using FocusFM.Data.DBRepository.MeterOccupier;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.MeterOccupier;

namespace FocusFM.Service.MeterOccupier
{
    public class MeterOccupierService:IMeterOccupierService
    {
        #region Fields
        private readonly IMeterOccupierRepository _repository;
        #endregion

        #region Construtor
        public MeterOccupierService(IMeterOccupierRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public Task<List<MeterOccupierTypeResponseModel>> GetMeterOccupierTypeDropdown()
        {
            return _repository.GetMeterOccupierTypeDropdown();
        }

        public Task<int> SaveMeterOccupier(MeterOccupierRequestModel model, long id)
        {
            return _repository.SaveMeterOccupier(model, id);
        }

        public Task<List<MeterOccupierResponseModel>> GetMeterOccupierList(GetMeterOccupierListRequestModel model)
        {
            return _repository.GetMeterOccupierList(model);
        }

        public Task<int> DeleteMeterOccupier(long MeterOccupierId, long CurrentMeterOccupierId)
        {
            return _repository.DeleteMeterOccupier(MeterOccupierId,CurrentMeterOccupierId);
        }

        public Task<int> ActiveInActiveMeterOccupier(long MeterOccupierId, long CurrentMeterOccupierId)
        {
            return _repository.ActiveInActiveMeterOccupier(MeterOccupierId,CurrentMeterOccupierId);
        }

        public Task<List<MeterOccupierDropdownResponseModel>> GetMeterOccupierDropdown(int TypeId)
        {
            return _repository.GetMeterOccupierDropdown(TypeId);
        }
        public Task<List<MeterOccupierExportResponseModel>> GetMeterOccupierExportData(MeterOccupierExportRequestModel model)
        {
            return _repository.GetMeterOccupierExportData(model);
        }
        #endregion
    }
}
