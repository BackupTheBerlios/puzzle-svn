using System;
using System.Collections.Generic;
using System.Text;

namespace Mojo
{
    public enum FactoryType
    {
        NamedFactory,
        DefaultForType,
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class FactoryMethodAttribute : Attribute
    {
        public string FactoryId { get; set; }
        public FactoryType RegisterAs { get; set; }
        public InstanceMode InstanceMode { get; set; }


        public FactoryMethodAttribute()
        {
        }

        public FactoryMethodAttribute(FactoryType registerAs)
        {
            RegisterAs = registerAs;
        }

        public FactoryMethodAttribute(FactoryType registerAs,InstanceMode instanceMode)
        {
            RegisterAs = registerAs;
            InstanceMode = instanceMode;
        }

        public FactoryMethodAttribute(string factoryId)
        {
            FactoryId = factoryId;
        }

        public FactoryMethodAttribute(string factoryId, InstanceMode instanceMode)
        {
            FactoryId = factoryId;
            InstanceMode = instanceMode;
        }

        public FactoryMethodAttribute(InstanceMode instanceMode)
        {
            InstanceMode = instanceMode;
        }
    }
}
