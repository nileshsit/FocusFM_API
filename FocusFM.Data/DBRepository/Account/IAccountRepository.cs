

using FocusFM.Model.Login;
using FocusFM.Model.Profile;

namespace FocusFM.Data.DBRepository.Account
{
    public interface IAccountRepository
    {
        Task<LoginResponseModel> LoginUser(LoginRequestModel model);
        Task<SaltResponseModel> GetUserSalt(string EmailId, bool IsAdmin);
        Task<long> UpdateLoginToken(string Token, long UserId, bool IsAdmin);
        Task<long> LogoutUser(long UserId, bool IsAdmin);
        Task<ForgetPasswordResponseModel> ForgetPassword(string EmailId, bool IsAdmin);
        Task<int> SaveOTP(long UserId, int randomNumer, string EmailId, bool IsAdmin);
        Task<long> GetUserIDByEmail(string EmailId, bool IsAdmin);
        Task<string> VerificationCode(long UserId, int OTP, int PasswrodValid, bool IsAdmin);
        Task<string> ResetPassword(long UserId, string EmailId, string Password, string Salt, bool IsAdmin);
        Task<long> ValidateUserTokenData(long UserId, string jwtToken, DateTime TokenValidDate, bool IsAdmin);
        Task<string> ChangePassword(long UserId, string Password, string Salt, bool IsAdmin);
        Task<int> ActiveInActive(string EmailId, bool IsAdmin);
        Task<long> UpdateProfile(ProfileRequestModel model);
    }
}