using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Entity.DTO.Product
{
    public class ProductDTORequest
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FeaturedImage { get; set; }
        public Guid CategoryGuid { get; set; }
        public int CategoryID { get; set; }
        public double? UnitPrice { get; set; }

    }
}
