using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateCustomer(Customer customer)
        {
            Create(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            Delete(customer);
        }

        public async Task<PagedList<Customer>> GetAllCustomersAsync(CustomerParameters customerParameters)
        {
            var customers = FindAll();
            SearchBySignupDate(ref customers, customerParameters.MinSignupDate, customerParameters.MaxSignupDate);
            SearchByName(ref customers, customerParameters.name);
            SearchByLastName(ref customers, customerParameters.lastName);
            SearchByEmail(ref customers, customerParameters.email);
            SearchByArchive(ref customers, customerParameters.archive);
            
            return await PagedList<Customer>.ToPagedList(customers,
                customerParameters.PageNumber,
                customerParameters.PageSize);
        }

        private void SearchBySignupDate(ref IQueryable<Customer> customers, DateTime? MinSignupDate, DateTime? MaxSignupDate)
        {
            if (MinSignupDate == null && MaxSignupDate != null)
            {
                customers = customers.Where(c => c.signupDate.CompareTo(MaxSignupDate.Value) <= 0);
            }
            else if (MinSignupDate != null && MaxSignupDate == null)
            {
                customers = customers.Where(c => c.signupDate.CompareTo(MinSignupDate.Value) >= 0);
            }
            else if (MinSignupDate != null && MaxSignupDate != null)
            {
                customers = customers.Where(c => c.signupDate.CompareTo(MinSignupDate.Value) >= 0 &&
                    c.signupDate.CompareTo(MaxSignupDate.Value) <= 0);
            }
        }

        private void SearchByName(ref IQueryable<Customer> customers, string customerName)
        {
            if (!customers.Any() || string.IsNullOrWhiteSpace(customerName))
                return;
            customers = customers.Where(c => c.name.ToLower().Contains(customerName.Trim().ToLower()));
        }

        private void SearchByLastName(ref IQueryable<Customer> customers, string customerLastName)
        {
            if (!customers.Any() || string.IsNullOrWhiteSpace(customerLastName))
                return;
            customers = customers.Where(c => c.lastName.ToLower().Contains(customerLastName.Trim().ToLower()));
        }

        private void SearchByEmail(ref IQueryable<Customer> customers, string customerEmail)
        {
            if (!customers.Any() || string.IsNullOrWhiteSpace(customerEmail))
                return;
            customers = customers.Where(c => c.email.ToLower().Contains(customerEmail.Trim().ToLower()));
        }

        private void SearchByArchive(ref IQueryable<Customer> customers, bool? archive)
        {
            if (!customers.Any() || archive == null)
                return;
            customers = customers.Where(c => c.archive == archive);
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await FindByCondition(cu => cu.id == customerId)
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerWithDetailsAsync(int customerId)
        {
            return await FindByCondition(cu => cu.id == customerId)
                .Include(c => c.Addresses)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync();
        }

        public void UpdateCustomer(Customer customer)
        {
            Update(customer);
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await FindByCondition(c => c.email == email)
                .FirstOrDefaultAsync();
        }
    }
}