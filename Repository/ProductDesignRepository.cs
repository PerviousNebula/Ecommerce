using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ProductDesignRepository : RepositoryBase<ProductDesign>, IProductDesignRepository
    {
        public ProductDesignRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateProductDesign(ProductDesign productDesign)
        {
            Create(productDesign);
        }

        public void DeleteProductDesign(ProductDesign productDesign)
        {
            Delete(productDesign);
        }

        public void UpdateProductDesign(ProductDesign productDesign)
        {
            Update(productDesign);
        }
    }
}