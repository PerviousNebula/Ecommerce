using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        Task<PagedList<Address>> GetAllAddressesAsync(AddressParameters addressParameters);
        Task<Address> GetAddressByIdAsync(int addressId);
        void CreateAddress(Address address);
        void CreateAddresses(IEnumerable<Address> addresses);
        void UpdateAddress(Address address);
        void UpdateAddresses(IEnumerable<Address> addresses);
        void DeleteAddress(Address address);
    }
}