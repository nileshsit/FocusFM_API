namespace FocusFM.Model.Token
{
    public class UserTokenModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public DateTime TokenValidTo { get; set; }
        public long AdminRoleId { get; set; }

    }

    public class TokenModel
    {
        public long UserId { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public string CurrencySign { get; set; }
        public DateTime TokenValidTo { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}