using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICustomerRepository Customer { get; }
        IAddressRepository Address { get; }
        Task SaveAsync();
    }
}