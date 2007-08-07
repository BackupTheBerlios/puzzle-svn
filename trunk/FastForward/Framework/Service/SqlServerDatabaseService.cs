using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Puzzle.SideFX.Framework;

namespace Puzzle.FastForward.Framework.Service
{
    public class SqlServerDatabaseService : DatabaseServiceBase
    {
        public SqlServerDatabaseService(IEngine engine) : base(engine)
        {
        }

        public override IDbConnection GetConnection(string connectionString)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

    }
}
