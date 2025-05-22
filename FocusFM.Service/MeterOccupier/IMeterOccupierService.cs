using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.MeterOccupier;

namespace FocusFM.Service.MeterOccupier
{
    public interface IMeterOccupierService
    {
        Task<List<MeterOccupierResponseModel>> GetMeterOccupierList(GetMeterOccupierListRequestModel model);
        Task<List<GetMeterUsingIdResponseModel>> GetMeterUsingId(GetMeterUsingIdRequestModel model);
        Task<List<MeterOccupierTypeResponseModel>> GetMeterOccupierTypeDropdown();
        Task<int> SaveMeterOccupier(MeterOccupierRequestModel model, long CurrentMeterOccupierId);
        Task<int> DeleteMeterOccupier(long MeterOccupierId, long CurrentMeterOccupierId);
        Task<int> ActiveInActiveMeterOccupier(long MeterOccupierId, long CurrentMeterOccupierId);
        Task<List<MeterOccupierDropdownResponseModel>> GetMeterOccupierDropdown(int MeterOccupierTypeId);          // 1=Tenant,2=Landlord
        Task<List<MeterOccupierExportResponseModel>> GetMeterOccupierExportData(MeterOccupierExportRequestModel model);
    }
}
