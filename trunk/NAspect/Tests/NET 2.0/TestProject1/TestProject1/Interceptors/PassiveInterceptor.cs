using System;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace KumoUnitTests.Interceptors
{
    public class PassiveInterceptor : IInterceptor
    {
        public object HandleCall(MethodInvocation call)
        {
            object res = call.Proceed();
            return res;
        }
    }
}