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
        public const string ForgetPasswordSuccess = "Verification code sent successfully. Please check your inbox.";
        public const string ForgetPasswordError = "An error occurred while sending the email! Please try again.";
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
        public const string UpdateUserSuccess = "User updated successfully.";
        public const string UserInactive = "User inactivated successfully.";
        public const string UserActive = "User activated successfully.";
        public const string DeleteUserSuccess = "User deleted successfully.";

        // Domain
        public const string SaveDomainSuccess = "Domain saved successfully.";
        public const string UpdateDomainSuccess = "Domain updated successfully.";
        public const string DomainExists = "A domain with this name already exists.";
        public const string DeleteDomainSuccess = "Domain deleted successfully.";
        public const string DomainInUse = "Domain is in use.";

        // MainParentItem
        public const string SaveMainParentItemSuccess = "Main parent item saved successfully.";
        public const string UpdateMainParentItemSuccess = "Main parent item updated successfully.";
        public const string MainParentItemExists = "The main parent item with this name already exists.";
        public const string DeleteMainParentItemSuccess = "Main parent item deleted successfully.";
        public const string MainParentItemInUse = "Main parent item is in use.";

        // MainChildItem
        public const string SaveMainChildItemSuccess = "Main child item saved successfully.";
        public const string UpdateMainChildItemSuccess = "Main child item updated successfully.";
        public const string MainChildItemExists = "The main child item with this name already exists.";
        public const string DeleteMainChildItemSuccess = "Main child item deleted successfully.";
        public const string MainChildItemInUse = "Main child item is in use.";

        // SubParentItem
        public const string SaveSubParentItemSuccess = "Sub parent item saved successfully.";
        public const string UpdateSubParentItemSuccess = "Sub parent item updated successfully.";
        public const string SubParentItemExists = "The sub parent item with this name already exists.";
        public const string DeleteSubParentItemSuccess = "Sub parent item deleted successfully.";
        public const string SubParentItemInUse = "Sub parent item is in use.";

        // SubChildItem
        public const string SaveSubChildItemSuccess = "Sub child item saved successfully.";
        public const string UpdateSubChildItemSuccess = "Sub child item updated successfully.";
        public const string SubChildItemExists = "The sub child item with this name already exists.";
        public const string DeleteSubChildItemSuccess = "Sub child item deleted successfully.";
        public const string SubChildItemInUse = "Sub child item is in use.";

        // Sales Management
        public const string SaveSalesSuccess = "Partner saved successfully.";
        public const string UpdateSalesSuccess = "Partner updated successfully.";
        public const string SalesInactive = "Partner inactivated successfully.";
        public const string SalesActive = "Partner activated successfully.";
        public const string DeleteSalesSuccess = "Partner deleted successfully.";
        public const string SaleRegisterSuccess = "This is a members-only portal. Our team will review the account, and you will be notified upon approval.";
        public const string AlreadyRegisteredButPending = "Email already registered, please contact admin for approval.";
        public const string RejectedSale = "Partner request rejected successfully.";
        public const string ApprovedSale = "Partner request approved successfully.";
        public const string PendingRequest = "Your request is pending. Please contact admin for login approval.";
        public const string FormSavedSuccess = "Request form saved successfully.";
        public const string FormUpdatedSuccess = "Request form updated successfully.";

        // Note
        public const string SaveNoteSuccess = "Note saved successfully.";
        public const string UpdateNoteSuccess = "Note updated successfully.";
        public const string DeleteNoteSuccess = "Note deleted successfully.";
        public const string AlreadyExists = "Note already exists.";

        // Currency
        public const string SaveCurrencySuccess = "Currency saved successfully.";
        public const string UpdateCurrencySuccess = "Currency updated successfully.";
        public const string CurrencyExists = "A currency with this name already exists.";
        public const string DeleteCurrencySuccess = "Currency deleted successfully.";
        public const string CurrencyInUse = "Currency is in use.";

        // Quotes
        public const string SaveQuotesSuccess = "Quotes saved successfully.";
        public const string UpdateQuotesSuccess = "Quotes updated successfully.";
        public const string QuotesExists = "A quote with this name already exists.";
        public const string DocumentCountGreaterThanTen = "A maximum of 10 documents are allowed to be uploaded, and each document must be less than 10 MB in size.";
        public const string DeleteQuotesSuccess = "Quotes deleted successfully.";
        public const string QuotesInUse = "Quotes are in use.";
        public const string QuotesDocumentDeleteSuccess = "Document deleted successfully.";
        public const string QuotesDocumentDeleteFail = "An error occurred while deleting the document.";
        public const string AgreementDocumentSaveSuccess = "Document saved successfully.";
        public const string AgreementDocumentDeleteSuccess = "Document deleted successfully.";

        // Form Builder
        public const string SaveFormStepSuccess = "Form step saved successfully.";
        public const string UpdateFormStepSuccess = "Form step updated successfully.";
        public const string FormStepExists = "Form step already exists.";
        public const string SaveFormQuestionSuccess = "Form question saved successfully.";
        public const string UpdateFormQuestionSuccess = "Form question updated successfully.";
        public const string FormQuestionExists = "Form question already exists.";
        public const string SaveFormOptionSuccess = "Form option saved successfully.";
        public const string UpdateFormOptionSuccess = "Form option updated successfully.";
        public const string FormOptionExists = "Form option already exists.";
        public const string FormStepDeleteSuccess = "Form step and associated questions and options deleted successfully.";
        public const string FormQuestionDeleteSuccess = "Form question and associated options deleted successfully.";
        public const string FormOptionDeleteSuccess = "Form option deleted successfully.";
        public const string FormStepDeleteFail = "An error occurred while deleting the form step.";
        public const string FormQuestionDeleteFail = "An error occurred while deleting the form question.";
        public const string FormOptionDeleteFail = "An error occurred while deleting the form option.";
        public const string RequestFormDocumentUploadSuccess = "Document uploaded successfully.";
        public const string RequestFormDeleteSuccess = "Request form deleted successfully.";
        public const string RequestFormDeleteFail = "An error occurred while deleting the request form.";
        public const string CustomDevRequestDocumentSaveSuccess = "Document saved successfully.";
        public const string CustomDevRequestDocumentDeleteSuccess = "Document deleted successfully.";
    }
}