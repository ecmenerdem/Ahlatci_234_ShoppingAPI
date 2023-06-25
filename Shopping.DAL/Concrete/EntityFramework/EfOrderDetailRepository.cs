using Microsoft.EntityFrameworkCore;
using Shopping.DAL.Abstract;
using Shopping.DAL.Concrete.EntityFramework.DataManagement;
using ShoppingAPI.Entity.Poco;

namespace Shopping.DAL.Concrete.EntityFramework;

public class EfOrderDetailRepository : EfRepository<OrderDetail>, IOrderDetailRepository
{
    public EfOrderDetailRepository(DbContext context) : base(context)
    {
    }
}