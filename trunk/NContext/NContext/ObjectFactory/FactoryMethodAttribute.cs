using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FactoryMethodAttribute : Attribute
    {
        public string FactoryId { get; set; }
        public Type DefaultForType { get; set; }
        public ObjectInstanceMode InstanceMode { get; set; }

        public FactoryMethodAttribute()
        {
        }        
    }
}
