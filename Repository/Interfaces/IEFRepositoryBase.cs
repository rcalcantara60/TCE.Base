using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TCE.Repository.Interfaces
{
    public interface IEFRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void DeleteWhere(Expression<Func<TEntity, bool>> predicate);
        Task DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingle(int id);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, bool @readonly = false, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = false, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> All(bool @readonly = false);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = false, params Expression<Func<TEntity, object>>[] includeProperties);
        int Count();
        Task<int> CountAsync();
        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        long LongCount();
        Task<long> LongCountAsync();
        long LongCount(Expression<Func<TEntity, bool>> predicate);
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}