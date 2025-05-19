using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.Common;

namespace FocusFM.Model.Site.Meter
{
    public class MeterExportRequestModel:CommonExportRequestModel
    {
        public int MeterTypeId { get; set; }
        public long SiteId { get; set; }
    }
}
