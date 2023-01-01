using Infrastructure.Persistence.MSSQL.Contexts;
using Infrastructure.Persistence.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastructure.Persistence.MSSQL.RepositoryAbstractions;

namespace Infrastructure.Persistence.MSSQL.Repositories
{
    public class CustomerWritableRepository : ICustomerWritableRepository
    {
        private readonly SampleDbContext _dbContext;

        public CustomerWritableRepository(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> Get(long key)
        {
            var existingCustomer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == key);
            return existingCustomer!;
        }

        public Task<IQueryable<Customer>> GetAll()
        {
            var allCustomers = _dbContext.Customers.AsQueryable();
            return Task.FromResult(allCustomers);
        }

        public async Task<Customer> Add(Customer customer)
        {
            if (customer.Id != 0)
            {
                throw new InvalidOperationException("Adding entity must be new and with value 0 of Id.");
            }

            try
            {
                if (_dbContext.Customers.Any())
                {
                    var existingCustomer = await _dbContext.Customers.SingleOrDefaultAsync(x =>
                    $"{x.FirstName}-{x.LastName}-{x.DateOfBirth}" == $"{customer.FirstName.Trim().ToLower()}-{customer.LastName.ToLower()}-{customer.DateOfBirth}");
                    if (existingCustomer is not null)
                    {
                        throw new Exception("A Customer with exact entered first name, last name and birthdate already exists.");
                    }

                    existingCustomer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Email == customer.Email.Trim().ToLower());
                    if (existingCustomer is not null)
                    {
                        throw new Exception("A Customer with exact entered email already exists.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            customer = TrimAndLowerCaseCustomerProps(customer);

            var addedCustomerEntry = await _dbContext.AddAsync(customer);
            return addedCustomerEntry.Entity;
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
            var existingCustomer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == customer.Id);
            if (existingCustomer is null)
            {
                throw new Exception("There is no Customer With Given Id.");
            }

            existingCustomer = await _dbContext.Customers.SingleOrDefaultAsync(x => $"{x.FirstName}-{x.LastName}-{x.DateOfBirth}" == $"{customer.FirstName.Trim().ToLower()}-{customer.LastName.ToLower()}-{customer.DateOfBirth}");
            if (existingCustomer is not null)
            {
                throw new Exception("A Customer with exact entered first name, last name and birthdate already exists.");
            }

            existingCustomer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Email == customer.Email.Trim().ToLower() && x.Id != customer.Id);
            if (existingCustomer is not null)
            {
                throw new Exception("A Customer with exact entered email already exists.");
            }

            customer = TrimAndLowerCaseCustomerProps(customer);
            var updatedCustomerEntry = _dbContext.Update(customer);
            return updatedCustomerEntry.Entity;
        }

        public async Task Delete(long key)
        {
            var existingCustomer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == key);
            if (existingCustomer is null)
            {
                throw new Exception("There is no Customer With Given Id.");
            }

            //Todo: It's better to make it logical delete (add default filter in context configuration files)
            _dbContext.Customers.Remove(existingCustomer);
        }

        public IQueryable<Customer> SearchForPeople<TOrderByKey>(Expression<Func<Customer, bool>> predicate, Expression<Func<Customer, TOrderByKey>> orderBy)
        {
            var items = _dbContext.Customers.Where(predicate).OrderBy(orderBy).AsQueryable();
            return items;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
