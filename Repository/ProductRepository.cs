using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public async Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters)
        {
            var products = FindAll();
            SearchByName(ref products, productParameters.name);
            SearchBySKU(ref products, productParameters.sku);
            SearchByArchive(ref products, productParameters.archive);
            SearchByCategoryId(ref products, productParameters.categoryId);
            SearchByPrice(ref products, productParameters.MinPrice, productParameters.MaxPrice);
            SearchByStock(ref products, productParameters.MaxStock);
            SearchByWeight(ref products, productParameters.MaxWeight);

            return await PagedList<Product>.ToPagedList(products.OrderBy(p => p.name),
                productParameters.PageNumber,
                productParameters.PageSize);
        }

        private void SearchByName(ref IQueryable<Product> products, string name)
        {
            if (!products.Any() || string.IsNullOrWhiteSpace(name))
                return;

            products = products.Where(p => p.name.ToLower().Contains(name.Trim().ToLower()));
        }

        private void SearchBySKU(ref IQueryable<Product> products, string sku)
        {
            if (!products.Any() || string.IsNullOrWhiteSpace(sku))
                return;

            products = products.Where(p => p.sku.ToLower().Contains(sku.Trim().ToLower()));
        }

        private void SearchByArchive(ref IQueryable<Product> products, bool? archive)
        {
            if (!products.Any() || archive == null)
                return;

            products = products.Where(p => p.archive == archive);
        }

        private void SearchByCategoryId(ref IQueryable<Product> products, int? categoryId)
        {
            if (!products.Any() || categoryId == null)
                return;

            products = products.Where(p => p.categoryId == categoryId);
        }

        private void SearchByPrice(ref IQueryable<Product> products, double? MinPrice, double? MaxPrice)
        {
            if (!products.Any())
                return;

            if (MinPrice == null && MaxPrice != null)
            {
                products = products.Where(c => c.price <= MaxPrice);
            }
            else if (MinPrice != null && MaxPrice == null)
            {
                products = products.Where(c => c.price >= MinPrice);
            }
            else if (MinPrice != null && MaxPrice != null)
            {
                products = products.Where(c => c.price >= MinPrice && c.price <= MaxPrice);
            }
        }

        private void SearchByStock(ref IQueryable<Product> products, double? MaxStock)
        {
            if (!products.Any() || MaxStock == null)
                return;

            products = products.Where(p => p.stock <= MaxStock);
        }

        private void SearchByWeight(ref IQueryable<Product> products, double? MaxWeight)
        {
            if (!products.Any() || MaxWeight == null)
                return;

            products = products.Where(p => p.weight <= MaxWeight);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await FindByCondition(p => p.id == productId)
                .Include(p => p.Sizes)
                .Include(p => p.Colors)
                .FirstOrDefaultAsync();
        }

        public void CreateProduct(Product product)
        {
            Create(product);
        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }
    }
}