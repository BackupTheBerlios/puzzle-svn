using System.Collections;
using System.Reflection;
using Puzzle.NAspect.Framework.Tools;

namespace Puzzle.NAspect.Framework.Aop
{
	public class SignaturePointcut : PointcutBase
	{
		public string TargetMethodSignature;

		#region Pointcut

		public SignaturePointcut(string targetMethodSignature, IList interceptors)
		{
			this.TargetMethodSignature = targetMethodSignature;
			this.Interceptors = interceptors;
		}

		#endregion

		#region Pointcut

		public SignaturePointcut(string targetMethodSignature, IInterceptor[] interceptors)
		{
			this.TargetMethodSignature = targetMethodSignature;
			this.Interceptors = new ArrayList(interceptors);
		}

		#endregion

		#region Pointcut

		public SignaturePointcut(string targetMethodSignature, IInterceptor interceptor)
		{
			this.TargetMethodSignature = targetMethodSignature;
			this.Interceptors = new ArrayList(new IInterceptor[] {interceptor});
		}

		#endregion

		public override bool IsMatch(MethodBase method)
		{
			string methodsignature = AopTools.GetMethodSignature(method);
			if (Text.IsMatch(methodsignature, TargetMethodSignature))
				return true;
			else
				return false;
		}


	}
}