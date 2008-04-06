using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerationStudio.Attributes
{
    [AttributeUsage (AttributeTargets.Class,AllowMultiple=true)]
    public class ElementParentAttribute : Attribute
    {
        public Type ParentType { get; set; }
        public ElementParentAttribute(Type parent)
        {
            ParentType = parent;
        }
    }
}
