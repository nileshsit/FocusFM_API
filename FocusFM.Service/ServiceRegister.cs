

using FocusFM.Service.Account;
using FocusFM.Service.AdminUser;
using FocusFM.Service.Providers;
using FocusFM.Service.Site;
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
                { typeof(IMeterOccupierService), typeof(MeterOccupierService) },
                { typeof(IProviderService), typeof(ProviderService) },
                { typeof(ISiteService), typeof(SiteService) },
            };
            return serviceDictonary;
        }
    }
}