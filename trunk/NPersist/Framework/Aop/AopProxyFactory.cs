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
using Puzzle.NAspect.Framework;
using Puzzle.NPersist.Framework.Interfaces;

namespace Puzzle.NPersist.Framework.Aop
{
	/// <summary>
	/// Summary description for AopProxyFactory.
	/// </summary>
	public class AopProxyFactory : IProxyFactory , IContextChild
	{
		private Engine aopEngine;
		private IContext context;

		public AopProxyFactory()
		{
			aopEngine = new Engine("Puzzle.NPersist.Framework");			
		}

		public Puzzle.NPersist.Framework.Interfaces.IInterceptableList CreateListProxy(Type baseType, Puzzle.NPersist.Framework.Persistence.IObjectFactory objectFactory, params object[] ctorArgs)
		{
		//	return Puzzle.NPersist.Framework.Proxy.ListProxyFactory.CreateProxy(baseType,objectFactory,ctorArgs) ;

			if (baseType == typeof(IInterceptableList))
			{
				return (Puzzle.NPersist.Framework.Interfaces.IInterceptableList) context.ObjectFactory.CreateInstance(baseType,ctorArgs);
			}
			else
			{
				Type proxyType = aopEngine.CreateProxyType(baseType) ;
				object[] proxyArgs = aopEngine.AddStateToCtorParams(context,ctorArgs);

				return (Puzzle.NPersist.Framework.Interfaces.IInterceptableList) context.ObjectFactory.CreateInstance(proxyType,proxyArgs);
				//return Puzzle.NPersist.Framework.Proxy.ListProxyFactory.CreateProxy(baseType,objectFactory,ctorArgs) ;
			}
		}

		public object CreateEntityProxy(Type baseType, Puzzle.NPersist.Framework.Persistence.IObjectFactory objectFactory, Puzzle.NPersist.Framework.Mapping.IClassMap classMap, object[] ctorArgs)
		{			

			Type proxyType = aopEngine.CreateProxyType(baseType) ;
			object[] proxyArgs = aopEngine.AddStateToCtorParams(context,ctorArgs);

			return context.ObjectFactory.CreateInstance(proxyType,proxyArgs);
		}

		public Type GetEntityProxyType(Type baseType, Puzzle.NPersist.Framework.Mapping.IClassMap classMap)
		{
			return aopEngine.CreateProxyType(baseType);
		}

		public IContext Context
		{
			get { return context; }
			set 
			{ 
				context = value;
				aopEngine.Configuration.Aspects.Clear() ;
				aopEngine.Configuration.Aspects.Add(new NPersistEntityAspect(Context));		
				aopEngine.Configuration.Aspects.Add(new NPersistListAspect(Context));		
				aopEngine.LogManager = context.LogManager;
			}
		}
	}
}
