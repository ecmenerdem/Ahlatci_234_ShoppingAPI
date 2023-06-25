using ShoppingAPI.Entity.Base;

namespace ShoppingAPI.Entity.Poco
{
    public class Order:AuditableEntity
    {
       public Order()
        {
            this.OrderDetails=new HashSet<OrderDetail>();
        }
        public int UserID { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
        public User User { get; set; }
    }
}
