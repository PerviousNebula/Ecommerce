using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            Create(orderDetail);
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            Delete(orderDetail);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            Update(orderDetail);
        }
    }
}