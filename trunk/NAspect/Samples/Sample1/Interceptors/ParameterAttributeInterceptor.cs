using System.Reflection;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace ConsoleApplication1
{
	public class ParameterAttributeInterceptor : IInterceptor
	{
		public object HandleCall(MethodInvokation call)
		{
			ParameterInfo[] parameterInfos = call.Method.GetParameters();
			int i = 0;
			foreach (ParameterInfo parameterInfo in parameterInfos)
			{
				InterceptedParameter interceptedParameter = (InterceptedParameter) call.Parameters[i];
				object[] attributes = parameterInfo.GetCustomAttributes(typeof (ParameterAttribute), true);
				foreach (ParameterAttribute attribute in attributes)
				{
					attribute.Validate(parameterInfo.Name, parameterInfo.ParameterType, interceptedParameter.Value);
				}
				i++;
			}

			object res = call.Proceed();

			return res;
		}
	}
}