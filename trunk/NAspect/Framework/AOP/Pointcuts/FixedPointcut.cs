// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Collections;
using System.Reflection;
using Puzzle.NAspect.Framework.Interception;
using Puzzle.NAspect.Framework.Tools;
using Puzzle.NAspect.Framework.Utils;

namespace Puzzle.NAspect.Framework.Aop
{
    /// <summary>
    /// Pointcut that matches method signatures.
    /// </summary>
    public class FixedPointcut : PointcutBase
    {
        public override bool IsMatch(MethodBase method)
        {
            return false;
        }
    }
}