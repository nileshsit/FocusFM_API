
using FocusFM.Data.DBRepository.User;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.User;

namespace FocusFM.Service.User
{
    public class UserService:IUserService
    {
        #region Fields
        private readonly IUserRepository _repository;
        #endregion

        #region Construtor
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public Task<List<UserTypeResponseModel>> GetUserTypeDropdown()
        {
            return _repository.GetUserTypeDropdown();
        }

        public Task<int> SaveUser(UserRequestModel model, long id)
        {
            return _repository.SaveUser(model, id);
        }

        public Task<List<UserResponseModel>> GetUserList(GetUserListRequestModel model)
        {
            return _repository.GetUserList(model);
        }

        public Task<int> DeleteUser(long UserId, long CurrentUserId)
        {
            return _repository.DeleteUser(UserId,CurrentUserId);
        }

        public Task<int> ActiveInActiveUser(long UserId, long CurrentUserId)
        {
            return _repository.ActiveInActiveUser(UserId,CurrentUserId);
        }

        public Task<List<UserDropdownResponseModel>> GetUserDropdown(int TypeId)
        {
            return _repository.GetUserDropdown(TypeId);
        }
        #endregion
    }
}
