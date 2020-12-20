using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateColors(IEnumerable<Color> colors)
        {
            foreach (var color in colors)
            {
                Create(color);
            }
        }

        public void DeleteColor(Color color)
        {
            Delete(color);
        }

        public async Task<Color> GetColorByIdAsync(int colorId)
        {
            return await FindByCondition(c => c.id == colorId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Color>> GetColorsByProductId(int productId)
        {
            return await FindByCondition(c => c.productId == productId)
                .ToListAsync();
        }

        public void UpdateColor(Color color)
        {
            Update(color);
        }

        public void UpdateColors(IEnumerable<Color> colors)
        {
            foreach (var color in colors)
            {
                Update(color);
            }
        }
    }
}