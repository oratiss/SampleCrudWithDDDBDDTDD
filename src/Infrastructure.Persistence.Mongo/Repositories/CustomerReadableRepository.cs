using Infrastructure.Persistence.Mongo.RepositoryAbstractions;
using MongoDB.Driver;
using System.Linq.Expressions;
using Infrastructure.Persistence.Mongo.Configurations;
using Microsoft.Extensions.Options;
using Infrastructure.Persistence.Mongo.Models;

namespace Infrastructure.Persistence.Mongo.Repositories
{
    public class CustomerReadableRepository : ICustomerReadableRepository
    {
        private readonly IMongoCollection<Customer> _customersCollection;
        public CustomerReadableRepository(IOptions<MongoDbConfiguration> mongoDbOptions)
        {
            var mongoClient = new MongoClient(mongoDbOptions.Value.ConnectionUri);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbOptions.Value.DatabaseName);
            _customersCollection = mongoDatabase.GetCollection<Customer>(mongoDbOptions.Value.CustomerCollectionName);
        }
        public async Task<Customer> Get(long key)
        {
            var customer = await _customersCollection.Find(u => u.Id == key).SingleOrDefaultAsync();
            if (customer is null)
            {
                throw new Exception("Customer Not Found");
            }
            return customer;
        }

        public async Task<IQueryable<Customer>> GetAll()
        {
            var allCustomers = await _customersCollection.Find(u => true).ToListAsync();
            if (!allCustomers.Any())
            {
                throw new Exception("Customers Not Found");
            }
            return allCustomers.AsQueryable();
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

            if (existingCustomer is null)
            {
                throw new Exception("Customer Not Found");
            }

            existingCustomer = TrimAndLowerCaseCustomerProps(customer);
            existingCustomer.DateOfBirth = customer.DateOfBirth;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.BankAccountNumber = customer.BankAccountNumber;
            await _customersCollection.ReplaceOneAsync(u => u.Id == customer.Id, existingCustomer);
            var updatedCustomer = await _customersCollection.Find(x => x.Email == customer.Email.Trim().ToLower()).SingleOrDefaultAsync();
            return updatedCustomer;
        }

        public async Task Delete(long key)
        {
            var user = await _customersCollection.Find(u => u.Id == key).SingleOrDefaultAsync();
            if (user is null)
            {
                throw new Exception("Customer Not Found");
            }
            await _customersCollection.DeleteOneAsync(u => u.Id == key);
        }

        public async Task<IQueryable<Customer>> SearchForPeople<TOrderByKey>(Expression<Func<Customer, bool>> predicate,
            Expression<Func<Customer, TOrderByKey>> orderBy)
        {
            var customers = (await _customersCollection.Find(predicate).ToListAsync()).AsQueryable().OrderBy(orderBy);
            return customers;
        }

    }
}
