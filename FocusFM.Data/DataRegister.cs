using FocusFM.Data.DBRepository.Account;
using FocusFM.Data.DBRepository.User;

namespace FocusFM.Data
{
    public static class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dataDictionary = new Dictionary<Type, Type>
            {
                {typeof(IUserRepository),typeof(UserRepository) },
                {typeof(IAccountRepository),typeof(AccountRepository) },
            };
            return dataDictionary;
        }
    }
}