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
using Puzzle.NAspect.Framework.Interception;

namespace Puzzle.NAspect.Framework
{
    /// <summary>
    /// Representation of a method call.
    /// </summary>
    [DebuggerStepThrough()]
	public class MethodInvocation
	{
        /// <summary>
        /// The object on which the method was invoked.
        /// </summary>
		public readonly IAopProxy Target;
        /// <summary>
        /// The intercepted method. (in the dynamic proxy)
        /// </summary>
		public readonly MethodBase Method;
        /// <summary>
        /// Untyped list of <c>InterceptedParameters</c>.
        /// </summary>
		public readonly IList Parameters;
        /// <summary>
        /// The return type of the method (if available, ctors do not have a return type).
        /// </summary>
		public readonly Type ReturnType;
        /// <summary>
        /// The intercepted methods base implementation. (in the base type)
        /// </summary>
		private readonly MethodInfo EndMethod;
        /// <summary>
        /// Untyped list of <c>IInterceptor</c>'s or <c>BeforeDelegate</c>, <c>AroundDelegate</c> or <c>AfterDelegate</c>
        /// </summary>
		private IList Interceptors;

        /// <summary>
        /// current interception chain step. (current interceptor index)
        /// </summary>
		private int Step = 0;

		#region constructor

        /// <summary>
        /// Ctor for MethodInvocation
        /// </summary>
        /// <param name="target">The object on which the method was invoked.</param>
        /// <param name="method">The intercepted method. (in the dynamic proxy)</param>
        /// <param name="endMethod">The intercepted methods base implementation. (in the base type)</param>
        /// <param name="parameters">Untyped list of <c>InterceptedParameters</c>.</param>
        /// <param name="returnType">The return type of the method (if available, ctors do not have a return type).</param>
        /// <param name="interceptors">Untyped list of <c>IInterceptor</c>'s or <c>BeforeDelegate</c>, <c>AroundDelegate</c> or <c>AfterDelegate</c></param>
        [DebuggerStepThrough()]
		public MethodInvocation(IAopProxy target, MethodBase method, MethodInfo endMethod, IList parameters, Type returnType, IList interceptors)
		{
			this.Target = target;
			this.Method = method;
			this.EndMethod = endMethod;
			this.Parameters = parameters;
			this.ReturnType = returnType;
			this.Interceptors = interceptors;
		}

		#endregion

		#region Proceed


        /// <summary>
        /// Executes the next step of the interception chain.
        /// </summary>
        /// <returns>The result of the next interceptor or base implementation</returns>
        [DebuggerStepThrough ()]
        [DebuggerHidden ()]
		public object Proceed()
		{
			if (Step < Interceptors.Count)
			{
                object currentInterceptor = Interceptors[Step];
                if (currentInterceptor is IAfterInterceptor)
                {
                    return InterceptAfter(currentInterceptor);
                }
                else if (currentInterceptor is IBeforeInterceptor)
                {
                    return InterceptBefore(currentInterceptor);
                }
                else if (currentInterceptor is IAroundInterceptor)
                {
                    return InterceptAround(currentInterceptor);
                }
                else if (currentInterceptor is AfterDelegate)
                {
                    return InterceptAfterDelegate(currentInterceptor);
                }
                else if (currentInterceptor is BeforeDelegate)
                {
                    return InterceptBeforeDelegate(currentInterceptor);
                }
                else if (currentInterceptor is AroundDelegate)
                {
                    return InterceptAroundDelegate(currentInterceptor);
                }
                else
                {
                    throw new Exception("Unknown interceptor type");
                }
                
			}
			else
			{
				return CallEndMethod();
			}
		}

        [DebuggerStepThrough()]
        [DebuggerHidden()]
        private object InterceptAroundDelegate(object currentInterceptor)
        {
            AroundDelegate interceptor = (AroundDelegate)currentInterceptor;
            Step++;
            return interceptor(this);
        }

        [DebuggerStepThrough()]
        [DebuggerHidden()]
        private object InterceptBeforeDelegate(object currentInterceptor)
        {
            BeforeDelegate interceptor = (BeforeDelegate)currentInterceptor;
            interceptor(new BeforeMethodInvocation(this));
            Step++;
            object res = this.Proceed();

            return res;
        }

        [DebuggerStepThrough()]
        [DebuggerHidden()]
        private object InterceptAfterDelegate(object currentInterceptor)
        {
            AfterDelegate interceptor = (AfterDelegate)currentInterceptor;
            Step++;
            object res = this.Proceed();
            interceptor(new AfterMethodInvocation(this));

            return res;
        }

        [DebuggerStepThrough()]
        [DebuggerHidden()]
        private object InterceptAround(object currentInterceptor)
        {
            //invoke the next interceptor
            IAroundInterceptor interceptor = (IAroundInterceptor)currentInterceptor;
            Step++;
            return interceptor.HandleCall(this);
        }

        [DebuggerStepThrough()]
        [DebuggerHidden()]
        private object InterceptBefore(object currentInterceptor)
        {
            IBeforeInterceptor beforeInterceptor = (IBeforeInterceptor)currentInterceptor;
            beforeInterceptor.BeforeCall(new BeforeMethodInvocation(this));
            Step++;
            object res = this.Proceed();

            return res;
        }

        [DebuggerStepThrough()]
        [DebuggerHidden()]
        private object InterceptAfter(object currentInterceptor)
        {
            IAfterInterceptor afterInterceptor = (IAfterInterceptor)currentInterceptor;
            Step++;
            object res = this.Proceed();
            afterInterceptor.AfterCall(new AfterMethodInvocation(this));

            return res;
        }

		#endregion

		#region CallEndMethod

        [DebuggerStepThrough()]
        [DebuggerHidden()]
		private object CallEndMethod()
		{
			if (EndMethod.GetParameters().Length != Parameters.Count)
			{
				object[] parr = new object[Parameters.Count - 1];

				//copy paramvalues into param list
				for (int i = 1; i < Parameters.Count; i++)
					parr[i - 1] = ((InterceptedParameter) Parameters[i]).Value;

				//call the end method
				object result = EndMethod.Invoke(Target, parr);

				//copy back all param values (for out/ref params)
				for (int i = 1; i < Parameters.Count; i++)
					((InterceptedParameter) Parameters[i]).Value = parr[i - 1];

				return result;
			}
			else
			{
				object[] parr = new object[Parameters.Count];

				//copy paramvalues into param list
				for (int i = 0; i < Parameters.Count; i++)
					parr[i] = ((InterceptedParameter) Parameters[i]).Value;

				//call the end method
				object result = EndMethod.Invoke(Target, parr);

				//copy back all param values (for out/ref params)
				for (int i = 0; i < Parameters.Count; i++)
					((InterceptedParameter) Parameters[i]).Value = parr[i];
				return result;
			}
		}

		#endregion

		#region Signature

        /// <summary>
        /// Returns the absolute signature of the call.
        /// </summary>
		public string Signature
		{
			get
			{
				string parameters = "";
				foreach (InterceptedParameter param in Parameters)
					parameters += param.Type.FullName + ",";

				if (parameters != "")
					parameters = parameters.Substring(0, parameters.Length - 1);

				if (this.Method is ConstructorInfo)
				{
					return string.Format("{2} {0}({1})", Method.Name, parameters, this.Target.GetType().FullName);
				}
				else
				{
					return string.Format("{3} {0} {1}({2})", ReturnType == null ? "Void" : ReturnType.ToString(), Method.Name, parameters, this.Target.GetType().FullName);
				}
			}
		}

		#endregion

		#region ValueSignature

        /// <summary>
        /// Returns the value signature of the call.
        /// parameter values are represented with ".ToString()"
        /// </summary>
		public string ValueSignature
		{
			get
			{
				string parameters = "";
				foreach (InterceptedParameter param in Parameters)
				{
					if (param.Value == null)
					{
						parameters += "null,";
					}
					else
					{
						parameters += param.Value.ToString() + ",";
					}
				}

				if (parameters != "")
					parameters = parameters.Substring(0, parameters.Length - 1);

				if (this.Method is ConstructorInfo)
				{
					return string.Format("{2} {0}({1})", Method.Name, parameters, this.Target.GetType().FullName);
				}
				else
				{
					return string.Format("{3} {0} {1}({2})", ReturnType == null ? "Void" : ReturnType.ToString(), Method.Name, parameters, this.Target.GetType().FullName);
				}
			}
		}

		#endregion
	}
}