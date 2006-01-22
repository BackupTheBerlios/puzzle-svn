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
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework.ConfigurationElements;
using Puzzle.NCore.Framework.Logging;

namespace Puzzle.NAspect.Framework
{
	public class Engine
	{
		private IDictionary proxyLookup;
		private IDictionary wrapperLookup;

		public readonly AspectMatcher AspectMatcher = new AspectMatcher();
		public readonly PointcutMatcher PointCutMatcher = new PointcutMatcher();

		#region Engine

        /// <summary>
        /// AOP Engine constructor
        /// </summary>
        /// <param name="configurationName">name of configuration/type cache to use</param>
		public Engine(string configurationName)
		{
			configuration = new EngineConfiguration();
			proxyLookup = ConfigurationCache.GetProxyLookup(configurationName);
			wrapperLookup = ConfigurationCache.GetWrapperLookup(configurationName);
			logManager = new LogManager();
		}

		#endregion

		#region Public Property Configuration

		private EngineConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
		public EngineConfiguration Configuration
		{
			get { return this.configuration; }
			set { this.configuration = value; }
		}

		#endregion

		#region Public Property LogManager

		private ILogManager logManager;

		public ILogManager LogManager
		{
			get { return this.logManager; }
			set { this.logManager = value; }
		}

		#endregion

		#region CreateProxy




		/// <summary>
		/// Creates a dynamic proxy type and returns an instance of the proxy
		/// </summary>
		/// <param name="type">type to be proxied</param>
		/// <param name="args">constructor args</param>
		/// <returns>instance of the proxy type</returns>
		public object CreateProxy(Type type, params object[] args)
		{
			string message = string.Format("Creating proxy for type {0}", type.FullName);
			LogManager.Info(this, message, "");

			return CreateProxyWithState(null, type, args);
		}

		public object CreateWrapper(object instance)
		{
			string message = string.Format("Creating wrapper for type {0}", instance.GetType().FullName);
			LogManager.Info(this, message, "");
			
			Type wrapperType = CreateWrapperType(instance.GetType());

			object wrapperObject = Activator.CreateInstance(wrapperType, new object[]{instance} );
			return wrapperObject;
		}

		public object[] AddStateToCtorParams(object state, object[] args)
		{
			if (args == null)
				args = new object[] {null};

			object[] proxyArgs = new object[args.Length + 1];
			Array.Copy(args, 0, proxyArgs, 1, args.Length);
			proxyArgs[0] = state;

			return proxyArgs;
		}

		public object CreateProxyWithState(object state, Type type, params object[] args)
		{
			string message = string.Format("Creating context bound wrapper for type {0}", type.FullName);
			LogManager.Info(this, message, "");
			Type proxyType = CreateProxyType(type);

			object[] proxyArgs;
			if (proxyType == type)
			{
				proxyArgs = args;
			}
			else
			{
				proxyArgs = AddStateToCtorParams(state, args);
			}

			object proxyObject = Activator.CreateInstance(proxyType, proxyArgs);
			return proxyObject;
		}

		public Type CreateProxyType(Type type)
		{
			lock (proxyLookup.SyncRoot)
			{
				Type proxyType = null;
				//incase a proxy for this type does not exist , generate it
				if (proxyLookup[type] == null)
				{
					IList typeAspects = AspectMatcher.MatchAspectsForType(type, Configuration.Aspects);

					IList typeMixins = GetMixinsForType(type);

					typeMixins.Add(typeof (AopProxyMixin));

					proxyType = SubclassProxyFactory.CreateProxyType(type, typeAspects, typeMixins, this);
					if (proxyType == null)
						throw new NullReferenceException(string.Format("Could not generate proxy for type '{0}'", type.FullName));

					proxyLookup[type] = proxyType;
					string message = string.Format("Emitting new proxy type for type {0}", type.FullName);
					LogManager.Info(this, message, "");
				}
				else
				{
					//fetch the proxy type from the lookup
					proxyType = proxyLookup[type] as Type;
					string message = string.Format("Fetching proxy type from cache for type {0}", type.FullName);
					LogManager.Info(this, message, "");
				}
				return proxyType;
			}
		}

		public Type CreateWrapperType(Type type)
		{
			lock (wrapperLookup.SyncRoot)
			{
				Type wrapperType = null;
				//incase a proxy for this type does not exist , generate it
				if (wrapperLookup[type] == null)
				{
					IList typeAspects = AspectMatcher.MatchAspectsForType(type, Configuration.Aspects);

					IList typeMixins = GetMixinsForType(type);

					typeMixins.Add(typeof (AopProxyMixin));

					wrapperType = InterfaceProxyFactory.CreateProxyType(type, typeAspects, typeMixins, this);
					if (wrapperType == null)
						throw new NullReferenceException(string.Format("Could not generate wrapper for type '{0}'", type.FullName));

					wrapperLookup[type] = wrapperType;
					string message = string.Format("Emitting new wrapper type for type {0}", type.FullName);
					LogManager.Info(this, message, "");
				}
				else
				{
					//fetch the proxy type from the lookup
					wrapperType = wrapperLookup[type] as Type;
					string message = string.Format("Fetching proxy wrapper from cache for type {0}", type.FullName);
					LogManager.Info(this, message, "");
				}
				return wrapperType;
			}
		}

		private IList GetMixinsForType(Type type)
		{
			IList typeAspects = AspectMatcher.MatchAspectsForType(type, Configuration.Aspects);
			Hashtable mixins = new Hashtable();
			foreach (IAspect aspect in typeAspects)
			{
				foreach (Type mixinType in aspect.Mixins)
				{
					//distinct add mixin..
					mixins[mixinType] = mixinType;
				}
			}
			IList distinctMixins = new ArrayList(mixins.Values);


			string message = string.Format("Getting mixins for type {0}", type.FullName);
			string verbose = "";
			foreach (Type mixinType in distinctMixins)
			{
				verbose += mixinType.Name;
				if (mixinType != distinctMixins[distinctMixins.Count - 1])
					verbose += ", ";
			}

			LogManager.Info(this, message, verbose);

			return distinctMixins;
		}

#if NET2
        public T CreateProxy<T>(params object[] args)
        {
            Type type = typeof(T);
            object o = CreateProxy(type, args);
            return (T)o;
        }

        public T CreateProxyWithState<T>(object state,params object[] args)
        {
            Type type = typeof(T);
            object o = CreateProxyWithState(state,type, args);
            return (T)o;
        }
#endif

		#endregion
	}
}