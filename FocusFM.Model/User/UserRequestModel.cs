using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.User
{
    public class UserRequestModel
    {
        public long? UserId { get; set; }
        [Required(ErrorMessage = "UserType is required.")]
        public int UserTypeId { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "MobileNo must contain only numbers.")]
        [StringLength(15, ErrorMessage = "MobileNo must be at least 15 characters long.", MinimumLength = 10)]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "PinCode is required.")]
        [StringLength(6, ErrorMessage = "PinCode must be at least 6 characters long.", MinimumLength = 4)]
        public string PinCode { get; set; }
    }
}
