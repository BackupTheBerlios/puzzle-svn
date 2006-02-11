using System;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework.Interception;

namespace ConsoleApplication1
{
	public class TraceWriter : IAroundInterceptor
	{
		public object HandleCall(MethodInvocation call)
		{
			Console.WriteLine("precall " + call.ValueSignature);
			object res = call.Proceed();
			Console.WriteLine("postcall " + call.ValueSignature);
			Console.WriteLine("returning {0}", res);
			return res;
		}
	}
}