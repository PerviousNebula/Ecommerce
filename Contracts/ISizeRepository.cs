using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ISizeRepository : IRepositoryBase<Size>
    {
        Task<IEnumerable<Size>> GetSizesByProductId(int productId);
        Task<Size> GetSizeById(int sizeId);
        void CreateSizes(IEnumerable<Size> sizes);
        void UpdateSize(Size size);
        void UpdateSizes(IEnumerable<Size> sizes);
        void DeleteSize(Size size);
    }
}