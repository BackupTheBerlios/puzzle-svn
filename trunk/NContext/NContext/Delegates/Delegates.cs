﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public delegate object FactoryDelegate();
    public delegate void ConfigureDelegate(object item);
}
