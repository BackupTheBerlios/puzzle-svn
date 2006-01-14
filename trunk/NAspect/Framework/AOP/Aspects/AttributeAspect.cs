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
using System.Reflection.Emit;

namespace Puzzle.NAspect.Framework.Aop
{
	/// <summary>
	/// Summary description for Aspect.
	/// </summary>
	public class AttributeAspect : AspectBase
	{
		public Type AttributeType;

		#region Aspect

		public AttributeAspect(string Name, Type attributeType, IList mixins, IList pointcuts)
		{
			this.Name = Name;
			this.AttributeType = attributeType;
			this.Mixins = mixins;
			this.Pointcuts = pointcuts;

		}

		#endregion

		#region Aspect

		public AttributeAspect(string Name, Type attributeType, Type[] mixins, IPointcut[] pointcuts)
		{
			this.Name = Name;
			this.AttributeType = attributeType;
			this.Mixins = new ArrayList(mixins);
			this.Pointcuts = new ArrayList(pointcuts);
		}

		#endregion

		#region Aspect

		public AttributeAspect(string Name, Type attributeType, string TargetMethodsignature, IInterceptor Interceptor)
		{
			this.Name = Name;
			this.AttributeType = attributeType;
			this.Pointcuts.Add(new SignaturePointcut(TargetMethodsignature, Interceptor));
		}

		#endregion

		public override bool IsMatch(Type type)
		{
			Type tmp = type;
			while (tmp.Assembly is AssemblyBuilder)
				tmp = tmp.BaseType;

			if (tmp.GetCustomAttributes(AttributeType, true).Length > 0)
				return true;
			else
				return false;
		}
	}
}