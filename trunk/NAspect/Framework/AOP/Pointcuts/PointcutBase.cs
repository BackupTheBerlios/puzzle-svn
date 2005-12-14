using System.Collections;
using System.Reflection;

namespace Puzzle.NAspect.Framework.Aop
{
	public abstract class PointcutBase : IPointcut
	{
		private IList interceptors = new ArrayList();

		public IList Interceptors
		{
			get { return interceptors; }
			set { interceptors = value; }
		}

		public abstract bool IsMatch(MethodBase method);
	}
}