using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<PagedList<Order>> GetAllOrdersAsync(OrderParameters orderParameters);
        Task<Order> GetOrderByIdAsync(int orderId);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}