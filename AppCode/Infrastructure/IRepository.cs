using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QonaqWebApp.AppCode.Infrastructure
{
    public interface IRepository<T>
        where T : class
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);

        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        void Delete(T entity);

        T Edit(T entity);

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);

        T GetById(int id);
        T GetByCode(string id);

        Task<T> GetByIdAsync(int id);

        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);

        int SaveChanges();
        Task<int> SaveChangesAsync();

    }
}
