

using FocusFM.Model.AdminUser;
using FocusFM.Model.CommonPagination;

namespace FocusFM.Service.AdminUser
{
    public interface IAdminUserService
    {
        Task<int> SaveUser(AdminUserRequestModel model,long id, string password, string passSalt,string? ProfileImage);
        Task<List<AdminUserResponseModel>> GetUserListAdmin(CommonPaginationModel model);
        Task<List<AdminUserResponseModel>> GetUserById(long UserId);
        Task<string> GetUserByReceiveDocEmail();
        Task<int> DeleteUser(long UserId, long CurrentUserId);
        Task<int> InActiveUser(long UserId, long CurrentUserId);      
    }
}