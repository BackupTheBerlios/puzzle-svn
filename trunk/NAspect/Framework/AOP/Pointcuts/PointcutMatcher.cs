// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Collections;
using System.Reflection;

namespace Puzzle.NAspect.Framework.Aop
{
	public class PointcutMatcher
	{
		public PointcutMatcher()
		{
		}

		public bool MethodShouldBeProxied(MethodInfo method, IList aspects)
		{
			foreach (IAspect aspect in aspects)
			{
				foreach (IPointcut pointcut in aspect.Pointcuts)
				{
					MethodBase methodbase = method;
					if (pointcut.IsMatch(methodbase))
						return true;
				}
			}

			return false;
		}
	}
}