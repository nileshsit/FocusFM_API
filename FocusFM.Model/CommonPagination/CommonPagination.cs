using System.ComponentModel.DataAnnotations;

namespace FocusFM.Model.CommonPagination
{
    public class CommonPaginationModel
    {
        [Required(ErrorMessage = "Page Number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Page Size must be Greater than Zero.")]
        public long? PageNumber { get; set; } 
        [Required(ErrorMessage = "Pagesize is required")]
        [Range(1,int.MaxValue, ErrorMessage = "Page Number must be Greater than Zero.")]
        public long? PageSize { get; set; } 
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? StrSearch { get; set; }
    }
}