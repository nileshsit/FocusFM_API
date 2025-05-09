namespace FocusFM.Model.Settings
{
    public class AppSettings
    {
        public string? JWT_Secret { get; set; }
        public int JWT_Validity_Mins { get; set; }
        public int PasswordLinkValidityMins { get; set; }
        public string? MailChimpApiKey { get; set; }
        public string? MailChimpbaseURL { get; set; }
        public string ErrorEmail { get; set; }
        public string ErrorSendToEmail { get; set; }
        public int ForgotPasswordAttemptValidityHours { get; set; }
        public string Logo { get; set; }
        public string NonBrandLogo { get; set; }
        public string URL { get; set; }
    }
}