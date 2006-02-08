using System;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;

namespace KumoUnitTests.Interceptors
{
    public class SecurityInterceptor : IBeforeInterceptor
    {
        public void BeforeCall(BeforeMethodInvocation call)
        {
            Console.WriteLine("before");
        }
    }
}
