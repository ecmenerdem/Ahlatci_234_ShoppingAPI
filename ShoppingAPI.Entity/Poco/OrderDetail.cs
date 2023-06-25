using ShoppingAPI.Entity.Base;

namespace ShoppingAPI.Entity.Poco
{
    public class OrderDetail:AuditableEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public double UnitPrice { get; set; }
        public double? Quantity { get; set; }
        public double? Discount { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

    }
}
