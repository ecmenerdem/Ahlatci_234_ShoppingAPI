using Microsoft.EntityFrameworkCore;
using Shopping.DAL.Abstract;
using Shopping.DAL.Concrete.EntityFramework.DataManagement;
using ShoppingAPI.Entity.Poco;

namespace Shopping.DAL.Concrete.EntityFramework;

public class EfUserRepository : EfRepository<User>, IUserRepository
{
    public EfUserRepository(DbContext context) : base(context)
    {
    }
}