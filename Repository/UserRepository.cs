using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await FindByCondition(u => u.email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await FindByCondition(u => u.id == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await FindAll()
                .Include(u => u.Rol)
                .ToListAsync();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }
    }
}