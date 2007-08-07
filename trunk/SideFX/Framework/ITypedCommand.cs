using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework.Parsing;

namespace Puzzle.SideFX.Framework
{
    public interface ITypedCommand
    {
        Command ToCommand(); 
    }
}
