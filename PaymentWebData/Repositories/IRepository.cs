using PaymentWebEntity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebData.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task AddAsync(T entity);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int? id);
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
