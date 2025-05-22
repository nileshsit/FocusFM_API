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
        public const string UserStatusActiveInActive = "SP_CheckUserActiveOrNot";
        public const string UpdateProfile = "SP_UpdateProfile";
        #endregion

        #region AdminUser
        public const string SaveAdminUser = "SP_SaveUser";
        public const string GetAdminUserList = "SP_GetUserList";
        public const string InActiveAdminUser = "SP_InActiveUser";
        public const string DeleteAdminUser = "SP_DeleteUser";
        public const string GetAdminUserById = "SP_GetUser_ById";
        public const string GetAdminUserByReceiveDocEmail = "SP_GetUserByReceiveDocEmail";
        #endregion

        #region MeterOccupier
        public const string SaveMeterOccupier = "SP_SaveMeterOccupier";
        public const string GetMeterOccupierList = "SP_GetMeterOccupierList";
        public const string GetMeterUsingId = "SP_GetMeterListById";
        public const string GetMeterOccupierTyeDropdown = "SP_GetMeterOccupierTypeDropdown";
        public const string ActiveInActiveMeterOccupier = "SP_ActiveInActiveMeterOccupier";
        public const string DeleteMeterOccupier = "SP_DeleteMeterOccupier";
        public const string GetMeterOccupierDropdown = "SP_GetMeterOccupierDropdown";
        public const string GetMeterOccupierExportData = "SP_GetMeterOccupierExportList";
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
        public const string SaveMeter = "SP_SaveMeter";
        public const string GetMeterList = "SP_GetMeterList";
        public const string ActiveInActiveMeter = "SP_ActiveInActiveMeter";
        public const string DeleteMeter = "SP_DeleteMeter";
        public const string GetMeterExportData = "SP_GetMeterExportList";
        public const string GetMeterDropdown = "SP_GetMeterDropdown";
        public const string GetMeterBulkImport = "SP_MeterBulkImport";

        #endregion


    }
}