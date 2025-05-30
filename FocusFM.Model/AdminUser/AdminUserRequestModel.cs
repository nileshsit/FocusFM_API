﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FocusFM.Model.AdminUser
{
    public class AdminUserRequestModel
    {
        public long? UserId { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(100, ErrorMessage = "FirstName - Only 100 Character allowed.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(100, ErrorMessage = "LastName - Only 100 Character allowed.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Id is required.")]
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
        public IFormFile? File { get; set; }
    }
}