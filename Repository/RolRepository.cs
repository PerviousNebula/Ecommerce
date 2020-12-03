using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RolRepository : RepositoryBase<Rol>, IRolRepository
    {
        public RolRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Rol>> GetRoles()
        {
            return await FindAll().ToListAsync();
        }
    }
}