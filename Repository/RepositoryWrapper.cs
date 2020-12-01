using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ICustomerRepository _customer;
        private IAddressRepository _address;

        public ICustomerRepository Customer {
            get {
                if(_customer == null)
                {
                    _customer = new CustomerRepository(_repoContext);
                }

                return _customer;
            }
        }

        public IAddressRepository Address {
            get {
                if(_address == null)
                {
                    _address = new AddressRepository(_repoContext);
                }

                return _address;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}