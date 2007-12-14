using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Interception;

namespace NObjectStore
{
    class EntityPropertySetInterceptor : IAroundInterceptor
    {
        public object HandleCall(Puzzle.NAspect.Framework.MethodInvocation call)
        {
            if (call.Method.DeclaringType == typeof(IPersistentObject))
                return call.Proceed();

            InterceptedParameter valueParameter = (InterceptedParameter)call.Parameters[0];

            string property = call.Method.Name.Substring(4);
            IPersistentObject managed = (IPersistentObject)call.Target;
            object res = call.Proceed();

            managed.SetReference(property, valueParameter.Value);

            if (!managed.Mute && !managed.Initializing)
                managed.Context.RegisterDirty(managed);
            
            managed.SetUnloaded(property, true);

            return res;
        }
    }
}
