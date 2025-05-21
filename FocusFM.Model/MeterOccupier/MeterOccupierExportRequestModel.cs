using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.Common;

namespace FocusFM.Model.MeterOccupier
{
    public class MeterOccupierExportRequestModel:CommonExportRequestModel
    {
        public string MeterOccupierTypeIds { get; set; }
    }
}
