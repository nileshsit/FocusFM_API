using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.Common;

namespace FocusFM.Model.User
{
    public class UserExportRequestModel:CommonExportRequestModel
    {
        public string UserTypeIds { get; set; }
    }
}
