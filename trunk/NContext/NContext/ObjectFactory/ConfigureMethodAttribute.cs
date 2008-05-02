using System;
using System.Collections.Generic;
using System.Text;

namespace Mojo
{
    public enum ConfigurationType
    {
        NamedConfiguration,
        DefaultForType,
        AppliesToAll,
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ConfigurationMethodAttribute : Attribute
    {
        public string ConfigId { get; set; }
        public ConfigurationType RegisterAs { get; set; }

        public ConfigurationMethodAttribute()
        {
        }

        public ConfigurationMethodAttribute(ConfigurationType registerAs)
        {
            RegisterAs = registerAs;
        }
    }
}
