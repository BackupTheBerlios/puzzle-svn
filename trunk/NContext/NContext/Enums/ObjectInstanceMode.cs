﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public enum InstanceMode
    {
        PerCall=0,
        PerGraph=1,
        PerThread=2,
        PerContext=3,
    }
}
