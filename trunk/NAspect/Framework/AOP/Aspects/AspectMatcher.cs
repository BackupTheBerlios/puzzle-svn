using System;
using System.Collections;

namespace Puzzle.NAspect.Framework.Aop
{
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
				if (aspect.IsMatch(type))
					matches.Add(aspect);
			}
			return matches;
		}

		#endregion
	}
}