using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IProductDesignRepository : IRepositoryBase<ProductDesign>
    {
        Task<ProductDesign> GetProductDesignByIdAsync(int productDesignId);
        void CreateProductDesign(ProductDesign productDesign);
        void UpdateProductDesign(ProductDesign productDesign);
        void DeleteProductDesign(ProductDesign productDesign);
    }
}