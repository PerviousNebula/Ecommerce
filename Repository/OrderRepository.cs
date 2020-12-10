using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

        public void GenerateOrderNumber(Order order)
        {
            var lastOrderId = RepositoryContext.Orders.OrderByDescending(o => o.id)
                .FirstOrDefault().id;
            order.id = lastOrderId;
            order.orderNumber = String.Concat("W", lastOrderId);
            Update(order);
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        public async Task<PagedList<Order>> GetAllOrdersAsync(OrderParameters orderParameters)
        {
            var orders = FindAll().Include(o => o.Address);
            SearchByOrderNumber(ref orders, orderParameters.orderNumber);
            SearchByAddress1(ref orders, orderParameters.address1);
            SearchByAddress2(ref orders, orderParameters.address2);
            SearchByCity(ref orders, orderParameters.city);
            SearchByState(ref orders, orderParameters.state);
            SearchByCountry(ref orders, orderParameters.country);
            SearchByZip(ref orders, orderParameters.zip);
            SearchByShippingPrice(ref orders, orderParameters.MinShipping, orderParameters.MaxShipping);
            SearchByTotalPrice(ref orders, orderParameters.MinTotal, orderParameters.MaxTotal);
            SearchByCreationDate(ref orders, orderParameters.MinDate, orderParameters.MaxDate);
            SearchByCustomer(ref orders, orderParameters.customerId);

            return await PagedList<Order>.ToPagedList(orders,
                orderParameters.PageNumber,
                orderParameters.PageSize);
        }

        public void SearchByOrderNumber(ref IIncludableQueryable<Order, Address> orders, string orderNumber)
        {
            if (!orders.Any() || string.IsNullOrEmpty(orderNumber))
                return;
            orders = orders.Where(o => o.orderNumber.ToLower().Contains(orderNumber.Trim().ToLower()))
                .Include(o => o.Address);
        }

        public void SearchByAddress1(ref IIncludableQueryable<Order, Address> orders, string address1)
        {
            if (!orders.Any() || string.IsNullOrEmpty(address1))
                return;
            orders = orders.Where(o => o.Address.address1.Contains(address1.Trim().ToLower()) || 
                o.address1.Contains(address1.Trim().ToLower()))
                .Include(o => o.Address);
        }

        public void SearchByAddress2(ref IIncludableQueryable<Order, Address> orders, string address2)
        {
            if (!orders.Any() || string.IsNullOrEmpty(address2))
                return;
            orders = orders.Where(o => o.Address.address2.Contains(address2.Trim().ToLower()) || 
                o.address2.Contains(address2.Trim().ToLower()))
                .Include(o => o.Address);
        }

        public void SearchByCity(ref IIncludableQueryable<Order, Address> orders, string city)
        {
            if (!orders.Any() || string.IsNullOrEmpty(city))
                return;
            orders = orders.Where(o => o.city.Contains(city.Trim().ToLower()))
                .Include(o => o.Address);
        }

        public void SearchByState(ref IIncludableQueryable<Order, Address> orders, string state)
        {
            if (!orders.Any() || string.IsNullOrEmpty(state))
                return;
            orders = orders.Where(o => o.state.Contains(state.Trim().ToLower()))
                .Include(o => o.Address);
        }

        public void SearchByCountry(ref IIncludableQueryable<Order, Address> orders, string country)
        {
            if (!orders.Any() || string.IsNullOrEmpty(country))
                return;
            orders = orders.Where(o => o.country.Contains(country.Trim().ToLower())).Include(o => o.Address);
        }

        public void SearchByZip(ref IIncludableQueryable<Order, Address> orders, string zip)
        {
            if (!orders.Any() || string.IsNullOrEmpty(zip))
                return;
            orders = orders.Where(o => o.zip.Contains(zip.Trim())).Include(o => o.Address);
        }

        private void SearchByShippingPrice(ref IIncludableQueryable<Order, Address> orders, double? MinPrice, double? MaxPrice)
        {
            if (!orders.Any())
                return;

            if (MinPrice == null && MaxPrice != null)
            {
                orders = orders.Where(o => o.shipping <= MaxPrice).Include(o => o.Address);
            }
            else if (MinPrice != null && MaxPrice == null)
            {
                orders = orders.Where(o => o.shipping >= MinPrice).Include(o => o.Address);
            }
            else if (MinPrice != null && MaxPrice != null)
            {
                orders = orders.Where(o => o.shipping >= MinPrice && o.shipping <= MaxPrice)
                    .Include(o => o.Address);
            }
        }

        private void SearchByTotalPrice(ref IIncludableQueryable<Order, Address> orders, double? MinTotal, double? MaxTotal)
        {
            if (!orders.Any())
                return;

            if (MinTotal == null && MaxTotal != null)
            {
                orders = orders.Where(o => o.total <= MaxTotal).Include(o => o.Address);
            }
            else if (MinTotal != null && MaxTotal == null)
            {
                orders = orders.Where(o => o.total >= MinTotal).Include(o => o.Address);
            }
            else if (MinTotal != null && MaxTotal != null)
            {
                orders = orders.Where(o => o.total >= MinTotal && o.total <= MaxTotal)
                    .Include(o => o.Address);
            }
        }
        
        private void SearchByCreationDate(ref IIncludableQueryable<Order, Address> orders, DateTime? MinDate, DateTime? MaxDate)
        {
            if (MinDate == null && MaxDate != null)
            {
                orders = orders.Where(o => o.date.CompareTo(MaxDate.Value) <= 0).Include(o => o.Address);
            }
            else if (MinDate != null && MaxDate == null)
            {
                orders = orders.Where(o => o.date.CompareTo(MinDate.Value) >= 0).Include(o => o.Address);
            }
            else if (MinDate != null && MaxDate != null)
            {
                orders = orders.Where(o => o.date.CompareTo(MinDate.Value) >= 0 &&
                    o.date.CompareTo(MaxDate.Value) <= 0).Include(o => o.Address);
            }
        }
        
        private void SearchByCustomer(ref IIncludableQueryable<Order, Address> orders, int? customerId)
        {
            if (!orders.Any() || customerId == null)
                return;
            orders = orders.Where(o => o.customerId == customerId).Include(o => o.Address);
        }
        
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await FindByCondition(o => o.id == orderId)
                .Include(o => o.Address)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync();
        }

        public void UpdateOrder(Order order)
        {
            Update(order);
        }
    }
}