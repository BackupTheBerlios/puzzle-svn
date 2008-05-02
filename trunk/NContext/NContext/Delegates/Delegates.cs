using System;
using System.Collections.Generic;
using System.Text;


namespace Mojo
{
    public delegate object FactoryDelegate();
    public delegate void ConfigureDelegate(object item);
}
