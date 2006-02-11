using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Interception;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine e = new Engine("test");
            e.Configuration.Aspects.Add(new SignatureAspect("TestAspect", "*", "*Bar*", new Interceptor()));
            Foo foo = e.CreateProxy<Foo>();
            foo.Bar();

            Console.ReadLine();
        }
    }

    public class Foo
    {
        public virtual void Bar()
        {
            Console.WriteLine("Hello Bar");
        }
    }

    public class Interceptor : IAroundInterceptor
    {

        #region IAroundInterceptor Members

        public object HandleCall(Puzzle.NAspect.Framework.MethodInvocation call)
        {
            Console.WriteLine("before");
            object res = call.Proceed();
            Console.WriteLine("after");
            return res;
        }

        #endregion
    }
}
