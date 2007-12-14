using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Interception;

namespace NObjectStore
{
    class EntityPropertyGetInterceptor : IAroundInterceptor
    {
        public object HandleCall(Puzzle.NAspect.Framework.MethodInvocation call)
        {
            if (call.Method.DeclaringType == typeof(IPersistentObject))
                return call.Proceed();

            string property = GetPropertyName(call);
            IPersistentObject managed = (IPersistentObject)call.Target;
            object res = managed.GetPropertyValue(property);
            call.Proceed ();
            return res;            
        }

        private static string GetPropertyName(Puzzle.NAspect.Framework.MethodInvocation call)
        {
            string property = call.Method.Name.Substring(4);
            return property;
        }
    }
}
