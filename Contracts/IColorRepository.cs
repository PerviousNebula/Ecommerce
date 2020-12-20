using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IColorRepository : IRepositoryBase<Color>
    {
        Task<IEnumerable<Color>> GetColorsByProductId(int productId);
        Task<Color> GetColorByIdAsync(int colorId);
        void CreateColors(IEnumerable<Color> colors);
        void UpdateColor(Color color);
        void UpdateColors(IEnumerable<Color> colors);
        void DeleteColor(Color color);
    }
}