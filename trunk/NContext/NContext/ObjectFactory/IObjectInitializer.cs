﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public interface IObjectInitializer : IContextBound
    {
        void Initialize();
    }
}
