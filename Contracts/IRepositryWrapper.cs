using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICustomerRepository Customer { get; }
        IAddressRepository Address { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IColorRepository Color { get; }
        ISizeRepository Size { get; }
        IUserRepository User { get; }
        IRolRepository Rol { get; }
        IOrderRepository Order { get; }
        Task SaveAsync();
    }
}