using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FocusFM.Model.User
{
    public class UserModel
    {
		[Required(ErrorMessage = "First Name required.")]
		[MaxLength(100, ErrorMessage = "Maxlength is 100 characters.")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name required.")]
		[MaxLength(100, ErrorMessage = "Maxlength is 100 characters.")]
		public string LastName { get; set; }
        [Required(ErrorMessage = "MobileNo Name required.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "MobileNo must contain only numbers.")]
        [StringLength(10, ErrorMessage = "MobileNo must be at least 10 characters long.", MinimumLength = 10)]
        public string MobileNo { get; set; }     
        public string? Photo { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}