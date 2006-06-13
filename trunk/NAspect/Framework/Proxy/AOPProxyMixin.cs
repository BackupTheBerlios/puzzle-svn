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
#if NET2
using Puzzle.NAspect.Debug.Serialization;
#endif

namespace Puzzle.NAspect.Framework
{
    /// <summary>
    /// Implementation of IAopProxy that is mixed into every subclass and interface proxy.    
    /// </summary>
	public class AopProxyMixin : IAopProxy, IProxyAware
	{
        /// <summary>
        /// custom data associated with the proxy instance
        /// </summary>
		private IDictionary data = new Hashtable();

        /// <summary>
        /// Default ctor.
        /// </summary>
		public AopProxyMixin()
		{
		}

        /// <summary>
        /// Custom data associated with the proxy instance
        /// </summary>
		public IDictionary Data
		{
			get { return data; }
		}

        ///// <summary>
        ///// The proxy instance.
        ///// </summary>
        //private IAopProxy target;

        /// <summary>
        /// Assigns a proxy instance to the mixin.
        /// </summary>
        /// <param name="target"></param>
		public void SetProxy(IAopProxy target)
		{
		//	this.target = target;
		}

        /// <summary>
        /// This is one of the key methods of the entire interception process.
        /// This method handles calls from the proxy and redirects them to the interceptors.
        /// </summary>
        /// <param name="target">The proxy instance on which the call was invoked</param>
        /// <param name="methodId">Unique identifier of the method</param>
        /// <param name="parameters">Untyped list of <c>InterceptedParameter</c>s</param>
        /// <param name="returnType">The return type of the invoked method</param>
        /// <returns>The result of the call chain</returns>
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

        /// <summary>
        /// .NET 1.x hack to get the default value of a type.
        /// This is currently not used.
        /// It was used in a very old version of NAspect where you could proxy abstract types and force them to return a default value.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
		public object GetTypeDefaultValue(Type type)
		{
			Array array = Array.CreateInstance(type, 1);
			object value = array.GetValue(0);
			return value;
		}
	}
}