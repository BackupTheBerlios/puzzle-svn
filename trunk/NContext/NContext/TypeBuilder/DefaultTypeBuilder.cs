using System;
using System.Collections.Generic;
using System.Text;

namespace Mojo
{
    public class DefaultTypeBuilder : ITypeBuilder
    {


        public virtual Type BuildType(Type baseType)
        {
            return baseType;
        }
    }
}
