using ApplicationService.BankAccounts;
using ApplicationService.Customers;
using Domain.BankAccounts;
using Domain.Customers;
using Infrastructure.Persistence.Mongo.Configurations;
using Infrastructure.Persistence.Mongo.Repositories;
using Infrastructure.Persistence.Mongo.RepositoryAbstractions;
using Infrastructure.Persistence.MSSQL.Contexts;
using Infrastructure.Persistence.MSSQL.Repositories;
using Infrastructure.Persistence.MSSQL.RepositoryAbstractions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SampleDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddScoped<ICustomerWritableRepository, CustomerWritableRepository>();
builder.Services.AddScoped<ICustomerReadableRepository, CustomerReadableRepository>();
builder.Services.AddScoped<ICustomerApplicationService, CustomerApplicationService>();

builder.Services.AddScoped<IBankAccountWritableRepository, BankAccountWritableRepository>();
builder.Services.AddScoped<IBankAccountReadableRepository, BankAccountReadableRepository>();
builder.Services.AddScoped<IBankAccountApplicationService, BankAccountApplicationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace Api
{
    public partial class Program { }
}
