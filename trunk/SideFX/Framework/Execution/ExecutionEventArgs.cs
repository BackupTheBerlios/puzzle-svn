using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework.Parsing;

namespace Puzzle.SideFX.Framework.Execution
{
    public class ExecutionEventArgs : EventArgs
    {
        public ExecutionEventArgs(Command command)
        {
            this.Command = command;
        }

        private Command command;

        public Command Command
        {
            get { return command; }
            set { command = value; }
        }	
    }
}
