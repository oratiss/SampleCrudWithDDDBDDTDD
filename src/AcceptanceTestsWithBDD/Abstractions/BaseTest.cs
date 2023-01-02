using Api;
using Infrastructure.Persistence.MSSQL.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AcceptanceTestsWithBDD.Abstractions
{
    //[AutoRollback]
    public class BaseTest : WebApplicationFactory<Program>
    {
        protected HttpClient? client;
        private IConfiguration _configuration = null!;

        public BaseTest()
        {

            var applicationFactory = WithWebHostBuilder(builder =>
            {
                Server.PreserveExecutionContext = true;
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    // expand default config with settings designed for Integration Tests
                    var rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString();
                    conf.AddJsonFile(Path.Combine(rootDirectory!, "appsettings.Integration.json"));
                    conf.AddEnvironmentVariables();

                    // here we can "compile" the settings. Api.Setup will do the same, it doesn't matter.
                    _configuration = conf.Build();
                });
                builder.ConfigureTestServices(services =>
                {
                    services.AddDbContext<SampleDbContext>(options =>
                    {
                        options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                    });
                });
            });
            client = applicationFactory.CreateClient();
        }

        public TConfig GetConfiguration<TConfig>()
        {
            // start my service to get the config built...
            this.CreateClient();
            // ... and then cast it to my type
            return _configuration.Get<TConfig>();
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            // shared extra set up goes here
            return base.CreateHost(builder);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            string[] tablesToClean = { "Customer", "BankAccount", "CustomerCreatedEvent" };
            using SqlConnection connection = new(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            foreach (string table in tablesToClean)
            {
                using SqlCommand cmd = new("Delete " + table, connection);
                cmd.ExecuteNonQuery();
            }
        }


    }
}
