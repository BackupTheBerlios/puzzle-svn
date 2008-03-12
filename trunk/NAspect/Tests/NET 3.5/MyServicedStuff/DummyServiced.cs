using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.EnterpriseServices;

[assembly: ApplicationName("MyDll")]
[assembly: Description("Sample Application for Enterprise Services")]
[assembly: ApplicationActivation(ActivationOption.Library)]

namespace Tests
{
    public class DummyServiced: ServicedComponent
    {
        public DummyServiced()
        {
        }

        public virtual void Foo()
        {
        }
    }
}
