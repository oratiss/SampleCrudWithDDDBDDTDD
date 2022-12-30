using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace AcceptanceTestsWithBDD.Abstractions
{
    [AutoRollback]
    public class BaseTest : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // shared extra set up goes here
            return base.CreateHost(builder);
        }
    }
}
