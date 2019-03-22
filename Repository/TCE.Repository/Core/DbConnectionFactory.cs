using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using TCE.Repository.Interfaces;

namespace TCE.Repository.Core
{

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<string, string> _connDict;

        public DbConnectionFactory(IDictionary<string, string> connDict)
        {
            _connDict = connDict;
        }

        public IDbConnection CreateDbConnection(string connectionName)
        {
            string connectionString = null;
            if (_connDict.TryGetValue(connectionName, out connectionString))
            {
                return new  OracleConnection(connectionString);
            }

            throw new Exception("Não foi possivel criar uma conexão.");
        }
    }
}
