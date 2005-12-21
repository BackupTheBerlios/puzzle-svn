using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace KumoUnitTests.Interceptors
{
	public class ChangeReturnValueInterceptor : IInterceptor
	{
		public object HandleCall(MethodInvocation call)
		{
			object res = call.Proceed();
			res = 1000;
			return res;
		}
	}
}