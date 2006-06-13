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

namespace Puzzle.NAspect.Framework.Aop
{
    /// <summary>
    /// For internal use only.
    /// This class loops through all aspects and tries to match them for a given type.
    /// </summary>
	public class AspectMatcher
	{
		public AspectMatcher()
		{
		}

		#region GetAspectsForType

		public IList MatchAspectsForType(Type type, IList aspects)
		{
			IList matches = new ArrayList();
			foreach (IAspect aspect in aspects)
			{
                IGenericAspect tmpAspect;
                if (aspect is IGenericAspect)
                    tmpAspect = (IGenericAspect)aspect;
                else
                    tmpAspect = TypedToGenericConverter.Convert((ITypedAspect)aspect);


                if (tmpAspect.IsMatch(type))
                    matches.Add(aspect);
			}
			return matches;
		}

		#endregion
	}
}