namespace FocusFM.Model.Profile
{
    public class ProfileRequestModel
    {
        public long? SaleId { get; set; }
        public string? UserEmail { get; set; }
        public string? FullName { get; set; }
        public string? MobileNo { get; set; }
        public string? CompanyName { get; set; }
        public long CurrencyId { get; set; }
        public decimal MarginInPer { get; set; }
    }
}