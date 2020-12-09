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
        private ICategoryRepository _category;
        private IProductRepository _product;
        private IColorRepository _color;
        private ISizeRepository _size;
        private IUserRepository _user;
        private IRolRepository _rol;
        private IOrderRepository _order;
        private IOrderDetailRepository _orderDetail;
        private IProductDesignRepository _productDesign;

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

        public ICategoryRepository Category {
            get {
                if(_category == null)
                {
                    _category = new CategoryRepository(_repoContext);
                }

                return _category;
            }
        }

        public IProductRepository Product {
            get {
                if(_product == null)
                {
                    _product = new ProductRepository(_repoContext);
                }

                return _product;
            }
        }

        public IColorRepository Color {
            get {
                if(_color == null)
                {
                    _color = new ColorRepository(_repoContext);
                }

                return _color;
            }
        }

        public ISizeRepository Size {
            get {
                if(_size == null)
                {
                    _size = new SizeRepository(_repoContext);
                }

                return _size;
            }
        }

        public IUserRepository User {
            get {
                if(_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }

                return _user;
            }
        }

        public IRolRepository Rol {
            get {
                if (_rol == null)
                {
                    _rol = new RolRepository(_repoContext);
                }

                return _rol;
            }
        }

        public IOrderRepository Order {
            get {
                if (_order == null)
                {
                    _order = new OrderRepository(_repoContext);
                }

                return _order;
            }
        }

        public IOrderDetailRepository OrderDetail {
            get {
                if (_orderDetail == null)
                {
                    _orderDetail = new OrderDetailRepository(_repoContext);
                }

                return _orderDetail;
            }
        }

        public IProductDesignRepository ProductDesign {
            get {
                if (_productDesign == null)
                {
                    _productDesign = new ProductDesignRepository(_repoContext);
                }

                return _productDesign;
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