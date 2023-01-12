using ApplicationService.BankAccounts;
using ApplicationService.Customers;
using EventPublisherWorkerService;
using Infrastructure.Persistence.Mongo.Configurations;
using Infrastructure.Persistence.Mongo.Repositories;
using Infrastructure.Persistence.Mongo.RepositoryAbstractions;
using Infrastructure.Persistence.MSSQL.Contexts;
using Infrastructure.Persistence.MSSQL.Repositories;
using Infrastructure.Persistence.MSSQL.RepositoryAbstractions;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        services.AddDbContext<SampleDbContext>(options =>
        {
            options.UseSqlServer(configuration?.GetConnectionString("DefaultConnection"));
        }, ServiceLifetime.Singleton);

        services.Configure<MongoDbConfiguration>(configuration?.GetSection("MongoDB"));

        services.AddSingleton<ICustomerWritableRepository, CustomerWritableRepository>();
        services.AddSingleton<ICustomerReadableRepository, CustomerReadableRepository>();
        services.AddSingleton<ICustomerApplicationService, CustomerApplicationService>();

        services.AddSingleton<IBankAccountWritableRepository, BankAccountWritableRepository>();
        services.AddSingleton<IBankAccountReadableRepository, BankAccountReadableRepository>();
        services.AddSingleton<IBankAccountApplicationService, BankAccountApplicationService>();
        services.AddHostedService<EventPublisherWorker>();
    })
    .Build();

await host.RunAsync();
