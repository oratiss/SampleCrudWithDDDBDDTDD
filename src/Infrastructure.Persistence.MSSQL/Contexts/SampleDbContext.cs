using Infrastructure.Persistence.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.MSSQL.Contexts
{
    public class SampleDbContext: DbContext
    {
        public SampleDbContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public DbSet<Customer> Customers { get; set; }


    }
}
