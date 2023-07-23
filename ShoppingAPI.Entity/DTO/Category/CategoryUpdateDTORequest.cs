using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Entity.DTO.Category
{
    public class CategoryUpdateDTORequest
    {
        public string Name { get; set; }
        public Guid? Guid { get; set; }
        public bool? IsActive { get; set; }
    }
}
