using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Puzzle.SideFX.Framework;

namespace Puzzle.FastForward.Framework.Service
{
    public class SqlServerSqlService : ISqlService
    {
        public SqlServerSqlService(IEngine engine)
        {
            this.engine = engine;
        }

        private IEngine engine;

        public void CreateTableWithPrimaryKeyColumn(IDbTransaction transaction, string name, string columnName)
        {
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();

            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Creating table {0} with primary key column {1}",
                    name, name + "Id"));

            StringBuilder sql = new StringBuilder();
            sql.Append("CREATE TABLE [dbo].[" + name + "] (");
            sql.Append("	[" + columnName + "] [int] IDENTITY (1, 1) NOT NULL");
            sql.Append(") ON [PRIMARY]");

            databaseService.ExecuteNonQuery(transaction, sql.ToString());

            sql = new StringBuilder();
            sql.Append("ALTER TABLE [dbo].[" + name + "] WITH NOCHECK ADD ");
            sql.Append("	CONSTRAINT [" + columnName + "_PK] PRIMARY KEY  CLUSTERED");
            sql.Append("	(");
            sql.Append("		[" + columnName + "]");
            sql.Append("	)  ON [PRIMARY]");

            databaseService.ExecuteNonQuery(transaction, sql.ToString());
        }

        public void CreateColumn(IDbTransaction transaction, string tableName, string columnName, DbType dbType)
        {
            CreateColumn(transaction, tableName, columnName, dbType, true, 255);
        }

        public void CreateColumn(IDbTransaction transaction, string tableName, string columnName, DbType dbType, bool nullable)
        {
            CreateColumn(transaction, tableName, columnName, dbType, nullable, 255);
        }

        public void CreateColumn(IDbTransaction transaction, string tableName, string columnName, DbType dbType, int stringLength)
        {
            CreateColumn(transaction, tableName, columnName, dbType, true, stringLength);
        }

        public void CreateColumn(IDbTransaction transaction, string tableName, string columnName, DbType dbType, bool nullable, int stringLength)
        {
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();

            string sqlType = GetType(dbType, stringLength);

            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Adding {0} column {1} to table {2}", 
                    sqlType, columnName, tableName));

            StringBuilder sql = new StringBuilder();
            sql.Append("ALTER TABLE [dbo].[" + tableName + "] ADD " + columnName + " " + sqlType);
            if (nullable)
                sql.Append(" NULL ");
            else
                sql.Append(" NOT NULL ");

            databaseService.ExecuteNonQuery(transaction, sql.ToString());
        }


        public string GetType(DbType dbType, int stringLength)
        {
            switch (dbType)
            {
                case DbType.AnsiString:
                    return "VARCHAR(" + stringLength.ToString() + ")";
                case DbType.Int32:
                    return "int";
                default:
                    throw new Exception("Unhandled column type " + dbType.ToString());
            }
        }

    }
}
