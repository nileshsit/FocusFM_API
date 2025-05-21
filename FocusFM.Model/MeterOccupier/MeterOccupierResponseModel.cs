using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.Common;

namespace FocusFM.Model.MeterOccupier
{
    public class MeterOccupierResponseModel: CommonResponseModel
    {
        public long MeterOccupierId { get; set; }
        public int MeterOccupierTypeId { get; set; }
        public string MeterOccupierType { get; set; }
        public string FirstName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string Location { get; set; }

    }
}
