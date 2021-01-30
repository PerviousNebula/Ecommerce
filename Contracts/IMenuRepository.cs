using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IMenuRepository : IRepositoryBase<Menu>
    {
        Task<IEnumerable<Menu>> GetMenus();
        void CreateMenu(Menu menu);
        void UpdateMenu(Menu menu);
        void DeleteMenu(Menu menu);
    }
}