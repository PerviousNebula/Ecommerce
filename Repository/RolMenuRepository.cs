using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RolMenuRepository : RepositoryBase<RolMenu>, IRolMenuRepository
    {
        public RolMenuRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateRolMenu(RolMenu rolMenu)
        {
            Create(rolMenu);
        }

        public void DeleteRolMenu(RolMenu rolMenu)
        {
            Delete(rolMenu);
        }

        public async Task<IEnumerable<RolMenu>> GetRolMenus()
        {
            return await FindAll()
                .Include(r => r.Rol)
                .Include(m => m.Menu)
                .ToListAsync();
        }

        public async Task<IEnumerable<RolMenu>> GetRolMenusByRolId(int rolId)
        {
            return await FindByCondition(rm => rm.rolId == rolId)
                .Include(m => m.Menu)
                .Include(m => m.Menu.MenuItems)
                .ToListAsync();
        }

        public void UpdateRolMenu(RolMenu rolMenu)
        {
            Update(rolMenu);
        }
    }
}