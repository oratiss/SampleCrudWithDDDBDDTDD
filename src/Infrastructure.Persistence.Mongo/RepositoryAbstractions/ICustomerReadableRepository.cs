using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence.Mongo.Models;

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
