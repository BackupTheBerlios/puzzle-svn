using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConfigureMethodAttribute : Attribute
    {
        public string ConfigId { get; set; }
        public Type DefaultForType { get; set; }
    }
}
