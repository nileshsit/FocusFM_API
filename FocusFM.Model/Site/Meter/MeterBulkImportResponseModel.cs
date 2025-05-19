using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.Site.Meter
{
    public class MeterBulkImportResponseModel:MeterBulkImportRequestModel
    {
        public string ErrorMessage { get; set; }
    }
}
