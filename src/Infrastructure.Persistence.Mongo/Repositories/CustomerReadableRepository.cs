using Infrastructure.Persistence.Mongo.Models;
using Infrastructure.Persistence.Mongo.RepositoryAbstractions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Mongo.Repositories
{
    public class CustomerReadableRepository : ICustomerReadableRepository
    {
        private readonly IMongoCollection<Customer> _customersCollection;
        public CustomerReadableRepository(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConnection"));
            var mongoDatabase = mongoClient.GetDatabase("SampleDDDBDDTDD_Db");
            _customersCollection = mongoDatabase.GetCollection<Customer>("Customers");
        }
        public async Task<Customer> Get(long key)
        {
            var customer = await _customersCollection.Find(u => u.Id == key).SingleOrDefaultAsync();
            if (customer is not null)
            {
                return customer;
            }
            throw new Exception("Customer Not Found");
        }

        public async Task<IQueryable<Customer>> GetAll()
        {
            var allCustomers = await _customersCollection.Find(u => true).ToListAsync();
            if (allCustomers.Any())
            {
                return allCustomers.AsQueryable();
            }
            throw new Exception("Customers Not Found");
        }

        public async Task<Customer> Add(Customer customer)
        {
            customer = TrimAndLowerCaseCustomerProps(customer);
            await _customersCollection.InsertOneAsync(customer);
            var existingCustomer = await _customersCollection.Find(x => x.Email == customer.Email).SingleOrDefaultAsync();
            return existingCustomer;
        }

        public Customer TrimAndLowerCaseCustomerProps(Customer customer)
        {
            customer.FirstName = customer.FirstName.Trim().ToLower();
            customer.LastName = customer.LastName.Trim().ToLower();
            customer.Email = customer.Email.Trim().ToLower();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            var existingCustomer = await _customersCollection.Find(u => u.Id == customer.Id).SingleOrDefaultAsync();
            if (existingCustomer is not null)
            {
                existingCustomer.FirstName = customer.FirstName.Trim().ToLower();
                existingCustomer.LastName = customer.LastName.Trim().ToLower();
                existingCustomer.DateOfBirth = customer.DateOfBirth;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.Email = customer.Email.Trim().ToLower();
                existingCustomer.BankAccount.Number = customer.BankAccount.Number;
                await _customersCollection.ReplaceOneAsync(u => u.Id == customer.Id, existingCustomer);
                var updatedCustomer = await _customersCollection.Find(x => x.Email == customer.Email).SingleOrDefaultAsync();
                return updatedCustomer;
            }

            throw new Exception("Customer Not Found");
        }

        public async Task Delete(long key)
        {
            var user = await _customersCollection.Find(u => u.Id == key).SingleOrDefaultAsync();
            if (user is not null)
            {
                await _customersCollection.DeleteOneAsync(u => u.Id == key);
            }
            throw new Exception("Customer Not Found");
        }

        public async Task<IQueryable<Customer>> SearchForPeople<TOrderByKey>(Expression<Func<Customer, bool>> predicate,
            Expression<Func<Customer, TOrderByKey>> orderBy)
        {
            var customers = (await _customersCollection.Find(predicate).ToListAsync()).AsQueryable().OrderBy(orderBy);
            return customers;
        }
    }
}
