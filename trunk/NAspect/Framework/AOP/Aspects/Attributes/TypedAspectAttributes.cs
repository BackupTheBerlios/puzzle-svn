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
    [AttributeUsage ( AttributeTargets.Method )]
	public class InterceptorAttribute : Attribute
	{
        #region Property Index
        private int index;
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


    [AttributeUsage(AttributeTargets.Class)]
    public class MixinAttribute : Attribute
    {
        #region Property MixinType
        private Type mixinType;
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

        public MixinAttribute(Type mixinType)
        {
            this.MixinType = mixinType;
        }
    }

    public class AspectTargetAttribute : Attribute
    {
        #region Property TargetAttribute
        private Type targetAttribute;
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
