using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Puzzle.FastForward.Framework.Service
{
    public interface ISqlService
    {
        void CreateTableWithPrimaryKeyColumn(IDbTransaction transaction, string name, string columnName);

        void CreateColumn(IDbTransaction transaction, string tableName, string columnName, DbType dbType);

        void CreateColumn(IDbTransaction transaction, string tableName, string columnName, DbType dbType, int stringLength);

        void CreateColumn(IDbTransaction transaction, string tableName, string columnName, DbType dbType, bool nullable);

        void CreateColumn(IDbTransaction transaction, string tableName, string columnName, DbType dbType, bool nullable, int stringLength);

        string GetType(DbType dbType, int stringLength);
    }
}
