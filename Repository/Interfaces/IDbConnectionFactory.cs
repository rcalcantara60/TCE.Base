using System;
using System.Data;
using TCE.Repository.Core;

namespace TCE.Repository.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection(string connectionName);
    }
}
