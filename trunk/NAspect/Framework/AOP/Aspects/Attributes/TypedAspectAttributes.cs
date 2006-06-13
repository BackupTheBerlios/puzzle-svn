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
using System.Reflection;

namespace Puzzle.NAspect.Framework.Aop
{
    /// <summary>
    /// Attribute that can be applied to interceptor methods in a typed aspect.
    /// Interceptor methods must match delegate signatures of either <c>AroundDelegate</c>, <c>BeforeDelegate</c> or <c>AfterDelegate</c> 
    /// <seealso cref="InterceptorAttribute"/>
    /// <seealso cref="MixinAttribute"/>
    /// <seealso cref="AspectTargetAttribute"/>
    /// <seealso cref="MixinAttribute"/>
    /// </summary>
    [AttributeUsage ( AttributeTargets.Method )]
	public class InterceptorAttribute : Attribute
	{
        #region Property Index
        private int index;
        /// <summary>
        /// Call chain index of the interceptor, mark your first interceptor with index=1 , then next with index=2 etc.
        /// </summary>
        public virtual int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }
        #endregion

        #region Property TargetAttribute
        private Type targetAttribute;
        /// <summary>
        /// When a type is matched, every method decorated with an attribute of this type with get the current interceptor applied.
        /// 
        /// <example>
        /// Sample of method that should get the interceptor applied:
        /// <code>
        /// [AttributeThatIWantToPointcutOn]
        /// public virtual void Foo()
        /// {
        /// }
        /// </code>
        /// 
        /// Sample interceptor in your typed aspect:
        /// <code>
        /// [Interceptor(Index=1,TargetAttribute=typeof(AttributeThatIWantToPointcutOn))]
        /// private object MyAroundInterceptor(MethodInvocation call)
        /// {
        ///     return call.Proceed();
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public virtual Type TargetAttribute
        {
            get
            {
                return this.targetAttribute;
            }
            set
            {
                this.targetAttribute = value;
            }
        }
        #endregion

        #region Property TargetSignature
        private string targetSignature;
        /// <summary>
        /// When a type is matched, every method with this signature will be matched.
        /// Valid wildcards are:
        /// ? for ignoring single characters
        /// * for ignoring one or more characters
        /// 
        /// <example>
        /// Sample of method that should get the interceptor applied:
        /// <code>      
        /// public virtual void Foo()
        /// {
        /// }
        /// </code>
        /// 
        /// Sample interceptor in your typed aspect:
        /// <code>
        /// [Interceptor(Index=1,TargetSignature="*void Foo()"))]
        /// private object MyAroundInterceptor(MethodInvocation call)
        /// {
        ///     return call.Proceed();
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public virtual string TargetSignature
        {
            get
            {
                return this.targetSignature;
            }
            set
            {
                this.targetSignature = value;
            }
        }
        #endregion

        public InterceptorAttribute()
        {
            
        }
    }


    /// <summary>
    /// Attribute that can be applied to typed aspects.
    /// <example>
    /// Example of typed aspect:
    /// <code>
    /// [AspectTarget(TargetType=typeof(SomeClassThatGetsThisAspectApplied)]
    /// [Mixin(typeof(MyMixin))]
    /// [Mixin(typeof(MyOther))]
    /// public class MyAspect : ITypedAspect //marker interface only
    /// {
    ///     ...
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="InterceptorAttribute"/>
    /// <seealso cref="MixinAttribute"/>
    /// <seealso cref="AspectTargetAttribute"/>
    /// <seealso cref="MixinAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MixinAttribute : Attribute
    {
        #region Property MixinType
        private Type mixinType;
        /// <summary>
        /// Type of the mixin to be applied to this aspect.
        /// <example>
        /// Example of typed aspect:
        /// <code>
        /// [AspectTarget(TargetType=typeof(SomeClassThatGetsThisAspectApplied)]
        /// [Mixin(typeof(MyMixin))] //mixes in MyMixin on all targets
        /// public class MyAspect : ITypedAspect ...
        /// </code>
        /// </example>
        /// </summary>
        public virtual Type MixinType
        {
            get
            {
                return this.mixinType;
            }
            set
            {
                this.mixinType = value;
            }
        }
        #endregion

        /// <summary>
        /// Mixin attribute Ctor.
        /// <example>
        /// Example of typed aspect:
        /// <code>
        /// [AspectTarget(TargetType=typeof(SomeClassThatGetsThisAspectApplied)]
        /// [Mixin(typeof(MyMixin))] //mixes in MyMixin on all targets
        /// public class MyAspect : ITypedAspect ...
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="mixinType">A type that should be mixed into every target of this aspect</param>
        public MixinAttribute(Type mixinType)
        {
            this.MixinType = mixinType;
        }
    }

    /// <summary>
    /// Attribute that can be applied to typed aspects.
    /// Determines what types should get this aspect applied.
    /// <example>
    /// <code>
    /// [AspectTarget(TargetType=typeof(SomeClass)] //SomeClass will get the MyAspect applied to it
    /// [Mixin(typeof(MyMixin))] 
    /// public class MyAspect : ITypedAspect ...
    /// </code>
    /// </example>
    /// <seealso cref="InterceptorAttribute"/>
    /// <seealso cref="MixinAttribute"/>
    /// <seealso cref="AspectTargetAttribute"/>
    /// <seealso cref="MixinAttribute"/>
    /// </summary>
    public class AspectTargetAttribute : Attribute
    {
        #region Property TargetAttribute
        private Type targetAttribute;
        /// <summary>
        /// Every type decorated with an attribute of this type will get the current aspect applied.
        /// <example>
        /// <code >
        /// [AspectTarget(TargetAttribute=typeof(SomeAttribute)] //every type decorated with SomeAttribute will get this aspect applied to it
        /// public class MyAspect : ITypedAspect ...
        /// </code>
        /// </example>
        /// </summary>
        public virtual Type TargetAttribute
        {
            get
            {
                return this.targetAttribute;
            }
            set
            {
                this.targetAttribute = value;
            }
        }
        #endregion

        #region Property TargetSignature
        private string targetSignature;
        /// <summary>
        /// Every type with a signature that matches this pattern will get the current aspect applied.
        /// <example>
        /// <code >
        /// [AspectTarget(TargetSignature="*SomeClass*")] //every type whose name matches *SomeClass* will get the current aspect applied
        /// public class MyAspect : ITypedAspect ...
        /// </code>
        /// </example>
        /// </summary>
        public virtual string TargetSignature
        {
            get
            {
                return this.targetSignature;
            }
            set
            {
                this.targetSignature = value;
            }
        }
        #endregion

        #region Property TargetType
        private Type targetType;
        /// <summary>
        /// Assigns a single type that should get this aspect applied to it.
        /// <example>
        /// <code>
        /// [AspectTarget(TargetType=typeof(SomeClass)] //SomeClass will get the MyAspect applied to it
        /// [Mixin(typeof(MyMixin))] 
        /// public class MyAspect : ITypedAspect ...
        /// </code>
        /// </example> 
        /// </summary>
        public virtual Type TargetType
        {
            get
            {
                return this.targetType;
            }
            set
            {
                this.targetType = value;
            }
        }
        #endregion
    }
}
