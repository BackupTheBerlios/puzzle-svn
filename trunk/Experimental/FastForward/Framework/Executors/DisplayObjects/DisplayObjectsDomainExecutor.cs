using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.FastForward.Framework.Service;
using System.Collections;

namespace Puzzle.FastForward.Framework.Executors.DisplayObjects
{
    public class DisplayObjectsDomainExecutor : IExecutor
    {
        #region IExecutor Members

        public void OnBeginning(object sender, EngineEventArgs e)
        {
        }

        public void OnExecuting(object sender, Puzzle.SideFX.Framework.Execution.ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;

            DisplayObjectsCommand displayObjectsCommand = DisplayObjectsCommand.Evaluate(engine, e.Command);
            if (displayObjectsCommand == null)
                return;

            IObjectService objectService = engine.GetService<IObjectService>();
            IDisplayService displayService = engine.GetService<IDisplayService>();
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();

            databaseService.EnsureTransaction();

            Type type = objectService.GetTypeByName(displayObjectsCommand.ClassName);

            IList objects = null;
            if (!string.IsNullOrEmpty(displayObjectsCommand.Where))
                objects = objectService.GetObjects(type, displayObjectsCommand.Where);
            else
                objects = objectService.GetObjects(type, displayObjectsCommand.Match);

            if (objects != null)
            {
                foreach (object obj in objects)
                {
                    if (displayObjectsCommand.List)
                        displayService.List(obj);
                    else
                        displayService.Display(obj);
                }
            }
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
