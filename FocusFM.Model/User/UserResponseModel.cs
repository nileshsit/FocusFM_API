namespace FocusFM.Model.User
{
    public class UserResponseModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public bool ReceiveDocEmail { get; set; }
        public int TotalRecords { get; set; }
        public int RowNumber { get; set; }
        public bool isActive { get; set; }
    }
}