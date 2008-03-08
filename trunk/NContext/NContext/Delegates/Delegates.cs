using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public delegate T FactoryDelegate <T>();
    public delegate void ConfigureDelegate<T>(T item);
}
