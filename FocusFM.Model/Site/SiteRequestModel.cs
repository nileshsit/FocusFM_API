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
        public string SiteName { get; set; }
        [Required(ErrorMessage = "ContactPersonName is required.")]
        public string ContactPersonName { get; set; }
        [Required(ErrorMessage = "ContactPerson Email Id is required.")]
        public string ContactPersonEmailId { get; set; }

        [Required(ErrorMessage = "Email Id is required.")]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "ContactPersonMobNo must contain only numbers.")]
        [StringLength(15, ErrorMessage = "ContactPersonMobNo must be at least 15 characters long.", MinimumLength = 10)]
        public string ContactPersonMobNo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "PinCode is required.")]
        [StringLength(6, ErrorMessage = "PinCode must be at least 6 characters long.", MinimumLength = 4)]
        public string PinCode { get; set; }
        public IFormFile? File { get; set; }
    }
}
