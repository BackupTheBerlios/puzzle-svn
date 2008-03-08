using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public class ObjectConfiguration 
    {
        public Func<object> FactoryDelegate { get; set; }
        public ObjectInstanceMode InstanceMode { get; set; }
    }
}
