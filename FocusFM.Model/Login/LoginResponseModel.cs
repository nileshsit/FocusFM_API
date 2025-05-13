namespace FocusFM.Model.Login
{
    public class LoginResponseModel
    {
        public long AdminUserId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string JWTToken { get; set; }
        public bool IsFirstLogin { get; set; }
        public bool IsAdmin { get; set; }
        //public long CompanyId { get; set; }
        public string Photo { get; set; }
        //public decimal? MarginInPer { get; set; }
    }

    public class ForgetPasswordResponseModel
    {
        public long AdminUserId { get; set; }
        public DateTime LastForgetPasswordSend { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public bool? IsEaziBusinessPartner { get; set; }
    }

    public class ResetPasswordRequestModel
    {
        public string EmailId { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordRequestModel
    {
        public string OldPassword { get; set; }
        public string CreatePassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}