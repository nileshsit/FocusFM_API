namespace FocusFM.Common.Helpers
{
    public class StoredProcedures
    {
        #region Login
        public const string LoginUser = "SP_LoginUser";
        public const string GetUserSaltByEmail = "SP_AdminUserGetSaltByEmail";
        public const string UpdateLoginToken = "SP_UpdateLoginToken";
        public const string LogoutUser = "SP_LogoutUser";
        public const string ForgetPassword = "SP_AdminUserForgetPassword";
        public const string SaveOTP = "SP_EmailOTPAdd";
        public const string GetUserIdByEmail = "SP_GetAdminUserIDByEmail";
        public const string VerifyOTP = "SP_EmailOTPVerify";
        public const string ResetPassword = "SP_UpdatePassword";
        public const string ChangePassword = "SP_ChangePassword";
        public const string ValidateToken = "SP_ValidateToken";
        public const string UserStatusActiveInActive = "SP_AdminUserActiveInActive";
        public const string UpdateProfile = "SP_UpdateProfile";
        #endregion

        #region AdminUser
        public const string SaveAdminUser = "SP_SaveAdminUser";
        public const string GetAdminUserList = "SP_GetUserListAdmin";
        public const string InActiveAdminUser = "SP_InActiveAdminUser";
        public const string DeleteAdminUser = "SP_DeleteAdminUser";
        public const string GetAdminUserById = "SP_GetAdminUser_ById";
        public const string GetAdminUserByReceiveDocEmail = "SP_GetAdminUserByReceiveDocEmail";
        #endregion

        #region User
        public const string SaveUser = "SP_SaveUser";
        public const string GetUserList = "SP_GetUserList";
        public const string GetUserTyeDropdown = "SP_GetUserTypeDropdown";
        public const string ActiveInActiveUser = "SP_ActiveInActiveUser";
        public const string DeleteUser = "SP_DeleteUser";
        public const string GetUserDropdown = "SP_GetUserDropdown";
        #endregion

        #region Provider
        public const string SaveProvider = "SP_SaveProvider";
        public const string GetProviderList = "SP_GetProviderList";
        public const string ActiveInActiveProvider = "SP_ActiveInActiveProvider";
        public const string DeleteProvider = "SP_DeleteProvider";
        public const string GetProviderDropdown = "SP_GetProviderDropdown";
        #endregion

        #region Site
        public const string SaveSite = "SP_SaveSite";
        public const string GetSiteList = "SP_GetSiteList";
        public const string ActiveInActiveSite = "SP_ActiveInActiveSite";
        public const string DeleteSite = "SP_DeleteSite";
        #endregion

        #region Site Floor
        public const string SaveFloor = "SP_SaveFloor";
        public const string GetFloorList = "SP_GetFloorList";
        public const string ActiveInActiveFloor = "SP_ActiveInActiveFloor";
        public const string DeleteFloor = "SP_DeleteFloor";
        public const string GetFloorDropdown = "SP_GetFloorDropdown";
        #endregion

        #region Meter
        public const string GetMeterTypeDropdown = "SP_GetMeterTypeDropdown";
        public const string GetMeterReadingTypeDropdown = "SP_GetMeterReadingTypeDropdown";

        #endregion


    }
}