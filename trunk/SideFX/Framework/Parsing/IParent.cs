using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.SideFX.Framework.Parsing
{
    public interface IParent
    {
        void AddParameter(Parameter parameter);

        IList<Parameter> GetParameters();
    }
}
