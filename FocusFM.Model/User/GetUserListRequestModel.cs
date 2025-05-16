using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusFM.Model.CommonPagination;

namespace FocusFM.Model.User
{
    public class GetUserListRequestModel:CommonPaginationModel
    {
        public string UserTypeIds { get; set; }
    }
}
