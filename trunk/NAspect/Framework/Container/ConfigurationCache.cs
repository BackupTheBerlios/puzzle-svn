using System;
using System.Collections;

namespace Puzzle.NAspect.Framework
{
	public class ConfigurationCache
	{
		private static volatile Hashtable proxyCache = new Hashtable();
		private static volatile Hashtable wrapperCache = new Hashtable();
		private static volatile object syncRoot = new object();

		public static IDictionary GetProxyLookup(string configurationName)
		{
			lock (syncRoot)
			{
				if (proxyCache[configurationName] == null)
					proxyCache[configurationName] = new Hashtable();

				return proxyCache[configurationName] as IDictionary;
			}
		}

		public static IDictionary GetWrapperLookup(string configurationName)
		{
			lock (syncRoot)
			{
				if (wrapperCache[configurationName] == null)
					wrapperCache[configurationName] = new Hashtable();

				return wrapperCache[configurationName] as IDictionary;
			}
		}
	}
}