using FocusFM.Data.DBRepository.Account;
using FocusFM.Data.DBRepository.AdminUser;
using FocusFM.Data.DBRepository.User;

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
                {typeof(IUserRepository),typeof(UserRepository) },
            };
            return dataDictionary;
        }
    }
}