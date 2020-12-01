using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<PagedList<Customer>> GetAllCustomersAsync(CustomerParameters customerParameters);
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<Customer> GetCustomerWithAddressesAsync(int customerId);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}