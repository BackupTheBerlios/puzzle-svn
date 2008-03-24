using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;


namespace Puzzle.NContext.Framework
{
    public delegate object FactoryDelegate();
    public delegate void ConfigureDelegate(object item);
}
