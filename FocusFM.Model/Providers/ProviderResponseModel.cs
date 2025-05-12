using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.Common;

namespace FocusFM.Model.Providers
{
    public class ProviderResponseModel: CommonResponseModel
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string ProviderTemplate { get; set; }
    }
}
