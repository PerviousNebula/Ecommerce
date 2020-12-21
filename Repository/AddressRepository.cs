using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public async Task<PagedList<Address>> GetAllAddressesAsync(AddressParameters addressParameters)
        {
            var addresses = FindAll();
            SearchByAddress1(ref addresses, addressParameters.address1);
            SearchByAddress2(ref addresses, addressParameters.address2);
            SearchByCity(ref addresses, addressParameters.city);
            SearchByState(ref addresses, addressParameters.state);
            SearchByCountry(ref addresses, addressParameters.country);
            SearchByZip(ref addresses, addressParameters.zip);
            SearchByArchive(ref addresses, addressParameters.archive);
            SearchByCustomer(ref addresses, addressParameters.customerId);

            return await PagedList<Address>.ToPagedList(addresses,
                addressParameters.PageNumber,
                addressParameters.PageSize);
        }

        private void SearchByAddress1(ref IQueryable<Address> addresses, string address1)
        {
            if (!addresses.Any() || string.IsNullOrWhiteSpace(address1))
                return;
            addresses = addresses.Where(a => a.address1.ToLower().Contains(address1.Trim().ToLower()));
        }

        private void SearchByAddress2(ref IQueryable<Address> addresses, string address2)
        {
            if (!addresses.Any() || string.IsNullOrWhiteSpace(address2))
                return;
            addresses = addresses.Where(a => a.address2.ToLower().Contains(address2.Trim().ToLower()));
        }

        private void SearchByCity(ref IQueryable<Address> addresses, string city)
        {
            if (!addresses.Any() || string.IsNullOrWhiteSpace(city))
                return;
            addresses = addresses.Where(a => a.city.ToLower().Contains(city.Trim().ToLower()));
        }

        private void SearchByState(ref IQueryable<Address> addresses, string state)
        {
            if (!addresses.Any() || string.IsNullOrWhiteSpace(state))
                return;
            addresses = addresses.Where(a => a.state.ToLower().Contains(state.Trim().ToLower()));
        }

        private void SearchByCountry(ref IQueryable<Address> addresses, string country)
        {
            if (!addresses.Any() || string.IsNullOrWhiteSpace(country))
                return;
            addresses = addresses.Where(a => a.country.ToLower().Contains(country.Trim().ToLower()));
        }

        private void SearchByZip(ref IQueryable<Address> addresses, string zip)
        {
            if (!addresses.Any() || string.IsNullOrWhiteSpace(zip))
                return;
            addresses = addresses.Where(a => a.zip.ToLower().Contains(zip.Trim().ToLower()));
        }

        private void SearchByArchive(ref IQueryable<Address> addresses, bool? archive)
        {
            if (!addresses.Any() || archive == null)
                return;
            addresses = addresses.Where(a => a.archive == archive);
        }

        private void SearchByCustomer(ref IQueryable<Address> addresses, int? customerId)
        {
            if (!addresses.Any() || customerId == null)
                return;
            addresses = addresses.Where(a => a.customerId == customerId);
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await FindByCondition(a => a.id == addressId)
                .FirstOrDefaultAsync();
        }

        public void CreateAddress(Address address)
        {
            Create(address);
        }

        public void UpdateAddress(Address address)
        {
            Update(address);
        }

        public void DeleteAddress(Address address)
        {
            Delete(address);
        }

        public void CreateAddresses(IEnumerable<Address> addresses)
        {
            foreach (var address in addresses)
            {
                Create(address);
            }
        }

        public void UpdateAddresses(IEnumerable<Address> addresses)
        {
            foreach (var address in addresses)
            {
                Update(address);
            }
        }
    }
}