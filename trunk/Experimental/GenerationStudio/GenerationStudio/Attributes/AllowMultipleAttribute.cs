using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerationStudio.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowMultipleAttribute : Attribute
    {
        public bool Allow { get; set; }

        public AllowMultipleAttribute(bool allow)
        {
            Allow = allow;
        }
    }
}
