using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IRolRepository : IRepositoryBase<Rol>
    {
        Task<IEnumerable<Rol>> GetRoles();
    }
}