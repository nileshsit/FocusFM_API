using Dapper;
using FocusFM.Common.Helpers;
using FocusFM.Model.Config;
using FocusFM.Model.Login;
using FocusFM.Model.Profile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;

namespace FocusFM.Data.DBRepository.Account
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public AccountRepository
        (
            IConfiguration config,
            IOptions<DataConfig> dataConfig
        ) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Methods
        public async Task<SaltResponseModel> GetUserSalt(string EmailId, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@EmailId", EmailId);
            param.Add("@IsAdmin", IsAdmin);
            return await QueryFirstOrDefaultAsync<SaltResponseModel>(StoredProcedures.GetUserSaltByEmail, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<LoginResponseModel> LoginUser(LoginRequestModel model)
        {
            var param = new DynamicParameters();
            param.Add("@EmailId", model.EmailId);
            param.Add("@Password", model.Password);
            param.Add("@IsAdmin", model.IsAdmin);
            return await QueryFirstOrDefaultAsync<LoginResponseModel>(StoredProcedures.LoginUser, param, commandType: CommandType.StoredProcedure);
        }
        public async Task<long> UpdateLoginToken(string Token, long UserId, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@Token", Token);
            param.Add("@UserId", UserId);
            param.Add("@IsAdmin", IsAdmin);
            return await QueryFirstOrDefaultAsync<long>(StoredProcedures.UpdateLoginToken, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<long> LogoutUser(long UserId, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@IsAdmin", IsAdmin);
            return await QueryFirstOrDefaultAsync<long>(StoredProcedures.LogoutUser, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<ForgetPasswordResponseModel> ForgetPassword(string EmailId, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@EmailId", EmailId);
            param.Add("@IsAdmin", IsAdmin);
            return await QueryFirstOrDefaultAsync<ForgetPasswordResponseModel>(StoredProcedures.ForgetPassword, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> SaveOTP(long UserId, int randomNumer, string EmailId, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@OTP", randomNumer);
            param.Add("@EmailId", EmailId);
            param.Add("@IsAdmin", IsAdmin);
            return await ExecuteAsync<int>(StoredProcedures.SaveOTP, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<long> GetUserIDByEmail(string EmailId, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@EmailId", EmailId);
            param.Add("@IsAdmin", IsAdmin);
            return await QueryFirstOrDefaultAsync<long>(StoredProcedures.GetUserIdByEmail, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<string> VerificationCode(long UserId, int OTP, int PasswrodValid, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@OTP", OTP);
            param.Add("@Minute", PasswrodValid);
            param.Add("@IsAdmin", IsAdmin);
            return await QueryFirstOrDefaultAsync<string>(StoredProcedures.VerifyOTP, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<string> ResetPassword(long UserId, string EmailId, string Password, string Salt, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@EmailId", EmailId);
            param.Add("@Password", Password);
            param.Add("@Salt", Salt);
            param.Add("@IsAdmin", IsAdmin);
            return await QueryFirstOrDefaultAsync<string>(StoredProcedures.ResetPassword, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<long> ValidateUserTokenData(long UserId, string jwtToken, DateTime TokenValidDate, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@jwtToken", jwtToken);
            param.Add("@TokenValidDate", TokenValidDate);
            param.Add("@IsAdmin", IsAdmin);
            return await QueryFirstOrDefaultAsync<long>(StoredProcedures.ValidateToken, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<string> ChangePassword(long UserId, string Password, string Salt, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@Password", Password);
            param.Add("@Salt", Salt);
            param.Add("@IsAdmin", IsAdmin);
            return await QueryFirstOrDefaultAsync<string>(StoredProcedures.ChangePassword, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> ActiveInActive(string EmailId, bool IsAdmin)
        {
            var param = new DynamicParameters();
            param.Add("@EmailId", EmailId);
            param.Add("@IsAdmin", IsAdmin);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.UserStatusActiveInActive, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<long> UpdateProfile(ProfileRequestModel model)
        {
            var param = new DynamicParameters();
            param.Add("@SaleId", model.SaleId);
            param.Add("@UserEmail", model.UserEmail);
            param.Add("@FullName", model.FullName);
            param.Add("@MobileNo", model.MobileNo);
            param.Add("@CompanyName", model.CompanyName);
            param.Add("@CurrencyId", model.CurrencyId);
            param.Add("@MarginInPer", model.MarginInPer);
            return await QueryFirstOrDefaultAsync<long>(StoredProcedures.UpdateProfile, param, commandType: CommandType.StoredProcedure);
        } 
        #endregion
    }
}