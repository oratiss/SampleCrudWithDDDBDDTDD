using Infrastructure.Persistence.MSSQL.Models;
using Infrastructure.Persistence.MSSQL.RepositoryAbstractions;
using System.Linq.Expressions;
using Infrastructure.Persistence.MSSQL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.MSSQL.Repositories
{
    public class BankAccountWritableRepository: IBankAccountWritableRepository
    {
        private readonly SampleDbContext _dbContext;

        public BankAccountWritableRepository(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BankAccount> Get(long key)
        {
            var existingBankAccount = await _dbContext.BankAccounts.FindAsync(key);
            return existingBankAccount!;
        }

        public Task<IQueryable<BankAccount>> GetAll()
        {
            var allBankAccounts = _dbContext.BankAccounts.AsQueryable();
            return Task.FromResult(allBankAccounts);
        }

        public async Task<BankAccount> Add(BankAccount bankAccount)
        {
            if (bankAccount.Id != 0)
            {
                throw new InvalidOperationException("Adding entity must be new and with value 0 of Id.");
            }

            try
            {
                if (_dbContext.Customers.Any())
                {
                    var existingBankAccounts = _dbContext.BankAccounts.Where(x =>
                        $"{x.CustomerVoForBankAccount.FullName}-{x.CustomerVoForBankAccount.DateOfBirth}" == $"{bankAccount.CustomerVoForBankAccount.FullName.Trim().ToLower()}-{bankAccount.CustomerVoForBankAccount.DateOfBirth}");
                    if (existingBankAccounts.Any())
                    {
                        throw new Exception("One or more BankAccounts with exact entered full name and birthdate already exist.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            bankAccount.CustomerVoForBankAccount.FullName = bankAccount.CustomerVoForBankAccount.FullName.Trim().ToLower();
            var addedBankAccountEntry = await _dbContext.AddAsync(bankAccount);
            await _dbContext.SaveChangesAsync();
            return addedBankAccountEntry.Entity;
        }

        public async Task<BankAccount> Update(BankAccount bankAccount)
        {
            var existingBankAccount = await _dbContext.BankAccounts.FindAsync(bankAccount.Id);
            if (existingBankAccount is null)
            {
                throw new Exception("There is no BankAccount With Given Id.");
            }

            var existingBankAccounts = _dbContext.BankAccounts.Where(x => $"{x.CustomerVoForBankAccount.FullName}-{x.CustomerVoForBankAccount.DateOfBirth}" == $"{bankAccount.CustomerVoForBankAccount.FullName.Trim().ToLower()}-{bankAccount.CustomerVoForBankAccount.DateOfBirth}");
            if (existingBankAccounts.Any())
            {
                throw new Exception("A BankAccount with exact entered full name and birthdate already exists.");
            }
            
            bankAccount.CustomerVoForBankAccount.FullName = bankAccount.CustomerVoForBankAccount.FullName.Trim().ToLower();
            var updateBankAccount = _dbContext.Update(bankAccount);
            return updateBankAccount.Entity;
        }

        public async Task Delete(long key)
        {
            var existingBankAccount = await _dbContext.BankAccounts.FindAsync(key);
            if (existingBankAccount is null)
            {
                throw new Exception("There is no bank account With Given Id.");
            }

            //Todo: It's better to make it logical delete (add default filter in context configuration files)
            _dbContext.BankAccounts.Remove(existingBankAccount);
        }

        public IQueryable<BankAccount> SearchBankAccounts<TOrderByKey>(Expression<Func<BankAccount, bool>> predicate, Expression<Func<BankAccount, TOrderByKey>> orderBy)
        {
            var bankAccounts = _dbContext.BankAccounts.Where(predicate).OrderBy(orderBy).AsQueryable();
            return bankAccounts;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
