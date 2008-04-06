using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerationStudio.Attributes
{
    [AttributeUsage (AttributeTargets.Method)]
    public class ElementVerbAttribute : Attribute
    {
        public string Name { get; set; }
        public bool Default { get; set; }
        public ElementVerbAttribute(string name)
        {
            Name = name;
        }
    }
}
