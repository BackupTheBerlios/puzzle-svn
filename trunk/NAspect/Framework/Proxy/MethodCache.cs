// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics;

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
        [DebuggerStepThrough()]
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