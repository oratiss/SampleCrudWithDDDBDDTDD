using Infrastructure.Persistence.Mongo.Models;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Mongo.RepositoryAbstractions
{
    public interface ICustomerReadableRepository
    {
        Task<Customer> Get(long key);
        Task<IQueryable<Customer>> GetAll();
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(Customer customer);
        Task Delete(long key);

        Task<IQueryable<Customer>> SearchForPeople<TOrderByKey>(Expression<Func<Customer, bool>> predicate,
            Expression<Func<Customer, TOrderByKey>> orderBy);

    }
}
