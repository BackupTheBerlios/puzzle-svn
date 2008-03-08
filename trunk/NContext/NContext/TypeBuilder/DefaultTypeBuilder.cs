using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public class DefaultTypeBuilder : ITypeBuilder
    {
        public virtual Type BuildType(Type baseType)
        {
            return baseType;
        }
    }
}
