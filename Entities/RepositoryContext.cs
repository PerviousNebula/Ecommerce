using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}