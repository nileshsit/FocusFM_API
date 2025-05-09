

using FocusFM.Model.CommonPagination;
using FocusFM.Model.User;

namespace FocusFM.Data.DBRepository.User
{
    public interface IUserRepository
    {
        Task<int> SaveUser(UserRequestModel model, long id, string password, string passSalt);
        Task<List<UserResponseModel>> GetUserListAdmin(CommonPaginationModel model);
        Task<List<UserResponseModel>> GetUserById(long UserId);
        Task<string> GetUserByReceiveDocEmail();
        Task<int> DeleteUser(long UserId);
        Task<int> InActiveUser(long UserId);
    }
}