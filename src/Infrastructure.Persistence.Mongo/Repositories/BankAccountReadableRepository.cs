using Infrastructure.Persistence.Mongo.Models;
using Infrastructure.Persistence.Mongo.RepositoryAbstractions;
using System.Linq.Expressions;
using Infrastructure.Persistence.Mongo.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Mongo.Repositories
{
    public class BankAccountReadableRepository : IBankAccountReadableRepository
    {
        private readonly IMongoCollection<BankAccount> _bankAccountCollection;

        public BankAccountReadableRepository(IOptions<MongoDbConfiguration> mongoDbOptions)
        {
            var mongoClient = new MongoClient(mongoDbOptions.Value.ConnectionUri);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbOptions.Value.DatabaseName);
            _bankAccountCollection = mongoDatabase.GetCollection<BankAccount>(mongoDbOptions.Value.BankAccountCollectionName);
        }

        public async Task<BankAccount> Get(long key)
        {
            var bankaccount = await _bankAccountCollection.Find(x => x.Id == key).SingleOrDefaultAsync();
            if (bankaccount is null)
            {
                throw new Exception("BankAccount Not Found");
            }
            return bankaccount;
        }

        public async Task<IQueryable<BankAccount>> GetAll()
        {
            var allBankAccounts = await _bankAccountCollection.Find(x => true).ToListAsync();

            if (!allBankAccounts.Any())
            {
                throw new Exception("BankAccounts not found");
            }
            return allBankAccounts.AsQueryable();
        }

        public async Task<BankAccount> Add(BankAccount bankAccount)
        {
            bankAccount.FullName = bankAccount.FullName.Trim().ToLower();
            await _bankAccountCollection.InsertOneAsync(bankAccount);
            var existingCustomer = await _bankAccountCollection.Find(x => x.FullName == bankAccount.FullName.Trim().ToLower()).SingleOrDefaultAsync();
            return existingCustomer;
        }

        public async Task<BankAccount> Update(BankAccount bankAccount)
        {
            var existingBankAccount = await _bankAccountCollection.Find(u => u.Id == bankAccount.Id).SingleOrDefaultAsync();

            if (existingBankAccount is null)
            {
                throw new Exception("BankAccount Not Found");
            }

            existingBankAccount.FullName = bankAccount.FullName.Trim().ToLower();
            existingBankAccount.DateOfBirth = bankAccount.DateOfBirth;
            existingBankAccount.AccountNumber = bankAccount.AccountNumber;
            await _bankAccountCollection.ReplaceOneAsync(u => u.Id == bankAccount.Id, existingBankAccount);
            var updatedBankAccount = await _bankAccountCollection.Find(x => x.FullName == bankAccount.FullName.Trim().ToLower()).SingleOrDefaultAsync();
            return updatedBankAccount;

        }

        public async Task Delete(long key)
        {
            var user = await _bankAccountCollection.Find(u => u.Id == key).SingleOrDefaultAsync();
            if (user is null)
            {
                throw new Exception("BankAccount Not Found");
            }
            await _bankAccountCollection.DeleteOneAsync(u => u.Id == key);
        }

        public async Task<IQueryable<BankAccount>> SearchForBankAccounts<TOrderByKey>(Expression<Func<BankAccount, bool>> predicate, Expression<Func<BankAccount, TOrderByKey>> orderBy)
        {
            var bankAccounts = (await _bankAccountCollection.Find(predicate).ToListAsync()).AsQueryable()
                .OrderBy(orderBy);
            return bankAccounts;
        }
    }
}
