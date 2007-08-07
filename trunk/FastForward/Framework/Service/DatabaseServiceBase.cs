using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Puzzle.SideFX.Framework;

namespace Puzzle.FastForward.Framework.Service
{
    public abstract class DatabaseServiceBase : IDatabaseService
    {
        public DatabaseServiceBase(IEngine engine)
        {
            this.engine = engine;
        }

        private IEngine engine;

        private IDbTransaction transaction = null;

        #region IDatabaseService Members

        public void CommitEventualTransaction()
        {
            if (this.transaction != null)
            {
                ILoggingService loggingService = engine.GetService<ILoggingService>();
                if (loggingService != null)
                    loggingService.LogInfo(this, "Committing transaction.");

                IDbConnection connection = this.transaction.Connection;
                this.transaction.Commit();
                connection.Dispose();
                this.transaction = null;
            }
        }

        public void AbortEventualTransaction()
        {
            if (this.transaction != null)
            {
                ILoggingService loggingService = engine.GetService<ILoggingService>();
                if (loggingService != null)
                    loggingService.LogInfo(this, "Rolling back transaction.");

                IDbConnection connection = this.transaction.Connection;
                this.transaction.Rollback();
                connection.Dispose();
                this.transaction = null;
            }
        }

        public IDbConnection GetConnection()
        {
            IConfigurationService configurationService = engine.GetService<IConfigurationService>();
            return GetConnection(configurationService.ConnectionString);
        }

        public abstract IDbConnection GetConnection(string connectionString);

        public IDbTransaction EnsureTransaction()
        {
            IConfigurationService configurationService = engine.GetService<IConfigurationService>();
            return EnsureTransaction(configurationService.ConnectionString);
        }

        public IDbTransaction EnsureTransaction(string connectionString)
        {
            return EnsureTransaction(GetConnection(connectionString));
        }

        public IDbTransaction EnsureTransaction(IDbConnection connection)
        {
            if (this.transaction == null)
                BeginTransaction(connection);
            return transaction;
        }

        public IDbTransaction BeginTransaction()
        {
            IConfigurationService configurationService = engine.GetService<IConfigurationService>();
            return BeginTransaction(configurationService.ConnectionString);
        }

        public IDbTransaction BeginTransaction(string connectionString)
        {
            return BeginTransaction(GetConnection(connectionString));
        }

        public IDbTransaction BeginTransaction(IDbConnection connection)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, "Beginning transaction.");

            this.transaction = connection.BeginTransaction();
            return transaction;
        }

        public int ExecuteNonQuery(IDbTransaction transaction, string sql)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Executing non query: {0}", sql));

            IDbCommand command = CreateCommand(transaction, sql);
            return command.ExecuteNonQuery();
        }

        public IDbCommand CreateCommand(IDbTransaction transaction, string sql)
        {
            IDbCommand command = transaction.Connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            return command;
        }

        public DbType GetDbType(Type type, int stringLength)
        {
            if (typeof(string).IsAssignableFrom(type))
                return DbType.AnsiString;
            if (typeof(int).IsAssignableFrom(type))
                return DbType.Int32;
            throw new Exception("Unknown type " + type.ToString());
        }

        #endregion
    }
}
