using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace KumoUnitTests.Interceptors
{
	public class ExplicitIFaceClonableInterceptor : IInterceptor
	{
		public object HandleCall(MethodInvocation call)
		{
			object res = call.Proceed() ;
			SomeClassWithExplicitIFace some = (SomeClassWithExplicitIFace) res;
			some.SomeLongProp = 1234;
			return some;
		}
	}
}