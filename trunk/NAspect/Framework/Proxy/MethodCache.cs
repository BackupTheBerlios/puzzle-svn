using System;
using System.Collections;
using System.Reflection;

namespace Puzzle.NAspect.Framework
{
	public class MethodCache
	{

        //contains the base methodinfo for each methodid (wrappername)
		public static Hashtable methodLookup = new Hashtable();

        //contains the wrapper methodinfo for each methodid(wrappername)
		public static Hashtable wrapperMethodLookup = new Hashtable();

        //contains an arraylist of interceptors for each methodId (wrappername)
		public static Hashtable methodInterceptorsLookup = new Hashtable();

        //gets the interceptos for a methodid
		public static IList GetInterceptors(string methodId)
		{
			if (methodId == null)
				throw new NullReferenceException("Method may not be null");


			if (MethodCache.methodInterceptorsLookup.Contains(methodId))
			{
				IList methodinterceptors = MethodCache.methodInterceptorsLookup[methodId] as IList;
				return methodinterceptors;
			}

			throw new Exception("Unknown method");
		}

        //used by the explicit iface proyfier
		public static MethodInfo GetMethodMethodInfo = typeof(MethodCache).GetMethod("GetMethod");
		public static MethodBase GetMethod(string methodId)
		{
			return (MethodBase)methodLookup[methodId];
		}
	}
}