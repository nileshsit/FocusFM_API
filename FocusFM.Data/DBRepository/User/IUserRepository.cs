using FocusFM.Model.CommonPagination;
using FocusFM.Model.User;

namespace FocusFM.Data.DBRepository.User
{
    public interface IUserRepository
    {
        Task<List<UserTypeResponseModel>> GetUserTypeDropdown();
        Task<List<UserResponseModel>> GetUserList(CommonPaginationModel model);
        Task<int> SaveUser(UserRequestModel model, long CurrentUserId);
        Task<int> DeleteUser(long UserId);
        Task<int> ActiveInActiveUser(long UserId);
    }
}
