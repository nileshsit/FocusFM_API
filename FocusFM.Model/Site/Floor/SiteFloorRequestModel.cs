﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusFM.Model.Site.Floor
{
    public class SiteFloorRequestModel
    {
        public long? FloorId { get; set; }
        public long SiteId { get; set; }
        public string FloorName { get; set; }
    }
}
