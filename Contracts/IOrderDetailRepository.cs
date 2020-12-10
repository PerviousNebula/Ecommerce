using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IOrderDetailRepository : IRepositoryBase<OrderDetail>
    {
        Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId);
        void CreateOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(OrderDetail orderDetail);
    }
}