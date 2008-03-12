using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puzzle.NAspect.Framework.Interception;

namespace Tests
{
    public class MyInterceptor : IAroundInterceptor
    {
        public object HandleCall(Puzzle.NAspect.Framework.MethodInvocation call)
        {
            return call.Proceed();
        }
    }
}
