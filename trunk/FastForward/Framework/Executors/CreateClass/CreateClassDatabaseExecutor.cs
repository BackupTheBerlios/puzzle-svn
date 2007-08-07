using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.FastForward.Framework.Service;
using System.Data;
using Puzzle.SideFX.Framework.Execution;

namespace Puzzle.FastForward.Framework.Executors.CreateClass
{
    public class CreateClassDatabaseExecutor : IExecutor
    {

        #region IExecutor Members

        public void OnExecuting(object sender, ExecutionCancelEventArgs e)
        {
            CreateClassCommand createClassCommand = CreateClassCommand.Evaluate(e.Command);
            if (createClassCommand == null)
                return;

            IEngine engine = sender as IEngine;
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();
            ISqlService sqlService = engine.GetService<ISqlService>();

            //Begins a new transaction or gets the current transaction
            IDbTransaction transaction = databaseService.EnsureTransaction();

            //Create the database table with the primary key column
            sqlService.CreateTableWithPrimaryKeyColumn(transaction, createClassCommand.TableName, createClassCommand.Name + "Id");
        }

        public void OnExecuted(object sender, ExecutionEventArgs e)
        {
            ;
        }


        public void OnBeginning(object sender, EngineEventArgs e)
        {
            //We don't start a transaction against the database here, since we may
            //not always deal with commands working against the database. Thus we
            //start our transactions lazily in the OnExecuting method whenever a
            //create class command is found. 
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
