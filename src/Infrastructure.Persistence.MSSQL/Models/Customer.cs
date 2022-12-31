using System.Net.Http.Headers;
using Infrastructure.Persistence.MSSQL.BaseModels;

namespace Infrastructure.Persistence.MSSQL.Models
{
    public class Customer: PersistenceEntity<long>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
