using Microsoft.EntityFrameworkCore;
using Shopping.DAL.Abstract;
using Shopping.DAL.Concrete.EntityFramework.DataManagement;
using ShoppingAPI.Entity.Poco;

namespace Shopping.DAL.Concrete.EntityFramework;

public class EfProductRepository : EfRepository<Product>, IProductRepository
{
    public EfProductRepository(DbContext context) : base(context)
    {
    }
}