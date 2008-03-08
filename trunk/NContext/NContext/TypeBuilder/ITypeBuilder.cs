using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public interface ITypeBuilder
    {
        Type BuildType(Type baseType);
    }
}
