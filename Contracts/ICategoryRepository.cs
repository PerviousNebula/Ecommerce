using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParameters categoryParameters);
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<Category> GetCustomerWithProductsAsync(int categoryId);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}