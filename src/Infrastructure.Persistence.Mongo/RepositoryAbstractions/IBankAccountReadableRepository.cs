using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence.Mongo.Models;

namespace Infrastructure.Persistence.Mongo.RepositoryAbstractions
{
    public interface IBankAccountReadableRepository
    {
        Task<BankAccount> Get(long key);
        Task<IQueryable<BankAccount>> GetAll();
        Task<BankAccount> Add(BankAccount bankAccount);
        Task<BankAccount> Update(BankAccount bankAccount);
        Task Delete(long key);

        Task<IQueryable<BankAccount>> SearchForBankAccounts<TOrderByKey>(Expression<Func<BankAccount, bool>> predicate,
            Expression<Func<BankAccount, TOrderByKey>> orderBy);
    }
}
