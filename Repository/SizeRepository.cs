using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateSizes(IEnumerable<Size> sizes)
        {
            foreach (var size in sizes)
            {
                Create(size);
            }
        }

        public void DeleteSize(Size size)
        {
            Delete(size);
        }

        public async Task<Size> GetSizeById(int sizeId)
        {
            return await FindByCondition(s => s.id == sizeId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Size>> GetSizesByProductId(int productId)
        {
            return await FindByCondition(s => s.productId == productId)
                .ToListAsync();
        }

        public void UpdateSize(Size size)
        {
            Update(size);
        }
    }
}