using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework.Parsing;

namespace Puzzle.SideFX.Framework.Tests
{
    public interface ITypedCommand
    {
        Command ToCommand(); 
    }
}
