using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class MenuItemRepository : RepositoryBase<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateMenuItem(MenuItem menuItem)
        {
            Create(menuItem);
        }

        public void DeleteMenuItem(MenuItem menuItem)
        {
            Delete(menuItem);
        }


        public void UpdateMenuItem(MenuItem menuItem)
        {
            Update(menuItem);
        }
    }
}