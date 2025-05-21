using FocusFM.Model.CommonPagination;
using FocusFM.Model.MeterOccupier;

namespace FocusFM.Data.DBRepository.MeterOccupier
{
    public interface IMeterOccupierRepository
    {
        Task<List<MeterOccupierTypeResponseModel>> GetMeterOccupierTypeDropdown();
        Task<List<MeterOccupierResponseModel>> GetMeterOccupierList(GetMeterOccupierListRequestModel model);
        Task<int> SaveMeterOccupier(MeterOccupierRequestModel model, long CurrentMeterOccupierId);
        Task<int> DeleteMeterOccupier(long MeterOccupierId, long CurrentMeterOccupierId);
        Task<int> ActiveInActiveMeterOccupier(long MeterOccupierId, long CurrentMeterOccupierId);
        Task<List<MeterOccupierDropdownResponseModel>> GetMeterOccupierDropdown(int TypeId);
        Task<List<MeterOccupierExportResponseModel>> GetMeterOccupierExportData(MeterOccupierExportRequestModel model);
    }
}