using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.Site.Meter
{
    public class MeterRequestModel
    {
        public long? MeterId { get; set; }
        [Required(ErrorMessage = "MeterName is required.")]
        [StringLength(100, ErrorMessage = "MeterName - Only 100 Character allowed.")]
        public string MeterName { get; set; }
        [Required(ErrorMessage = "MeterNumber is required.")]
        [StringLength(50, ErrorMessage = "MeterNumber - Only 50 Character allowed.")]
        public string MeterNumber { get; set; }
        [Required(ErrorMessage = "MeterTypeId is required.")]
        public int MeterTypeId { get; set; }
        [Required(ErrorMessage = "MeterReadingTypeId is required.")]
        public int MeterReadingTypeId { get; set; }
        [Required(ErrorMessage = "SiteId is required.")]
        public long SiteId { get; set; }
        [Required(ErrorMessage = "ProviderId is required.")]
        public int ProviderId { get; set; }
        [Required(ErrorMessage = "FloorId is required.")]
        public long FloorId { get; set; }
        [Required(ErrorMessage = "LandlordId is required.")]
        public long LandlordId { get; set; }
        [Required(ErrorMessage = "TenantId is required.")]
        public long? TenantId { get; set; }
        [Required(ErrorMessage = "MeterOccupierTypeId is required.")]
        public int MeterOccupierTypeId { get; set; }
    }
}
