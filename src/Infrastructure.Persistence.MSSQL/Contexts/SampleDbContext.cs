using Infrastructure.Persistence.MSSQL.Configurations;
using Infrastructure.Persistence.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.MSSQL.Contexts
{
    public class SampleDbContext: DbContext
    {
        public SampleDbContext()
        {
        }

        public SampleDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
        }
    }
}
