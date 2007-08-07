using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework.Parsing;

namespace Puzzle.SideFX.Framework.Execution
{
    public class ExecutionCancelEventArgs : ExecutionEventArgs
    {
        public ExecutionCancelEventArgs(Command command)
            : base(command)
        {
        }

        private bool cancel;

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }
	
    }
}
