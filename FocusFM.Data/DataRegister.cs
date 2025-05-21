using FocusFM.Data.DBRepository.Account;
using FocusFM.Data.DBRepository.AdminUser;
using FocusFM.Data.DBRepository.MeterOccupier;
using FocusFM.Data.DBRepository.Providers;
using FocusFM.Data.DBRepository.Site;

namespace FocusFM.Data
{
    public static class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dataDictionary = new Dictionary<Type, Type>
            {
                {typeof(IAdminUserRepository),typeof(AdminUserRepository) },
                {typeof(IAccountRepository),typeof(AccountRepository) },
                {typeof(IMeterOccupierRepository),typeof(MeterOccupierRepository) },
                {typeof(IProviderRepository),typeof(ProviderRepository) },
                {typeof(ISiteRepository),typeof(SiteRepository) },
            };
            return dataDictionary;
        }
    }
}