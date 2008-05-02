using System;
using System.Collections.Generic;
using System.Text;

namespace Mojo
{
    public class ObjectFactoryInfo 
    {
        public string DisplayName { get; set; }
        public FactoryDelegate FactoryDelegate { get; set; }
        public InstanceMode InstanceMode { get; set; }
    }
}
