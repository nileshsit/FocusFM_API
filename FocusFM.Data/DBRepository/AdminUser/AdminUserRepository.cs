
using Dapper;
using FocusFM.Common.Helpers;
using FocusFM.Model.AdminUser;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;

namespace FocusFM.Data.DBRepository.AdminUser
{
    public class AdminUserRepository : BaseRepository, IAdminUserRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public AdminUserRepository
        (
            IConfiguration config,
            IOptions<DataConfig> dataConfig
        ) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Methods
        public async Task<int> SaveUser(AdminUserRequestModel model, long id, string password, string passSalt, string? ProfileImage)
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
            param.Add("@Address", model.Address);
            param.Add("@City", model.City);
            param.Add("@Country", model.Country);
            param.Add("@PinCode", model.PinCode);
            param.Add("@Photo", ProfileImage);
            param.Add("@CreatedBy", id);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.SaveAdminUser, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<AdminUserResponseModel>> GetUserListAdmin(CommonPaginationModel model)
        {
            var param = new DynamicParameters();
            param.Add("@pageIndex", model.PageNumber);
            param.Add("@pageSize", model.PageSize);
            param.Add("@orderBy", model.SortColumn);
            param.Add("@sortOrder", model.SortOrder);
            param.Add("@strSearch", model.StrSearch);
            var data = await QueryAsync<AdminUserResponseModel>(StoredProcedures.GetAdminUserList, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<AdminUserResponseModel>> GetUserById(long UserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            var data = await QueryAsync<AdminUserResponseModel>(StoredProcedures.GetAdminUserById, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<string> GetUserByReceiveDocEmail()
        {
            var data = await QueryFirstOrDefaultAsync<string>(StoredProcedures.GetAdminUserByReceiveDocEmail, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> DeleteUser(long UserId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.DeleteAdminUser, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> InActiveUser(long UserId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.InActiveAdminUser, param, commandType: CommandType.StoredProcedure);
            return result;
        }
        #endregion
    }
}