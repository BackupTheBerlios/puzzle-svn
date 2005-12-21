using System;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace KumoUnitTests.Interceptors
{
	public class AddExceptionInterceptor : IInterceptor
	{
		public object HandleCall(MethodInvocation call)
		{
			Type returnType = call.ReturnType;
			object res = null;
			try
			{
				res = call.Proceed();
			}
			catch (Exception x)
			{
				throw new Exception("added exception", x);
			}

			return res;
		}


	}
}