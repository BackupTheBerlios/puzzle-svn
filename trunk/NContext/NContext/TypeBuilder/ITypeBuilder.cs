using System;
using System.Collections.Generic;
using System.Text;

namespace Mojo
{
    public interface ITypeBuilder
    {
        Type BuildType(Type baseType);
    }
}
