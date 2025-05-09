namespace FocusFM.Common.Helpers
{
    public class StoredProcedures
    {
        #region Login
        public const string LoginUser = "SP_LoginUser";
        public const string GetUserSaltByEmail = "SP_UserGetSaltByEmail";
        public const string UpdateLoginToken = "SP_UpdateLoginToken";
        public const string LogoutUser = "SP_LogoutUser";
        public const string ForgetPassword = "SP_UserForgetPassword";
        public const string SaveOTP = "SP_EmailOTPAdd";
        public const string GetUserIdByEmail = "SP_GetUserIDByEmail";
        public const string VerifyOTP = "SP_EmailOTPVerify";
        public const string ResetPassword = "SP_UpdatePassword";
        public const string ChangePassword = "SP_ChangePassword";
        public const string ValidateToken = "SP_ValidateToken";
        public const string UserStatusActiveInActive = "SP_UserActiveInActive";
        public const string UpdateProfile = "SP_UpdateProfile";
        #endregion

        #region User
        public const string SaveUser = "SP_SaveUser";
        public const string GetUserList = "SP_GetUserListAdmin";
        public const string InActiveUser = "SP_InActiveUser";
        public const string DeleteUser = "SP_DeleteUser";
        public const string GetUserById = "SP_GetUser_ById";
        public const string GetUserByReceiveDocEmail = "SP_GetUserByReceiveDocEmail";
        #endregion

       
    }
}