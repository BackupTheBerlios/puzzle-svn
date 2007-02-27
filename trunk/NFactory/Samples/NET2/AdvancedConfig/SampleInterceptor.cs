using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Interception;

namespace AdvancedConfig
{
    public class SampleInterceptor : IAroundInterceptor
    {
        #region IAroundInterceptor Members

        public object HandleCall(Puzzle.NAspect.Framework.MethodInvocation call)
        {
            Console.WriteLine("entering {0}", call.ValueSignature);            
            object res = call.Proceed();
            Console.WriteLine("exiting {0} and returning '{1}'", call.ValueSignature,res);
            return res;
        }

        #endregion
    }
}
