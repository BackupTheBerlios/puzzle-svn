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

namespace Puzzle.NAspect.Framework
{
    public class AfterMethodInvocation
    {
        private MethodInvocation invocation;
        public AfterMethodInvocation(MethodInvocation invocation)
        {
            this.invocation = invocation;
        }

        public IList Parameters
        {
            get
            {
                return invocation.Parameters;
            }
        }

        public MethodBase Method
        {
            get
            {
                return invocation.Method;
            }
        }

        public IAopProxy Target
        {
            get
            {
                return invocation.Target;
            }
        }

        public Type ReturnType
        {
            get
            {
                return invocation.ReturnType;
            }
        }

        public string Signature
        {
            get
            {
                return invocation.Signature;
            }
        }

        public string ValueSignature
        {
            get
            {
                return invocation.ValueSignature;
            }
        }
    }
}
