using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId)
        {
            return await FindByCondition(od => od.id == orderDetailId)
                .Include(od => od.ProductDesign)
                .FirstOrDefaultAsync();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            Update(orderDetail);
        }
    }
}