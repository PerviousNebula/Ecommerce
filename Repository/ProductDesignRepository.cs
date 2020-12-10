using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ProductDesign> GetProductDesignByIdAsync(int productDesignId)
        {
            return await FindByCondition(pd => pd.id == productDesignId)
                .FirstOrDefaultAsync();
        }

        public void UpdateProductDesign(ProductDesign productDesign)
        {
            Update(productDesign);
        }
    }
}