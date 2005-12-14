using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace KumoUnitTests.Interceptors
{
	public class IncreaseReturnValueInterceptor : IInterceptor
	{
		public object HandleCall(MethodInvokation call)
		{
			int res = (int)call.Proceed();

			res ++;
			return res;
		}
	}
}