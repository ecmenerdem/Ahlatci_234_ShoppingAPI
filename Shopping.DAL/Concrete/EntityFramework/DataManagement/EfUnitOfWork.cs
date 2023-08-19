using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shopping.DAL.Abstract;
using Shopping.DAL.Abstract.DataManagement;
using Shopping.DAL.Concrete.EntityFramework.Context;
using ShoppingAPI.Entity.Base;

namespace Shopping.DAL.Concrete.EntityFramework.DataManagement
{
    public class EfUnitOfWork:IUnitOfWork
    {
        private readonly ShoppingContext _shoppingContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public EfUnitOfWork(ShoppingContext shoppingContext, IHttpContextAccessor contextAccessor)
        {
            _shoppingContext = shoppingContext;
            _contextAccessor = contextAccessor;

            CategoryRepository = new EfCategoryRepository(_shoppingContext);
            UserRepository = new EfUserRepository(_shoppingContext);
            OrderRepository = new EfOrderRepository(_shoppingContext);
            ProductRepository = new EfProductRepository(_shoppingContext);
            OrderDetailRepository = new EfOrderDetailRepository(_shoppingContext);

        }


        public IUserRepository UserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderDetailRepository OrderDetailRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public async Task<int> SaveChangeAsync()
        {
            foreach (var item in _shoppingContext.ChangeTracker.Entries<AuditableEntity>())
            {
                if (item.State==EntityState.Added)
                {
                    item.Entity.AddedTime=DateTime.Now;
                    item.Entity.UpdatedTime=DateTime.Now;
                    item.Entity.AddedUser = 1;
                    item.Entity.UpdatedUser = 1;
                    item.Entity.GUID = Guid.NewGuid();
                    item.Entity.AddedIPV4Adress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    item.Entity.UpdatedIPV4Adress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                    if (item.Entity.IsActive==null)
                    {
                        item.Entity.IsActive = true;
                    }

                    item.Entity.IsDeleted = false;
                }
                else if (item.State==EntityState.Modified)
                {
                    item.Entity.UpdatedTime=DateTime.Now;
                    item.Entity.UpdatedUser = 1;
                    item.Entity.UpdatedIPV4Adress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                }
            }

            return await _shoppingContext.SaveChangesAsync();
        }
    }
}
