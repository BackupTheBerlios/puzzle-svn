using System;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace ConsoleApplication1
{
	public class TraceWriter : IInterceptor
	{
		public object HandleCall(MethodInvokation call)
		{
			Console.WriteLine("precall " + call.ValueSignature);
			object res = call.Proceed();
			Console.WriteLine("postcall " + call.ValueSignature);
			Console.WriteLine("returning {0}", res);
			return res;
		}
	}
}