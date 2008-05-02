using System;
using System.Collections.Generic;
using System.Text;

namespace Mojo
{
    public enum InstanceMode
    {
        PerCall=0,
        PerGraph=1,
        PerThread=2,
        PerContext=3,
    }
}
