using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IRolMenuRepository : IRepositoryBase<RolMenu>
    {
        Task<IEnumerable<RolMenu>> GetRolMenus();
        Task<IEnumerable<RolMenu>> GetRolMenusByRolId(int rolId);
        void CreateRolMenu(RolMenu rolMenu);
        void UpdateRolMenu(RolMenu rolMenu);
        void DeleteRolMenu(RolMenu rolMenu);
    }
}