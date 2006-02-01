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
using Puzzle.NAspect.Framework.Aop;

namespace Puzzle.NAspect.Framework
{
    class BeforeInterceptorHandler : IInterceptor
    {
        private IBeforeInterceptor beforeInterceptor;
        public BeforeInterceptorHandler(IBeforeInterceptor beforeInterceptor)
        {
            this.beforeInterceptor = beforeInterceptor;
        }

        public object HandleCall(MethodInvocation call)
        {
            BeforeMethodInvocation beforeCall = new BeforeMethodInvocation(call);
            beforeInterceptor.BeforeCall(beforeCall);
            object res = call.Proceed();            
            return res;
        }
    }
}
