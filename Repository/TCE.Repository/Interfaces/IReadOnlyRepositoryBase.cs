using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace TCE.Repository.Interfaces
{
    /// <summary>
    /// Para usar com MICRO ORM.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadOnlyRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
