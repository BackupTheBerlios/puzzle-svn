using System;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace KumoUnitTests.Interceptors
{
    public class PassiveInterceptor : IInterceptor
    {
        public object HandleCall(MethodInvokation call)
        {
            object res = call.Proceed();
            return res;
        }
    }
}