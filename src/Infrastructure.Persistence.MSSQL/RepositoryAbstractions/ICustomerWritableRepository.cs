using Infrastructure.Persistence.MSSQL.Models;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.MSSQL.RepositoryAbstractions
{
    public interface ICustomerWritableRepository
    {
        Task<Customer> Get(long key);
        Task<IQueryable<Customer>> GetAll();
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(Customer customer);
        Task Delete(long key);

        IQueryable<Customer> SearchForPeople<TOrderByKey>(Expression<Func<Customer, bool>> predicate,
            Expression<Func<Customer, TOrderByKey>> orderBy);
        Task Save();
    }
}
