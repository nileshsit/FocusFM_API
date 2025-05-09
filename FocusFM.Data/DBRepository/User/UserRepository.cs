
using System.Data;
using Dapper;
using FocusFM.Common.Helpers;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Config;
using FocusFM.Model.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FocusFM.Data.DBRepository.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public UserRepository
        (
            IConfiguration config, 
            IOptions<DataConfig> dataConfig
        ) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Methods
        public async Task<int> SaveUser(UserRequestModel model, long id, string password, string passSalt)
        {
            var param = new DynamicParameters();
            if (model.UserId == 0)
            {
                param.Add("@Password", password);
                param.Add("@PassSalt", passSalt);
            }
            else
            {
                param.Add("@Password", null);
                param.Add("@PassSalt", null); ;
            }
            param.Add("@UserId", model.UserId);
            param.Add("@FirstName", model.FirstName);
            param.Add("@LastName", model.LastName);
            param.Add("@EmailId", model.EmailId);
            param.Add("@MobileNo", model.MobileNo);
            param.Add("@ReceiveDocEmail", model.ReceiveDocEmail);
            param.Add("@CreatedBy", id);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.SaveUser, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<UserResponseModel>> GetUserListAdmin(CommonPaginationModel model)
        {
            var param = new DynamicParameters();
            param.Add("@pageIndex", model.PageNumber);
            param.Add("@pageSize", model.PageSize);
            param.Add("@orderBy", model.SortColumn);
            param.Add("@sortOrder", model.SortOrder);
            param.Add("@strSearch", model.StrSearch);
            var data = await QueryAsync<UserResponseModel>(StoredProcedures.GetUserList, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<UserResponseModel>> GetUserById(long UserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            var data = await QueryAsync<UserResponseModel>(StoredProcedures.GetUserById, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<string> GetUserByReceiveDocEmail()
        {
            var data = await QueryFirstOrDefaultAsync<string>(StoredProcedures.GetUserByReceiveDocEmail, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> DeleteUser(long UserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.DeleteUser, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> InActiveUser(long UserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.InActiveUser, param, commandType: CommandType.StoredProcedure);
            return result;
        } 
        #endregion
    }
}