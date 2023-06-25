using ShoppingAPI.Entity.Base;

namespace ShoppingAPI.Entity.Poco
{
    public class Category : AuditableEntity
    { 
        /*Constructor*/
        public Category()
        {
            Products = new HashSet<Product>();
        }
       public string Name { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }

        /* EF--> Lazy Loading / Change Tracking / Proxy */

    }
    

}
