// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

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

        public SignaturePointcut(string targetMethodSignature, IAroundInterceptor[] interceptors)
		{
			this.TargetMethodSignature = targetMethodSignature;
			this.Interceptors = new ArrayList(interceptors);
		}

		#endregion

		#region Pointcut

		public SignaturePointcut(string targetMethodSignature, IAroundInterceptor interceptor)
		{
			this.TargetMethodSignature = targetMethodSignature;
            this.Interceptors = new ArrayList(new IAroundInterceptor[] { interceptor });
		}

		#endregion

        #region Pointcut

        public SignaturePointcut(string targetMethodSignature, IAfterInterceptor interceptor)
        {
            this.TargetMethodSignature = targetMethodSignature;
            this.Interceptors = new ArrayList(new IAfterInterceptor[] { interceptor });
        }

        #endregion

        #region Pointcut

        public SignaturePointcut(string targetMethodSignature, IBeforeInterceptor interceptor)
        {
            this.TargetMethodSignature = targetMethodSignature;
            this.Interceptors = new ArrayList(new IBeforeInterceptor[] { interceptor });
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