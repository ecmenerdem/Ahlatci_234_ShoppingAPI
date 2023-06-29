using ShoppingAPI.Entity.Poco;

namespace Shopping.Business.Abstract;

public interface IOrderService:IGenericService<Order>
{
    Task OrderAddV1(Order order, List<OrderDetail> orderDetails);
}