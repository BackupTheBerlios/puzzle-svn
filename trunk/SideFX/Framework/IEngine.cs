using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework.Parsing;

namespace Puzzle.SideFX.Framework
{
    public interface IEngine
    {
        void RegisterService<T>(object service);
        void RegisterExecutor(IExecutor executor);

        T GetService<T>();

        void Execute(string text);
        //void Execute(ITypedCommand typedCommand);
        //void Execute(IList<ITypedCommand> typedCommands);
        //void Execute(IList<Command> commands);

    }
}
