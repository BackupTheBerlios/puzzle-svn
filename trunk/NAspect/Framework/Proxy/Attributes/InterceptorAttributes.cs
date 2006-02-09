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
using Puzzle.NAspect.Framework.Aop;
using System.Diagnostics;

namespace Puzzle.NAspect.Framework.Interception
{
    [AttributeUsage (AttributeTargets.Class)]
    public class MayBreakFlow : Attribute
    {
        #region Property Reason 
        private string reason;
        public string Reason
        {
            get
            {
                return this.reason;
            }
            set
            {
                this.reason = value;
            }
        }                        
        #endregion

        public MayBreakFlow()
        {
        }

        public MayBreakFlow(string reason)
        {
            this.Reason = reason;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class IsOptional : Attribute
    {
        public IsOptional()
        {
        }
    }

    public class Throws : Attribute
    {
        #region Property ExceptionType 
        private Type exceptionType;
        public Type ExceptionType
        {
            get
            {
                return this.exceptionType;
            }
            set
            {
                this.exceptionType = value;
            }
        }                        
        #endregion

        public Throws(Type exceptionType)
        {
            this.ExceptionType = exceptionType;
        }
    }

    public class Catches : Attribute
    {
        #region Property ExceptionType
        private Type exceptionType;
        public Type ExceptionType
        {
            get
            {
                return this.exceptionType;
            }
            set
            {
                this.exceptionType = value;
            }
        }
        #endregion

        public Catches(Type exceptionType)
        {
            this.ExceptionType = exceptionType;
        }
    }

    public class ReplaceException : Attribute
    {
        #region Property CatchType 
        private Type catchType;
        public Type CatchType
        {
            get
            {
                return this.catchType;
            }
            set
            {
                this.catchType = value;
            }
        }                        
        #endregion

        #region Property ThrowType 
        private Type throwType;
        public Type ThrowType
        {
            get
            {
                return this.throwType;
            }
            set
            {
                this.throwType = value;
            }
        }                        
        #endregion

        public ReplaceException(Type catchType, Type throwType)
        {
            this.CatchType = catchType;
            this.ThrowType = throwType;
        }
    }


}
