using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Interception;

namespace NObjectStore
{
    class EntityCtorInterceptor : IAroundInterceptor
    {
        public object HandleCall(Puzzle.NAspect.Framework.MethodInvocation call)
        {
            InterceptedParameter stateParameter = (InterceptedParameter)call.Parameters[0];
            object state = stateParameter.Value;

            IPersistentObject managed = (IPersistentObject)call.Target;
            managed.Context = (Context)state;

            object res = call.Proceed();

            return res;
        }
    }
}
