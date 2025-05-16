using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.CommonPagination;

namespace FocusFM.Model.Site.Meter
{
    public class GetMeterListRequestModel:CommonPaginationModel
    {
        public int MeterTypeId { get; set; }
        public long SiteId { get; set; }
    }
}
