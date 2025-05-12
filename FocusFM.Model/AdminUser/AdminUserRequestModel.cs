using System.ComponentModel.DataAnnotations;

namespace FocusFM.Model.AdminUser
{
    public class AdminUserRequestModel
    {
        public long UserId { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Id is required.")]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "MobileNo must contain only numbers.")]
        [StringLength(15, ErrorMessage = "MobileNo must be at least 15 characters long.", MinimumLength = 10)]
        public string MobileNo { get; set; }
        public bool ReceiveDocEmail { get; set; }
    }
}