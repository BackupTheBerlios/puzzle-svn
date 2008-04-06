using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerationStudio.Attributes
{
    public class ElementNameAttribute : Attribute 
    {
        public string Name { get; set; }

        public ElementNameAttribute(string name)
        {
            Name = name;
        }
    }
}
