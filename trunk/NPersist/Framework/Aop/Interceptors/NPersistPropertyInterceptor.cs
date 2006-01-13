// *
// * Copyright (C) 2005 Roger Johansson
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *
using System;
using Puzzle.NAspect.Framework;
using Puzzle.NPersist.Framework.Interfaces;
using IInterceptor = Puzzle.NAspect.Framework.Aop.IInterceptor;

namespace Puzzle.NPersist.Framework.Aop

{
	/// <summary>
	/// Summary description for NPersistPropertyInterceptor.
	/// </summary>
	public class NPersistPropertyInterceptor : IInterceptor
	{
		public NPersistPropertyInterceptor()
		{
		}

		public object HandleCall(MethodInvocation call)
		{		
			try
			{
				if (call.Method.Name.StartsWith("get_") )
					return HandleGetProperty(call);

				if (call.Method.Name.StartsWith("set_") )
					return HandleSetProperty(call);
			}
			catch 
			{
				throw;
				//throw new Exception("Could not handle call "+call.Method.Name + " " + x.ToString()) ;
			}

			throw new Exception ("Blame Roger for this bug! "+call.Method.Name);
		}

		private object HandleSetProperty(MethodInvocation call)
		{
			IProxy proxy = (IProxy) call.Target;
			bool cancel = false;
			object value = ((InterceptedParameter)call.Parameters[0]).Value;
			string propertyName = call.Method.Name.Substring(4);
			object refValue = value;
			Puzzle.NPersist.Framework.Interfaces.IInterceptor interceptor = proxy.GetInterceptor();
			if (interceptor != null) { interceptor.NotifyPropertySet(call.Target, propertyName, ref refValue, ref cancel); }
			if (cancel) { return null; }
			((InterceptedParameter)call.Parameters[0]).Value = refValue;
			call.Proceed();		
			return null;
		}

		private object HandleGetProperty(MethodInvocation call)
		{
			IProxy proxy = (IProxy) call.Target;
			string propertyName = call.Method.Name.Substring(4);
			object value = call.Proceed();
			bool cancel = false;
			Puzzle.NPersist.Framework.Interfaces.IInterceptor interceptor = proxy.GetInterceptor() ;
			if (interceptor != null) {interceptor.NotifyPropertyGet(call.Target,propertyName,ref value,ref cancel) ;}
			if (cancel) {return GetDefaultValue(call.ReturnType);}
			return value;
		}

		private static object GetDefaultValue(Type dataType)
		{
			Array array = Array.CreateInstance(dataType, 1);
			return array.GetValue(0);
		}
	}
}
