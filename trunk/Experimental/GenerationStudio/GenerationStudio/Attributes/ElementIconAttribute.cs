using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerationStudio.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ElementIconAttribute : Attribute
    {
        public string ResourceName { get; set; }
        public ElementIconAttribute(string resourceName)
        {
            ResourceName = resourceName;
        }
    }
}
