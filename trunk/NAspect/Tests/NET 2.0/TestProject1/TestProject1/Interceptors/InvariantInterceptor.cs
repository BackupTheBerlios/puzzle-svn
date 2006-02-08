using System;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace KumoUnitTests.Interceptors
{
    public class InvariantInterceptor : IAfterInterceptor
    {
        public void AfterCall(AfterMethodInvocation call)
        {
            Console.WriteLine("after");
        }
    }
}
