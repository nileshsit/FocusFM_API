using FocusFM.Model.CommonPagination;
using FocusFM.Model.User;

namespace FocusFM.Service.User
{
    public interface IUserService
    {
        Task<List<UserResponseModel>> GetUserList(CommonPaginationModel model);
        Task<List<UserTypeResponseModel>> GetUserTypeDropdown();
        Task<int> SaveUser(UserRequestModel model, long CurrentUserId);
        Task<int> DeleteUser(long UserId);
        Task<int> ActiveInActiveUser(long UserId);
    }
}
