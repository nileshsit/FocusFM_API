using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.CommonPagination;

namespace FocusFM.Model.MeterOccupier
{
    public class GetMeterOccupierListRequestModel:CommonPaginationModel
    {
        public string MeterOccupierTypeIds { get; set; }
    }
}
