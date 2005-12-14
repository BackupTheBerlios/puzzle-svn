using System;
using System.Collections;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace CacheSample
{
	public class CacheInterceptor : IInterceptor
	{
		private Hashtable cache = new Hashtable();

		public object HandleCall(MethodInvokation call)
		{
			string key = call.ValueSignature;
			if (!cache.ContainsKey(key))
			{				
				cache[key] = call.Proceed();
				Console.WriteLine("adding result to cache") ;
			}
			else
			{
				Console.WriteLine("result fetched from cache") ;
			}

			return cache[key];
		}
	}
}