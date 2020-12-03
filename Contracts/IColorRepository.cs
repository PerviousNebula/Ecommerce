using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IColorRepository : IRepositoryBase<Color>
    {
        Task<IEnumerable<Color>> GetColorsByProductId(int productId);
        void CreateColors(IEnumerable<Color> colors);
        void UpdateColor(Color color);
        void DeleteColor(Color color);
    }
}