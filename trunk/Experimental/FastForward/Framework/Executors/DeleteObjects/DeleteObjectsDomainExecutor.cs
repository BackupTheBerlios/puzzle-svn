using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.FastForward.Framework.Service;
using Puzzle.SideFX.Framework.Execution;
using System.Collections;

namespace Puzzle.FastForward.Framework.Executors.DeleteObjects
{
    public class DeleteObjectsDomainExecutor : IExecutor
    {

        #region IExecutor Members

        public void OnBeginning(object sender, EngineEventArgs e)
        {
        }

        public void OnExecuting(object sender, ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;

            DeleteObjectsCommand deleteObjectsCommand = DeleteObjectsCommand.Evaluate(engine, e.Command);
            if (deleteObjectsCommand == null)
                return;

            IObjectService objectService = engine.GetService<IObjectService>();
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();

            databaseService.EnsureTransaction();

            Type type = objectService.GetTypeByName(deleteObjectsCommand.ClassName);

            IList objects = null;
            if (!string.IsNullOrEmpty(deleteObjectsCommand.Where))
                objects = objectService.GetObjects(type, deleteObjectsCommand.Where);
            else
                objects = objectService.GetObjects(type, deleteObjectsCommand.Match);

            if (objects != null)
            {
                foreach (object obj in objects)
                {
                    objectService.DeleteObject(obj);
                }
            }
        }

        public void OnExecuted(object sender, Puzzle.SideFX.Framework.Execution.ExecutionEventArgs e)
        {
        }

        public void OnCommitting(object sender, EngineEventArgs e)
        {
            IEngine engine = sender as IEngine;
            IObjectService objectService = engine.GetService<IObjectService>();
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();
            objectService.Commit();
            databaseService.CommitEventualTransaction();
        }

        public void OnAborting(object sender, EngineEventArgs e)
        {
            IEngine engine = sender as IEngine;
            IObjectService objectService = engine.GetService<IObjectService>();
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();
            objectService.Abort();
            databaseService.AbortEventualTransaction();
        }

        #endregion
    }
}
