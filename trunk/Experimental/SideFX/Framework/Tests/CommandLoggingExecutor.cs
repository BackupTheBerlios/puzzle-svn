using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework.Execution;

namespace Puzzle.SideFX.Framework.Tests
{
    public class CommandLoggingExecutor : IExecutor
    {
        #region IExecutor Members

        public void OnExecuting(object sender, ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            loggingService.WriteToLog(e.Command.ToString());
        }

        public void OnExecuted(object sender, ExecutionEventArgs e)
        {
            //;
        }

        public void OnBeginning(object sender, EngineEventArgs e)
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
