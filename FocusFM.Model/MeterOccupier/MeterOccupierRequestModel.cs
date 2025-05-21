using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.MeterOccupier
{
    public class MeterOccupierRequestModel
    {
        public long? MeterOccupierId { get; set; }
        [Required(ErrorMessage = "MeterOccupierType is required.")]
        public int MeterOccupierTypeId { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(100, ErrorMessage = "FirstName - Only 100 Character allowed.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "EmailId is required.")]
        [StringLength(100, ErrorMessage = "EmailId - Only 100 Character allowed.")]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "MobileNo must contain only numbers.")]
        [StringLength(15, ErrorMessage = "MobileNo must be at least 15 characters long.", MinimumLength = 10)]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City - Only 50 Character allowed.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country - Only 100 Character allowed.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "PinCode is required.")]
        [StringLength(6, ErrorMessage = "PinCode must be at least 6 characters long.", MinimumLength = 4)]
        public string PinCode { get; set; }
    }
}
