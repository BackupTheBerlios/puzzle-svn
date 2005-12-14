using System;
using System.Collections;
using System.Reflection;

namespace Puzzle.NAspect.Framework.Aop
{
	public class AttributePointcut : PointcutBase
	{
		public Type AttributeType;

		#region Pointcut

		public AttributePointcut(Type attributeType, IList interceptors)
		{
			this.AttributeType = attributeType;
			this.Interceptors = interceptors;
		}

		#endregion

		#region Pointcut

		public AttributePointcut(Type attributeType, IInterceptor[] interceptors)
		{
			this.AttributeType = attributeType;
			this.Interceptors = new ArrayList(interceptors);
		}

		#endregion

		#region Pointcut

		public AttributePointcut(Type attributeType, IInterceptor interceptor)
		{
			this.AttributeType = attributeType;
			this.Interceptors = new ArrayList(new IInterceptor[] {interceptor});
		}

		#endregion

		public override bool IsMatch(MethodBase method)
		{
			if (method.GetCustomAttributes(AttributeType, true).Length > 0)
				return true;
			else
				return false;
		}


	}
}