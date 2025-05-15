using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FocusFM.Model.Site
{
    public class SiteRequestModel
    {
        public long? SiteId { get; set; }
        [Required(ErrorMessage = "SiteName is required.")]
        [StringLength(100, ErrorMessage = "FirstName - Only 100 Character allowed.")]
        public string SiteName { get; set; }
        [Required(ErrorMessage = "Contact Person Name is required.")]
        [StringLength(100, ErrorMessage = "Contact Person Name - Only 100 Character allowed.")]
        public string ContactPersonName { get; set; }
        [Required(ErrorMessage = "Contact Person Email Id is required.")]
        [StringLength(100, ErrorMessage = "Contact Person Email Id - Only 100 Character allowed.")]
        public string ContactPersonEmailId { get; set; }

        [Required(ErrorMessage = "Email Id is required.")]
        [StringLength(100, ErrorMessage = "Email Id - Only 100 Character allowed.")]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Contact Person Mobile No must contain only numbers.")]
        [StringLength(15, ErrorMessage = "Contact Person MobileNo must be at least 15 characters long.", MinimumLength = 10)]
        public string ContactPersonMobNo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City - Only 50 Character allowed.")]
        public string City { get; set; }

        [Required(ErrorMessage = "PinCode is required.")]
        [StringLength(6, ErrorMessage = "PinCode must be at least 6 characters long.", MinimumLength = 4)]
        public string PinCode { get; set; }
        public IFormFile? File { get; set; }
    }
}
