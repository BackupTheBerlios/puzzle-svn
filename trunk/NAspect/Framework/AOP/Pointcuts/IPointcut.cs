using System.Collections;
using System.Reflection;

namespace Puzzle.NAspect.Framework.Aop
{
	public interface IPointcut
	{
		IList Interceptors { get; }
		bool IsMatch(MethodBase method);
	}
}