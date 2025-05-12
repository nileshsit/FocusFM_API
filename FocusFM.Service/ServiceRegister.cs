

using FocusFM.Service.Account;
using FocusFM.Service.AdminUser;
using FocusFM.Service.Providers;
using FocusFM.Service.User;

namespace FocusFM.Service
{
    public static class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var serviceDictonary = new Dictionary<Type, Type>
            {
                { typeof(IAccountService), typeof(AccountService) },
                { typeof(IAdminUserService), typeof(AdminUserService) },
                { typeof(IUserService), typeof(UserService) },
                { typeof(IProviderService), typeof(ProviderService) },
            };
            return serviceDictonary;
        }
    }
}