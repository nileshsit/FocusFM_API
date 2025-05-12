using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.User
{
    public class UserResponseModel
    {
        public long UserId { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
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
