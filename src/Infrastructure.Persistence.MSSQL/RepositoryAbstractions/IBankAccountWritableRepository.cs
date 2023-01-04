using Infrastructure.Persistence.MSSQL.Models;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.MSSQL.RepositoryAbstractions
{
    public interface IBankAccountWritableRepository
    {
        Task<BankAccount> Get(long key);
        Task<IQueryable<BankAccount>> GetAll();
        Task<BankAccount> Add(BankAccount bankAccount);
        Task<BankAccount> Update(BankAccount bankAccount);
        Task Delete(long key);
        IQueryable<BankAccount> SearchBankAccounts<TOrderByKey>(Expression<Func<BankAccount, bool>> predicate,
            Expression<Func<BankAccount, TOrderByKey>> orderBy);
        Task Save();
    }
}
