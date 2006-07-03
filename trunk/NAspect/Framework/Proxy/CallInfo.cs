using System.Collections;
using System.Reflection;

namespace Puzzle.NAspect.Framework
{
	public class CallInfo
	{
		public MethodBase Method;
		public IList Interceptors;
        public IList InvocationParameterInfos;
		public string MethodId;
#if NET2
        public FastInvokeHandler Handler;


        public CallInfo(string methodId,MethodBase method, IList interceptors,IList invocationParameterInfos, FastInvokeHandler handler)
        {
			MethodId = methodId;
            Method = method;
            Interceptors = interceptors;
            InvocationParameterInfos = invocationParameterInfos; 
            Handler = handler;
        }
    
#else
		public CallInfo(string methodId,MethodBase method, IList interceptors,IList invocationParameterInfos)
		{
			MethodId = methodId;
			Method = method;
			Interceptors = interceptors;
            InvocationParameterInfos = invocationParameterInfos; 
		}
#endif
    }
}