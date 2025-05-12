using FocusFM.Data.DBRepository.AdminUser;
using FocusFM.Model.AdminUser;
using FocusFM.Model.CommonPagination;

namespace FocusFM.Service.AdminUser
{
    public class AdminUserService : IAdminUserService
    {
        #region Fields
        private readonly IAdminUserRepository _repository;
        #endregion

        #region Construtor
        public AdminUserService(IAdminUserRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public Task<int> SaveUser(AdminUserRequestModel model, long id, string password, string passSalt)
        {
            return _repository.SaveUser(model, id, password, passSalt);
        }

        public Task<List<AdminUserResponseModel>> GetUserListAdmin(CommonPaginationModel model)
        {
            return _repository.GetUserListAdmin(model);
        }

        public Task<List<AdminUserResponseModel>> GetUserById(long UserId)
        {
            return _repository.GetUserById(UserId);
        }

        public Task<string> GetUserByReceiveDocEmail()
        {
            return _repository.GetUserByReceiveDocEmail();
        }

        public Task<int> DeleteUser(long UserId)
        {
            return _repository.DeleteUser(UserId);
        }

        public Task<int> InActiveUser(long UserId)
        {
            return _repository.InActiveUser(UserId);
        }
        #endregion
    }
}