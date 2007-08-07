using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.SideFX.Framework.Execution;
using Puzzle.FastForward.Framework.Service;
using System.Collections;

namespace Puzzle.FastForward.Framework.Executors.UpdateObjects
{
    public class UpdateObjectsDomainExecutor : IExecutor
    {
        #region IExecutor Members

        public void OnBeginning(object sender, EngineEventArgs e)
        {
        }

        public void OnExecuting(object sender, ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;

            UpdateObjectsCommand updateObjectsCommand = UpdateObjectsCommand.Evaluate(engine, e.Command);
            if (updateObjectsCommand == null)
                return;

            IObjectService objectService = engine.GetService<IObjectService>();
            IDisplayService displayService = engine.GetService<IDisplayService>();

            Type type = objectService.GetTypeByName(updateObjectsCommand.ClassName);

            IList objects = objectService.GetObjects(type, updateObjectsCommand.Match);

            foreach (object obj in objects)
                displayService.Display(obj);
        }

        public void OnExecuted(object sender, ExecutionEventArgs e)
        {
        }

        public void OnCommitting(object sender, EngineEventArgs e)
        {
        }

        public void OnAborting(object sender, EngineEventArgs e)
        {
        }

        #endregion
    }
}
