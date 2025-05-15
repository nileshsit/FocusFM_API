using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.CommonPagination;

namespace FocusFM.Model.Site.Floor
{
    public class GetSiteFloorModel: CommonPaginationModel
    {
        public long SiteId { get; set; }
    }
}
