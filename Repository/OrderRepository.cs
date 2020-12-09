using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateOrder(Order order)
        {
            Create(order);
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        public async Task<PagedList<Order>> GetAllOrdersAsync(OrderParameters orderParameters)
        {
            var orders = FindAll().Include(o => o.Address);
            // ToDo: Filtrado....

            return await PagedList<Order>.ToPagedList(orders,
                orderParameters.PageNumber,
                orderParameters.PageSize);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await FindByCondition(o => o.id == orderId)
                .Include(o => o.Address)
                .FirstOrDefaultAsync();
        }

        public void UpdateOrder(Order order)
        {
            Update(order);
        }
    }
}