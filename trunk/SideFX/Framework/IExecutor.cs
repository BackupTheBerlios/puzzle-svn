using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework.Parsing;
using Puzzle.SideFX.Framework.Execution;

namespace Puzzle.SideFX.Framework
{
    public interface IExecutor
    {
        void OnBeginning(object sender, EngineEventArgs e);

        void OnExecuting(object sender, ExecutionCancelEventArgs e);

        void OnExecuted(object sender, ExecutionEventArgs e);

        void OnCommitting(object sender, EngineEventArgs e);

        void OnAborting(object sender, EngineEventArgs e);
    }
}
