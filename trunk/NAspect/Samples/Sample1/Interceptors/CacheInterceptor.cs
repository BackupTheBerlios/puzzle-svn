using System.Collections;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace ConsoleApplication1
{
	public class CacheInterceptor : IInterceptor
	{
		private Hashtable cache = new Hashtable();

		public object HandleCall(MethodInvocation call)
		{
			string key = call.ValueSignature;
			if (!cache.ContainsKey(key))
				cache[key] = call.Proceed();

			return cache[key];
		}
	}
}