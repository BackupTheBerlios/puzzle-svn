using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public class ObjectFactoryInfo 
    {
        public FactoryDelegate<object> FactoryDelegate { get; set; }
        public ObjectInstanceMode InstanceMode { get; set; }
    }
}
