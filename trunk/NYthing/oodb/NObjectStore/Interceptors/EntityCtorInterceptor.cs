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
            //get the context (state) param
            object state = stateParameter.Value;

            IPersistentObject managed = (IPersistentObject)call.Target;

            //assign the context to the target object
            managed.Context = (Context)state;

            Initialize(call.Target);

            object res = call.Proceed();

            CheckIntegrity();

            return res;
        }

        private void Initialize(IAopProxy iAopProxy)
        {

        }

        private void CheckIntegrity()
        {

        }
    }
}
