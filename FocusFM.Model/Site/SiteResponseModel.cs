using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.Common;

namespace FocusFM.Model.Site
{
    public class SiteResponseModel:CommonResponseModel
    {
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public string ContactPersonName { get; set; }
        public string EmailId { get; set; }
        public string ContactPersonMobNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string ContactPersonEmailId { get; set; }
        public string SiteImage { get; set; }
    }
}
