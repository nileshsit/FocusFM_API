using Dapper;
using FocusFM.Common.Helpers;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.Config;
using FocusFM.Model.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;

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
        public async Task<List<UserTypeResponseModel>> GetUserTypeDropdown()
        {
            var data = await QueryAsync<UserTypeResponseModel>(StoredProcedures.GetUserTyeDropdown, null, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<int> SaveUser(UserRequestModel model, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", model.UserId);
            param.Add("@UserTypeId", model.UserTypeId);
            param.Add("@FirstName", model.FirstName);
            param.Add("@PinCode", model.PinCode);
            param.Add("@EmailId", model.EmailId);
            param.Add("@MobileNo", model.MobileNo);
            param.Add("@Address", model.Address);
            param.Add("@City", model.City);
            param.Add("@Country", model.Country);
            param.Add("@CreatedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.SaveUser, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<UserResponseModel>> GetUserList(CommonPaginationModel model)
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
        public async Task<int> DeleteUser(long UserId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.DeleteUser, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ActiveInActiveUser(long UserId, long CurrentUserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@ModifiedBy", CurrentUserId);
            var result = await QueryFirstOrDefaultAsync<int>(StoredProcedures.ActiveInActiveUser, param, commandType: CommandType.StoredProcedure);
            return result;
        }
        #endregion
    }
}
