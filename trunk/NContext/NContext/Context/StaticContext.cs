using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public partial class Context
    {
        public static Context Configure()
        {
            return new Context();
        }
    }
}
