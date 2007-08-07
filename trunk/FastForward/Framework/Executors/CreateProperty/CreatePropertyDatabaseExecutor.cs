using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.FastForward.Framework.Service;
using System.Data;
using Puzzle.SideFX.Framework.Execution;

namespace Puzzle.FastForward.Framework.Executors.CreateProperty
{
    public class CreatePropertyDatabaseExecutor : IExecutor
    {
        #region IExecutor Members

        public void OnBeginning(object sender, EngineEventArgs e)
        {
        }

        public void OnExecuting(object sender, ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;

            CreatePropertyCommand createPropertyCommand = CreatePropertyCommand.Evaluate(engine, e.Command);
            if (createPropertyCommand == null)
                return;

            if (createPropertyCommand.Multiplicity == Multiplicity.None)
                CreateProperty(createPropertyCommand, sender, e);
            else
                CreateRelationship(createPropertyCommand, sender, e);
        }

        public void CreateProperty(CreatePropertyCommand createPropertyCommand, object sender, ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;

            IDatabaseService databaseService = engine.GetService<IDatabaseService>();
            ISqlService sqlService = engine.GetService<ISqlService>();
            ISchemaService schemaService = engine.GetService<ISchemaService>();

            //Begins a new transaction or gets the current transaction
            IDbTransaction transaction = databaseService.EnsureTransaction();

            string tableName = schemaService.GetTableForClass(createPropertyCommand.ClassName);
            DbType dbType = databaseService.GetDbType(createPropertyCommand.Type, createPropertyCommand.StringLength);

            //Create the database table with the primary key column
            sqlService.CreateColumn(transaction, tableName, createPropertyCommand.ColumnName, dbType, createPropertyCommand.Nullable);
        }

        public void CreateRelationship(CreatePropertyCommand createPropertyCommand, object sender, ExecutionCancelEventArgs e)
        {
            //IDatabaseService databaseService = engine.GetService<IDatabaseService>();
            //ISqlService sqlService = engine.GetService<ISqlService>();
            //ISchemaService schemaService = engine.GetService<ISchemaService>();

            ////Begins a new transaction or gets the current transaction
            //IDbTransaction transaction = databaseService.EnsureTransaction();

            //string tableName = schemaService.GetTableForClass(createPropertyCommand.ClassName);
            //DbType dbType = databaseService.GetDbType(createPropertyCommand.Type, createPropertyCommand.StringLength);

            ////Create the database table with the primary key column
            //sqlService.CreateColumn(transaction, tableName, createPropertyCommand.ColumnName, dbType, createPropertyCommand.Nullable);
        }

        public void OnExecuted(object sender, Puzzle.SideFX.Framework.Execution.ExecutionEventArgs e)
        {
        }

        public void OnCommitting(object sender, EngineEventArgs e)
        {
            IEngine engine = sender as IEngine;
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();

            databaseService.CommitEventualTransaction();
        }

        public void OnAborting(object sender, EngineEventArgs e)
        {
            IEngine engine = sender as IEngine;
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();

            databaseService.AbortEventualTransaction();
        }

        #endregion
    }
}
