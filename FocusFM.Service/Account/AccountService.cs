

using FocusFM.Data.DBRepository.Account;
using FocusFM.Model.Login;
using FocusFM.Model.Profile;

namespace FocusFM.Service.Account
{
    public class AccountService : IAccountService
    {
        #region Fields
        private readonly IAccountRepository _repository;
        #endregion

        #region Construtor
        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public async Task<LoginResponseModel> LoginUser(LoginRequestModel model)
        {
            return await _repository.LoginUser(model);
        }

        public async Task<long> UpdateLoginToken(string Token, long UserId, bool IsAdmin)
        {
            return await _repository.UpdateLoginToken(Token, UserId, IsAdmin);
        }

        public async Task<SaltResponseModel> GetUserSalt(string EmailId, bool IsAdmin)
        {
            return await _repository.GetUserSalt(EmailId, IsAdmin);
        }

        public async Task<long> LogoutUser(long UserId, bool IsAdmin)
        {
            return await _repository.LogoutUser(UserId, IsAdmin);
        }
        public async Task<ForgetPasswordResponseModel> ForgetPassword(string EmailId, bool IsAdmin)
        {
            return await _repository.ForgetPassword(EmailId, IsAdmin);
        }

        public async Task<int> SaveOTP(long UserId, int randomNumer, string EmailId, bool IsAdmin)
        {
            return await _repository.SaveOTP(UserId, randomNumer, EmailId, IsAdmin);
        }

        public async Task<long> GetUserIDByEmail(string EmailId, bool IsAdmin)
        {
            return await _repository.GetUserIDByEmail(EmailId, IsAdmin);
        }

        public async Task<string> VerificationCode(long UserId, int OTP, int PasswrodValid, bool IsAdmin)
        {
            return await _repository.VerificationCode(UserId, OTP, PasswrodValid, IsAdmin);
        }

        public async Task<string> ResetPassword(long UserId, string EmailId, string Password, string Salt, bool IsAdmin)
        {
            return await _repository.ResetPassword(UserId, EmailId, Password, Salt, IsAdmin);
        }

        public async Task<long> ValidateUserTokenData(long UserId, string jwtToken, DateTime TokenValidDate, bool IsAdmin)
        {
            return await _repository.ValidateUserTokenData(UserId, jwtToken, TokenValidDate, IsAdmin);
        }

        public async Task<string> ChangePassword(long UserId, string Password, string Salt, bool IsAdmin)
        {
            return await _repository.ChangePassword(UserId, Password, Salt, IsAdmin);
        }

        public async Task<int> UserActiveInActive(string EmailId, bool IsAdmin)
        {
            return await _repository.ActiveInActive(EmailId, IsAdmin);
        }

        public async Task<long> UpdateProfile(ProfileRequestModel model)
        {
            return await _repository.UpdateProfile(model);
        } 
        #endregion
    }
}