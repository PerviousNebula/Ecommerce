using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateMenu(Menu menu)
        {
            Create(menu);
        }

        public void DeleteMenu(Menu menu)
        {
            Delete(menu);
        }

        public async Task<IEnumerable<Menu>> GetMenus()
        {
            return await FindAll()
                .Include(m => m.MenuItems)
                .ToListAsync();
        }

        public void UpdateMenu(Menu menu)
        {
            Update(menu);
        }
    }
}