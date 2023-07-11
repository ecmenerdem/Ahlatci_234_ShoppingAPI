using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Entity.DTO.User
{
    public class UserDTOResponse:UserDTOBase
    {
        public Guid? Guid { get; set; }
    }
}
