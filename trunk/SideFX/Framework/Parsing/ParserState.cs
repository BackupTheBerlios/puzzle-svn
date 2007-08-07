using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.SideFX.Framework.Parsing
{
    public enum ParserState
    {
        BeginParameterName = 1,
        ParameterName = 2,
        EndParameterName = 3,
        BeginParameterValue = 4,
        ParameterValue = 5,
        EndParameterValue = 6,
    }
}
