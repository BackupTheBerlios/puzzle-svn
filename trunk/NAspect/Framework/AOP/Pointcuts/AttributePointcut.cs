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
using Puzzle.NAspect.Framework.Interception;

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
            this.Interceptors = new ArrayList(new IInterceptor[] { interceptor });
		}

		#endregion

        #region Pointcut

        public AttributePointcut(Type attributeType, Delegate interceptor)
        {
            this.AttributeType = attributeType;
            ArrayList arr = new ArrayList();
            arr.Add(interceptor);
            this.Interceptors = arr;
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