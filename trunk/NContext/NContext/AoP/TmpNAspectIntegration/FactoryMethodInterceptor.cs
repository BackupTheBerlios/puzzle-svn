using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puzzle.NAspect.Framework.Interception;
using Puzzle.NAspect.Framework;

namespace Puzzle.NContext.Framework
{
    public class FactoryMethodInterceptor : IAroundInterceptor
    {
        public object HandleCall(MethodInvocation call)
        {
            ITemplate template = call.Target as ITemplate;
            IContext context = template.Context;

            lock (context.State)
            {
                return call.Proceed();
            }
        }
    }
}
