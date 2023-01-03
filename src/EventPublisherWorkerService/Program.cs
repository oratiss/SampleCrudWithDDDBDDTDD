using EventPublisherWorkerService;
using Infrastructure.Persistence.MSSQL.Contexts;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var configuration =services.BuildServiceProvider().GetService<IConfiguration>();
        services.AddDbContextPool<SampleDbContext>(options =>
        {
            options.UseSqlServer(configuration?.GetConnectionString("DefaultConnection"));
        });
        services.AddHostedService<EventPublisherWorker>();
    })
    .Build();

await host.RunAsync();
