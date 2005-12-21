using System;
using System.Collections;
using System.Reflection;
using Puzzle.NAspect.Framework.Aop;

namespace Puzzle.NAspect.Framework
{
	public class MethodInvocation
	{
		public readonly IAopProxy Target;
		public readonly MethodBase Method;
		public readonly IList Parameters;
		public readonly Type ReturnType;
		private readonly MethodInfo EndMethod;
		private IList Interceptors;
		private int Step = 0;

		#region constructor

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

		public object Proceed()
		{
			if (Step < Interceptors.Count)
			{
				//invoke the next interceptor
				IInterceptor interceptor = (IInterceptor) Interceptors[Step];
				Step++;
				return interceptor.HandleCall(this);
			}
			else
			{
				return CallEndMethod();
			}
		}

		#endregion

		#region CallEndMethod

		public object CallEndMethod()
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