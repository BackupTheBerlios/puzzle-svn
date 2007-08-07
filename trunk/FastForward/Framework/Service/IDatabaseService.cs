using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Puzzle.FastForward.Framework.Service
{
    public interface IDatabaseService
    {
        IDbTransaction EnsureTransaction();

        IDbTransaction EnsureTransaction(string connectionString);

        IDbTransaction EnsureTransaction(IDbConnection connection);

        IDbConnection GetConnection();

        IDbConnection GetConnection(string connectionString);

        IDbTransaction BeginTransaction();

        IDbTransaction BeginTransaction(string connectionString);

        IDbTransaction BeginTransaction(IDbConnection connection);

        int ExecuteNonQuery(IDbTransaction transaction, string sql);

        IDbCommand CreateCommand(IDbTransaction transaction, string sql);

        void CommitEventualTransaction();

        void AbortEventualTransaction();

        DbType GetDbType(Type type, int stringLength);
    }
}
