
using System.Collections.Generic;


namespace TCE.Repository.Interfaces
{
    public interface IMicroORMBaseRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(string sql);
        IEnumerable<T> GetAll<T>(string sql) where T : class;
        IEnumerable<TEntity> GetAll(string sql, string connectionsName);
        IEnumerable<T> GetAll<T>(string sql, string connectionsName) where T : class;
        IEnumerable<TEntity> GetAllWithParam(string sql, IEnumerable<dynamic> parameters);
        IEnumerable<T> GetAllWithParam<T>(string sql, IEnumerable<dynamic> parameters) where T : class;
        IEnumerable<TEntity> GetAllWithParam(string sql, string connectionsName, IEnumerable<dynamic> parameters);
        IEnumerable<T> GetAllWithParam<T>(string sql, string connectionsName, IEnumerable<dynamic> parameters) where T : class;

    }
}
