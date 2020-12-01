using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        Task<PagedList<Address>> GetAllAddressesAsync(AddressParameters addressParameters);
        Task<Address> GetAddressByIdAsync(int addressId);
        void CreateAddress(Address address);
        void UpdateAddress(Address address);
        void DeleteAddress(Address address);

    }
}