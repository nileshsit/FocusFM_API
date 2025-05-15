namespace FocusFM.Common.Helpers
{
    public class ErrorMessages
    {
        // Common
        public const string InvalidSortOrder = "Invalid Sort Order: The specified sort order value exceeds the total number of records available.";
        public const string NoParametersPassed = "No parameters passed.";

        // Login
        public const string InvalidEmailId = "Invalid email address.";
        public const string InvalidCredential = "Invalid email ID or password.";
        public const string LoginSuccess = "Login successful.";
        public const string SomethingWentWrong = "Something went wrong! Please try again later.";
        public const string LogoutSuccess = "Logout successful.";
        public const string EmailIsRequired = "Email ID is required.";
        public const string EnterValidEmail = "Please enter a valid email.";
        public const string UpdateUser = "User profile updated successfully.";
        public const string EmailExists = "Email already exists.";
        public const string InvalidId = "ID must be greater than zero.";
        public const string UserInActive = "Your user ID is inactivated.";
        public const string UpdateProfileSuccess = "Profile updated successfully.";

        // ForgetPassword
        public const string MailSuccess = "Verification code sent successfully. Please check your inbox.";
        public const string MailError = "An error occurred while sending the email! Please try again.";
        public const string UserError = "You're not registered with us.";

        // ValidateResetPassword
        public const string ValidateResetPasswordSuccess = "User is valid.";
        public const string URLExpired = "This URL has expired... Please try again.";

        // VerifyOTP
        public const string VerifyCode = "Verification code verified successfully.";
        public const string WrongCode = "Verification code does not match.";

        // ResetPassword
        public const string ResetPasswordSuccess = "Password changed successfully.";
        public const string ConfirmPassword = "New password and confirmation password do not match.";
        public const string PasswordValidationConfirm = "Confirm password is required.";
        public const string PasswordValidation = "Create password is required.";
        public const string StrongPassword = "Password must be at least 6 characters long and contain a mixture of numbers, special characters, uppercase letters, lowercase letters, and no spaces.";
        public const string PasswordMatch = "New password can't be the same as the old password.";
        public const string PasswordCheck = "Please enter a valid old password.";
        public const string PasswordFieldValidation = "One or more fields are required.";

        // User Management
        public const string SaveUserSuccess = "User saved successfully.";
        public const string UserExists = "User already exists.";
        public const string UpdateUserSuccess = "User updated successfully.";
        public const string UserInactive = "User inactivated successfully.";
        public const string UserActive = "User activated successfully.";
        public const string DeleteUserSuccess = "User deleted successfully.";

        // Providers 
        public const string SaveProviderSuccess = "Provider saved successfully.";
        public const string UpdateProviderSuccess = "Provider updated successfully.";
        public const string ProviderInactive = "Provider inactivated successfully.";
        public const string ProviderActive = "Provider activated successfully.";
        public const string DeleteProviderSuccess = "Provider deleted successfully.";
        public const string NoFile = "No file uploaded.";
        public const string InvalidImageFile = "Only .PNG and .JPG files allowed.";
        public const string InvalidExcelFile = "Only .xls and .xlsx files allowed.";
        public const string FileSizeExceeds = "File Size Exceeds.";
        public const string ProviderExists = "Provider already exists.";

        // Sites 
        public const string SaveSiteSuccess = "Site saved successfully.";
        public const string UpdateSiteSuccess = "Site updated successfully.";
        public const string SiteInactive = "Site inactivated successfully.";
        public const string SiteActive = "Site activated successfully.";
        public const string DeleteSiteSuccess = "Site deleted successfully.";
        public const string SiteExists = "Site already exists.";

        // Sites Floor
        public const string SaveFloorSuccess = "Floor saved successfully.";
        public const string UpdateFloorSuccess = "Floor updated successfully.";
        public const string FloorInactive = "Floor inactivated successfully.";
        public const string FloorActive = "Floor activated successfully.";
        public const string DeleteFloorSuccess = "Floor deleted successfully.";
        public const string FloorExists = "Floor already exists.";
    }
}