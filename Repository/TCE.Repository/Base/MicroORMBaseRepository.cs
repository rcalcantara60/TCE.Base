using Dapper;
using System.Collections.Generic;
using System.Data;
using TCE.Repository.Interfaces;

namespace TCE.Repository.Base
{
    public class MicroORMBaseRepository<TEntity> : IMicroORMBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Connection para a execução em banco no contexto do método.
        /// </summary>
        public IDbConnection DbConnectionContext { get; protected set; }
        /// <summary>
        /// Connection para a execução em banco no contexto do repositório.
        /// </summary>
        public IDbConnection DbConnectionDefault { get; protected set; }
        public IDbConnectionFactory DbConnectionFactory { get; private set; }

        public MicroORMBaseRepository(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
            DbConnectionDefault = dbConnectionFactory.CreateDbConnection("ConnectionString");
        }

        public IEnumerable<TEntity> GetAll(string sql)
        {
            return DbConnectionDefault.Query<TEntity>(sql);
        }

        public IEnumerable<TEntity> GetAll(string sql, string connectionsName)
        {
            DbConnectionContext = DbConnectionFactory.CreateDbConnection(connectionsName);
            return DbConnectionContext.Query<TEntity>(sql);
        }

        public IEnumerable<T> GetAll<T>(string sql) where T : class
        {
            return DbConnectionDefault.Query<T>(sql);
        }

        public IEnumerable<T> GetAll<T>(string sql, string connectionsName) where T : class
        {
            DbConnectionContext = DbConnectionFactory.CreateDbConnection(connectionsName);
            return DbConnectionContext.Query<T>(sql);
        }

        public IEnumerable<TEntity> GetAllWithParam(string sql, IEnumerable<dynamic> parameters)
        {
            var retorno = DbConnectionDefault.Query<TEntity>(sql, GetDynamicParameters(parameters));
            return retorno;
        }

        public IEnumerable<T> GetAllWithParam<T>(string sql, IEnumerable<dynamic> parameters) where T : class
        {
            var retorno = DbConnectionDefault.Query<T>(sql, GetDynamicParameters(parameters));
            return retorno;
        }

        public IEnumerable<TEntity> GetAllWithParam(string sql, string connectionsName, IEnumerable<dynamic> parameters)
        {
            DbConnectionContext = DbConnectionFactory.CreateDbConnection(connectionsName);
            var retorno = DbConnectionContext.Query<TEntity>(sql, GetDynamicParameters(parameters));
            return retorno;
        }

        public IEnumerable<T> GetAllWithParam<T>(string sql, string connectionsName, IEnumerable<dynamic> parameters) where T : class
        {
            DbConnectionContext = DbConnectionFactory.CreateDbConnection(connectionsName);
            var retorno = DbConnectionContext.Query<T>(sql, GetDynamicParameters(parameters));
            return retorno;
        }

        private DynamicParameters GetDynamicParameters(IEnumerable<dynamic> parameters)
        {
            var dyParam = new DynamicParameters();

            foreach (var p in parameters)
            {
                dyParam.AddDynamicParams(p);
            }

            return dyParam;
        }
    }
}
