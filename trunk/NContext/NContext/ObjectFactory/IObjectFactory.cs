using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public interface IObjectFactory
    {
        IContext Context
        {
            get;
            set;
        }
    }
}
