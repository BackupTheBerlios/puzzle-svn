using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public class ObjectFactoryInfo 
    {
        public FactoryDelegate FactoryDelegate { get; set; }
        public InstanceMode InstanceMode { get; set; }
    }
}
