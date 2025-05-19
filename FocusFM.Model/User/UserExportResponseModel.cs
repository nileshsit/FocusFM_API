using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.User
{
    public class UserExportResponseModel
    {
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Location { get; set; }
    }
}
