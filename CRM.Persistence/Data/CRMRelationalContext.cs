using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.Persistence.DbContexts
{
    public class CRMRelationalContext : DbContext
    {
        public CRMRelationalContext (DbContextOptions<CRMRelationalContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = default!;
    }
}
