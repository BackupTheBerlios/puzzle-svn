using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public class ObjectFactoryInfo 
    {
        public string DisplayName { get; set; }
        public FactoryDelegate FactoryDelegate { get; set; }
        public InstanceMode InstanceMode { get; set; }
    }
}
