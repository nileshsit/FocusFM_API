using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.Site.Meter
{
    public class MeterExportResponseModel
    {
        public string MeterName { get; set; }
        public string MeterSerialNo { get; set; }
        public string MeterType { get; set; }
        public string MeterReadingType { get; set; }
        public string SiteName { get; set; }
        public string ProviderName { get; set; }
        public string FloorName { get; set; }
        public string LandlordName { get; set; }
        public string TenantName { get; set; }
        public string UserType { get; set; }
    }
}
