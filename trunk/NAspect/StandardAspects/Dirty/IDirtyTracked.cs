using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Standard
{
    public interface IDirtyTracked
    {
        bool IsDirty
        {
            get;
            set;
        }
    }
}
