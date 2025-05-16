using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.Common;

namespace FocusFM.Model.Site.Meter
{
    public class MeterResponseModel:CommonResponseModel
    {
        public long? MeterId { get; set; }
        public string MeterName { get; set; }
        public string MeterSerialNo { get; set; }
        public int MeterTypeId { get; set; }
        public string MeterType { get; set; }
        public int MeterReadingTypeId { get; set; }
        public string MeterReadingType { get; set; }
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public long FloorId { get; set; }
        public string FloorName { get; set; }
        public long LandlordId { get; set; }
        public string LandlordName { get; set; }
        public long? TenantId { get; set; }
        public string TenantName { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
    }
}
