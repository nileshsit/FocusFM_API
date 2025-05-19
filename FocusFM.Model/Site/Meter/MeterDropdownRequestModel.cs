using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.Site.Meter
{
    public class MeterDropdownRequestModel
    {
        public int ProviderId { get; set; }
        public int MeterTypeId { get; set; }
        public long SiteId { get; set; }
    }
}
