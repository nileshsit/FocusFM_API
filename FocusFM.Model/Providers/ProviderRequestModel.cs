﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FocusFM.Model.Providers
{
    public class ProviderRequestModel
    {
        public int? ProviderId { get; set; }
        [Required(ErrorMessage = "Provider Name is required.")]
        [StringLength(100, ErrorMessage = "ProviderName - Only 100 Character allowed.")]
        public string ProviderName { get; set; }
        public IFormFile? File { get; set; }
    }
}
