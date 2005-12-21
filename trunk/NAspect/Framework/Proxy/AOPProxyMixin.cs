using System;
using System.Collections;
using System.Reflection;
using Puzzle.NAspect.Framework.Aop;

namespace Puzzle.NAspect.Framework
{
	public class AopProxyMixin : IAopProxy, IProxyAware
	{
		//	private ProxyHost host;
		private IDictionary data = new Hashtable();

		public AopProxyMixin()
		{
		}

//		public ProxyHost Host
//		{
//			get { return host; }
//			set { host = value; }
//		}

		public IDictionary Data
		{
			get { return data; }
		}

		private IAopProxy target;

		public void SetProxy(IAopProxy target)
		{
			this.target = target;
		}

		public object HandleCall(IAopProxy target, string methodId, IList parameters, Type returnType)
		{
			MethodBase method = (MethodBase) MethodCache.methodLookup[methodId];
			MethodInfo wrappermethod = (MethodInfo) MethodCache.wrapperMethodLookup[methodId];

			IList interceptors = MethodCache.GetInterceptors(methodId);
			MethodInvocation invocation = new MethodInvocation(target, method, wrappermethod, parameters, returnType, interceptors);
			return invocation.Proceed();
		}

		public object GetTypeDefaultValue(Type type)
		{
			Array array = Array.CreateInstance(type, 1);
			object value = array.GetValue(0);
			return value;
		}
	}
}