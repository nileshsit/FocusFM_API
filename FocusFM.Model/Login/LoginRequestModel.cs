using System.ComponentModel.DataAnnotations;

namespace FocusFM.Model.Login
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email id required!")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password required!")]
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
    }

    public class SaltResponseModel
    {
        public long AdminUserId { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ForgetPasswordRequestModel
    {
        [Required(ErrorMessage = "EmailId is required")]
        public string? EmailId { get; set; }
    }

    public class VerificationOTPRequestModel
    {
        [Required(ErrorMessage = "Email id required!")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Verification-code is required!")]
        public int OTP { get; set; }

    }
}