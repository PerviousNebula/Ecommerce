using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public async Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParameters categoryParameters)
        {
            var categories = FindAll();
            SearchByName(ref categories, categoryParameters.name);
            SearchByArchive(ref categories, categoryParameters.archive);

            return await PagedList<Category>.ToPagedList(categories,
                categoryParameters.PageNumber,
                categoryParameters.PageSize);
        }

        private void SearchByName(ref IQueryable<Category> categories, string name)
        {
            if (!categories.Any() || string.IsNullOrWhiteSpace(name))
                return;
            categories = categories.Where(a => a.name.ToLower().Contains(name.Trim().ToLower()));
        }

        private void SearchByArchive(ref IQueryable<Category> categories, bool? archive)
        {
            if (!categories.Any() || archive == null)
                return;
            categories = categories.Where(a => a.archive == archive);
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await FindByCondition(a => a.id == categoryId)
                .FirstOrDefaultAsync();
        }

        public async Task<Category> GetCustomerWithProductsAsync(int categoryId)
        {
            return await FindByCondition(a => a.id == categoryId)
                .Include(c => c.Products)
                .FirstOrDefaultAsync();
        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public void UpdateCategory(Category category)
        {
            Update(category);
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
        }
    }
}