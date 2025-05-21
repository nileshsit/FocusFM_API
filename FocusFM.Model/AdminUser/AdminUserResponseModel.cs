using System.ComponentModel.DataAnnotations;
using FocusFM.Model.Common;

namespace FocusFM.Model.AdminUser
{
    public class AdminUserResponseModel: CommonResponseModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
    }
}