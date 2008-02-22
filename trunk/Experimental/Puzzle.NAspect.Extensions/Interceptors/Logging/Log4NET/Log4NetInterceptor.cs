using System;
using System.Collections;
using System.Reflection;
using System.Text;

using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Extensions.Attributes.Logging.Log4NET;

using log4net;

namespace Puzzle.NAspect.Extensions.Interceptors.Logging.Log4NET
{
	/// <summary>
	/// Summary description for Log4NetInterceptor.
	/// </summary>
	public class Log4NetInterceptor : IInterceptor
	{
		private static readonly ILog defaultLog = LogManager.GetLogger(typeof(Log4NetInterceptor));

		protected int call_it = 0;

		public object HandleCall(MethodInvokation call)
		{
			call_it ++;

			// Call intercepted function
			object result = call.Proceed();

			try 
			{
				MethodInfo mInfo = call.Method as MethodInfo;
				foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo)) 
				{
					if (attr.GetType() == typeof(Log4NetAttribute)) 
					{
						WriteLog(call, result, attr as Log4NetAttribute);
					}
				}
			} 
			catch(Exception ex) 
			{
				defaultLog.Error("Failure in Log4NetInterceptor", ex);
			}

			return result;
		}

		private void WriteLog(MethodInvokation call, object result, Log4NetAttribute attr) 
		{
			// Select logger
			ILog log = null;
			if(attr.LogPath != null) 
			{
				log = LogManager.GetLogger(attr.LogPath, typeof(Log4NetInterceptor));
			}
			else 
			{
				log = defaultLog;
			}
						
			// Write 
			if(attr == null || attr.Format == null) 
			{
				DefaultWriteLog(call, result, log, attr);
			}
			else 
			{
				object[] arg = new object[call.Parameters.Count + 1];
				int it;
				for(it = 0; it < call.Parameters.Count; it ++)
				{
					InterceptedParameter param = call.Parameters[it] as InterceptedParameter;
					arg[it] = param.Value;
				}

				arg[it] = result;

				Log(log, attr.Level, String.Format(attr.Format, arg));
			}
		}

		private void DefaultWriteLog(MethodInvokation call, object result, ILog log, Log4NetAttribute attr) 
		{
			StringBuilder buf = new StringBuilder();
			buf.Append(call.Method.Name + "(");

			bool fFirstParam = true;
			foreach (InterceptedParameter param in call.Parameters)
			{
				if(fFirstParam) 
					fFirstParam = false;
				else
					buf.Append(", ");

				buf.Append(param.Name + ":" + param.Value);
			}
			buf.Append(")");

			Log(log, attr.Level, buf.ToString());
		}

		private void Log(ILog log, Log4NetAttribute.Levels level, string strText) 
		{
			switch(level) 
			{
				case Log4NetAttribute.Levels.DEBUG:
					log.Debug(strText);
					break;
				case Log4NetAttribute.Levels.ERROR:
					log.Error(strText);
					break;
				case Log4NetAttribute.Levels.FATAL:
					log.Fatal(strText);
					break;
				case Log4NetAttribute.Levels.INFO:
					log.Info(strText);
					break;
				case Log4NetAttribute.Levels.WARN:
					log.Warn(strText);
					break;
			}
		}
	}
}