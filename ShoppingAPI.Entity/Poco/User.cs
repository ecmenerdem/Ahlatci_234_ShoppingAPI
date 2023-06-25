using System.ComponentModel.DataAnnotations;
using ShoppingAPI.Entity.Base;

namespace ShoppingAPI.Entity.Poco
{
    public class User : AuditableEntity
    {
        public User()
        {
            this.Orders= new HashSet<Order>();

            "Orders.OrderDetails.Product.Category"
            this.Orders.FirstOrDefault().OrderDetails.FirstOrDefault().Product.Category;
        }

        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }

    }
}
