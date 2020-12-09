using Entities.Models;

namespace Contracts
{
    public interface IOrderDetailRepository : IRepositoryBase<OrderDetail>
    {
        void CreateOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(OrderDetail orderDetail);
    }
}