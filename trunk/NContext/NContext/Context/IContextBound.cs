using System;
using System.Collections.Generic;
using System.Text;

namespace Mojo
{
    public interface IContextBound
    {
        IContext Context
        {
            get;
            set;
        }
    }
}
