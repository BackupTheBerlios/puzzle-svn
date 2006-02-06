// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Collections;
using System.Reflection;
using Puzzle.NAspect.Framework.Aop;
using System.Diagnostics;
using Puzzle.NAspect.Debug.Serialization;

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

        [DebuggerStepThrough()]
        [DebuggerHidden()]
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