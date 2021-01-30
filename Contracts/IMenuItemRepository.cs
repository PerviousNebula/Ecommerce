using Entities.Models;

namespace Contracts
{
    public interface IMenuItemRepository : IRepositoryBase<MenuItem>
    {
        void CreateMenuItem(MenuItem menuItem);
        void UpdateMenuItem(MenuItem menuItem);
        void DeleteMenuItem(MenuItem menuItem);
    }
}