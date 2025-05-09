using FocusFM.Data.DBRepository.User;
using FocusFM.Model.CommonPagination;
using FocusFM.Model.User;

namespace FocusFM.Service.User
{
    public class UserService : IUserService
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
        public Task<int> SaveUser(UserRequestModel model, long id, string password, string passSalt)
        {
            return _repository.SaveUser(model, id, password, passSalt);
        }

        public Task<List<UserResponseModel>> GetUserListAdmin(CommonPaginationModel model)
        {
            return _repository.GetUserListAdmin(model);
        }

        public Task<List<UserResponseModel>> GetUserById(long UserId)
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