using Entities.Models;

namespace Contracts
{
    public interface IProductDesignRepository : IRepositoryBase<ProductDesign>
    {
        void CreateProductDesign(ProductDesign productDesign);
        void UpdateProductDesign(ProductDesign productDesign);
        void DeleteProductDesign(ProductDesign productDesign);
    }
}