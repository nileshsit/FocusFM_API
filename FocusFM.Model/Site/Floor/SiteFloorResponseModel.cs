using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.Common;

namespace FocusFM.Model.Site.Floor
{
    public class SiteFloorResponseModel:CommonResponseModel
    {
        public long? FloorId { get; set; }
        public long SiteId { get; set; }
        public string FloorName { get; set; }
        public string SiteName { get; set; }
    }
}
