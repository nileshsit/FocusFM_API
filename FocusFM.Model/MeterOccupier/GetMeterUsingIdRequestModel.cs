using FocusFM.Model.CommonPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.MeterOccupier
{
    public class GetMeterUsingIdRequestModel : CommonPaginationModel
    {
        public long MeterOccupierId { get; set; }
        public long MeterOccupierTypeId { get; set; }
    }
}
