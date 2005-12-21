using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace KumoUnitTests.Interceptors
{
	public class ChangeRefParamValueInterceptor : IInterceptor
	{
		public object HandleCall(MethodInvocation call)
		{
			object res = call.Proceed();

			InterceptedParameter param = (InterceptedParameter) call.Parameters[0];
			param.Value = "some changed value";

			return res;
		}
	}
}