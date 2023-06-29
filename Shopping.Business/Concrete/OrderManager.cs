using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shopping.Business.Abstract;
using Shopping.DAL.Abstract.DataManagement;
using ShoppingAPI.Entity.Poco;

namespace Shopping.Business.Concrete
{
    public class OrderManager:IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderManager(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Order> GetAsync(Expression<Func<Order, bool>> Filter, params string[] IncludeProperties)
        {
            return await _uow.OrderRepository.GetAsync(Filter, IncludeProperties);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> Filter = null, params string[] IncludeProperties)
        {
            return await _uow.OrderRepository.GetAllAsync(Filter, IncludeProperties);
        }

        public async Task<Order> AddAsync(Order Entity)
        {
            // API den Gelen Data
            //OrderDetail od = new OrderDetail()
            //{
            //    ProductID = 2,
            //    Discount = 0,
            //    Quantity = 3,
            //    UnitPrice = 25
            //};

            //OrderDetail od1 = new OrderDetail()
            //{
            //    ProductID = 4,
            //    Discount = 10,
            //    Quantity = 4,
            //    UnitPrice = 12
            //};


            //Entity.OrderDetails.ToList().Add(od);
            //Entity.OrderDetails.ToList().Add(od1);

            await _uow.OrderRepository.AddAsync(Entity);
            await _uow.SaveChangeAsync();
            return Entity;

        }

        public async Task UpdateAsync(Order Entity)
        {
            await _uow.OrderRepository.UpdateAsync(Entity);
            await _uow.SaveChangeAsync();
        }

        public async Task RemoveAsync(Order Entity)
        {
            await _uow.OrderRepository.RemoveAsync(Entity);
            await _uow.SaveChangeAsync();
        }

        public async Task OrderAddV1(Order order, List<OrderDetail> orderDetails)
        {
            
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.Order = order;
                await _uow.OrderDetailRepository.AddAsync(orderDetail);
            }

            await _uow.OrderRepository.AddAsync(order);

            await _uow.SaveChangeAsync();
        }
    }
}
