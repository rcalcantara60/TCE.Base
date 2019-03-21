using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TCE.CrossCutting.Dto;

namespace TCE.DomainLayerBase.Base
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        ValidationResultDto Add(TEntity entity);
        Task<ValidationResultDto> AddAsync(TEntity entity);
        ValidationResultDto Update(TEntity entity);
        Task<ValidationResultDto> UpdateAsync(TEntity entity);
        ValidationResultDto Delete(TEntity entity);
        Task<ValidationResultDto> DeleteAsync(TEntity entity);
        ValidationResultDto DeleteWhere(Expression<Func<TEntity, bool>> predicate);
        Task<ValidationResultDto> DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingle(int id);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, bool @readonly = true);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = true);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, bool @readonly = true, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = true, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> All(bool @readonly = true);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true, params Expression<Func<TEntity, object>>[] includeProperties);
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