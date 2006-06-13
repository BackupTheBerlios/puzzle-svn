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
using Puzzle.NAspect.Framework.Tools;
using Puzzle.NAspect.Framework.Interception;

namespace Puzzle.NAspect.Framework.Aop
{
	/// <summary>
	/// Aspect that matches target types based on wildcard signatures.
    /// ? for ignoring single characters
    /// * for ignoring one or more characters
	/// </summary>
	public class SignatureAspect : GenericAspectBase
	{
        /// <summary>
        /// Signature of the type to match.
        /// ? for ignoring single characters
        /// * for ignoring one or more characters
        /// </summary>
		public string TargetTypeSignature;


        /// <summary>
        /// Signature aspect ctor.
        /// </summary>
        /// <param name="name">Name of the aspect</param>
        /// <param name="targetName">Signature of the target type</param>
        /// <param name="mixins">Untyped list of <c>System.Type</c>s to mixin</param>
        /// <param name="pointcuts">Untyped list of IPointcut instances</param>
		public SignatureAspect(string name, string targetName, IList mixins, IList pointcuts)
		{
			this.Name = name;
			this.TargetTypeSignature = targetName;
			this.Mixins = mixins;
			this.Pointcuts = pointcuts;

		}

        /// <summary>
        /// Signature aspect ctor.
        /// </summary>
        /// <param name="name">Name of the aspect</param>
        /// <param name="targetName">Signature of the target type</param>
        /// <param name="mixins">Array of <c>System.Type</c>s to mixin</param>
        /// <param name="pointcuts">Array of IPointcut instances</param>
		public SignatureAspect(string name, string targetName, Type[] mixins, IPointcut[] pointcuts)
		{
			this.Name = name;
			this.TargetTypeSignature = targetName;
			this.Mixins = new ArrayList(mixins);
			this.Pointcuts = new ArrayList(pointcuts);
		}

        /// <summary>
        /// Signature aspect ctor.
        /// </summary>
        /// <param name="name">Name of the aspect.</param>
        /// <param name="targetName">Signature of the target type.</param>
        /// <param name="TargetMethodsignature">Signature of the target methods.</param>
        /// <param name="Interceptor">Single <c>IInterceptor</c> that should intercept the matched methods.</param>
        public SignatureAspect(string name, string targetName, string TargetMethodsignature, IInterceptor Interceptor)
		{
			this.Name = name;
			this.TargetTypeSignature = targetName;
			this.Pointcuts.Add(new SignaturePointcut(TargetMethodsignature, Interceptor));
		}

        /// <summary>
        /// Signature aspect ctor.
        /// </summary>
        /// <param name="name">Name of the aspect</param>
        /// <param name="TargetType">Specific Type to which the aspect should be applied.</param>
        /// <param name="mixins">Untyped list of <c>System.Type</c>s to mixin</param>
        /// <param name="pointcuts">Untyped list of IPointcut instances</param>
		public SignatureAspect(string name, Type TargetType, IList mixins, IList pointcuts)
		{
			this.Name = name;
			this.TargetTypeSignature = TargetType.FullName;
			this.Mixins = mixins;
			this.Pointcuts = pointcuts;
		}

        /// <summary>
        /// Signature aspect ctor.
        /// </summary>
        /// <param name="name">Name of the aspect</param>
        /// <param name="TargetType">Specific Type to which the aspect should be applied.</param>
        /// <param name="mixins">Array of <c>System.Type</c>s to mixin</param>
        /// <param name="pointcuts">Array of IPointcut instances</param>
		public SignatureAspect(string name, Type TargetType, Type[] mixins, IPointcut[] pointcuts)
		{
			this.Name = name;
			this.TargetTypeSignature = TargetType.FullName;
			this.Mixins = new ArrayList(mixins);
			this.Pointcuts = new ArrayList(pointcuts);
		}

        /// <summary>
        /// Signature aspect ctor.
        /// </summary>
        /// <param name="name">Name of the aspect</param>
        /// <param name="TargetType">Specific Type to which the aspect should be applied.</param>
        /// <param name="TargetMethodsignature">Signature of the target methods.</param>
        /// <param name="Interceptor">Single <c>IInterceptor</c> that should intercept the matched methods.</param>
		public SignatureAspect(string name, Type TargetType, string TargetMethodsignature, IInterceptor Interceptor)
		{
			this.Name = name;
			this.TargetTypeSignature = TargetType.FullName;
			this.Pointcuts.Add(new SignaturePointcut(TargetMethodsignature, Interceptor));
		}

        /// <summary>
        /// Implementation of AspectBase.IsMatch
        /// <seealso cref="IGenericAspect.IsMatch"/>
        /// </summary>
        /// <param name="type">Type to match</param>
        /// <returns>true if the aspect should be applied to the type, otherwise false.</returns>
		public override bool IsMatch(Type type)
		{
			Type tmp = type;
			//traverse back in inheritance hierarchy to first non runtime emitted type 
			while (tmp.Assembly is AssemblyBuilder)
				tmp = tmp.BaseType;


			if (Text.IsMatch(tmp.FullName, TargetTypeSignature))
				return true;
			else
				return false;
		}
	}
}