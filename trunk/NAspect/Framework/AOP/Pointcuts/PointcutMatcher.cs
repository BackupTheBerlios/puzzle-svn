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