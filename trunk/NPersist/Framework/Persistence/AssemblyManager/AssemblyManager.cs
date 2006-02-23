// *
// * Copyright (C) 2005 Mats Helander : http://www.puzzleframework.com
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
using Puzzle.NPersist.Framework.BaseClasses;
using Puzzle.NPersist.Framework.Exceptions;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.NPersist.Framework.Proxy;

namespace Puzzle.NPersist.Framework.Persistence
{
	public class AssemblyManager : ContextChild, IAssemblyManager
	{
		private Hashtable m_LoadedAssemblies = new Hashtable();

		public void RegisterAssembly(Assembly asm)
		{
			if (!(m_LoadedAssemblies.ContainsKey(asm.FullName)))
			{
				try
				{
					m_LoadedAssemblies[asm.FullName] = asm;
				}
				catch (Exception ex)
				{
					throw new NPersistException("Assembly already registered: '" + asm.FullName + "'", ex); // do not localize
				}
			}			
		}


		public Assembly GetAssembly(string assemblyPath)
		{
			Assembly asm;
			if (!(m_LoadedAssemblies.ContainsKey(assemblyPath)))
			{
				try
				{
					asm = Assembly.LoadFrom(assemblyPath);
					m_LoadedAssemblies[assemblyPath] = asm;
				}
				catch (Exception ex)
				{
					throw new NPersistException("Could not load assembly from path: '" + assemblyPath + "'", ex); // do not localize
				}
			}
			return ((Assembly) (m_LoadedAssemblies[assemblyPath]));
		}

		public virtual Type MustGetTypeFromClassMap(IClassMap classMap)
		{
			Type type = GetTypeFromClassMap(classMap);

			if (type == null)
				throw new MappingException("Could not find the type for the class " + classMap.Name + " (found in the map file) in any loaded Assembly!");
 
			return type;
		}

		public virtual Type GetTypeFromClassMap(IClassMap classMap)
		{
			Type type;
			type = Type.GetType(classMap.GetFullName() + ", " + classMap.GetAssemblyName());
			if (type == null)
			{
				foreach (Assembly asm in m_LoadedAssemblies.Values)
				{
					type = asm.GetType(classMap.GetFullName());
					if (type != null)
					{
						break;
					}
				}
			}
			return type;
		}


		public virtual Type MustGetTypeFromPropertyMap(IPropertyMap propertyMap)
		{
			Type type = GetTypeFromPropertyMap(propertyMap);

			if (type == null)
				throw new MappingException("Could not find the type " + propertyMap.GetDataOrItemType() + " for the property " + propertyMap.ClassMap.Name + "." + propertyMap.Name + " (found in the map file) in any loaded Assembly!");
 
			return type;
		}

		public virtual Type GetTypeFromPropertyMap(IPropertyMap propertyMap)
		{
			Type type = null;
			string dataType = propertyMap.GetDataOrItemType() ;
			IClassMap classMap = propertyMap.ClassMap.DomainMap.GetClassMap(dataType);
			if (classMap != null)
			{
				type = Type.GetType(classMap.GetFullName() + ", " + classMap.GetAssemblyName());
				if (type == null)
				{
					foreach (Assembly asm in m_LoadedAssemblies.Values)
					{
						type = asm.GetType(classMap.GetFullName());
						if (type != null)
						{
							break;
						}
					}
				}				
			}
			else
			{
				type = Type.GetType(dataType);
				if (type == null)
				{
					IDomainMap domainMap = propertyMap.ClassMap.DomainMap;
					string fullName = dataType;
					if (domainMap.RootNamespace.Length > 0)
						fullName = domainMap.RootNamespace + "." + fullName;

					type = Type.GetType(fullName + ", " + domainMap.GetAssemblyName());
				}
			}
			return type;
		}


		public virtual object CreateInstance(Type type, params object[] ctorParams)
		{
			object obj;

			while(typeof(IInterceptable).IsAssignableFrom(type))
				type = type.BaseType;

//			if (typeof(IInterceptable).IsAssignableFrom(type))
//			{
//				obj = this.Context.ObjectFactory.CreateInstance(type, ctorParams);				
//			}
//			else
//			{
				IClassMap classMap = this.Context.DomainMap.MustGetClassMap(type);
				obj = this.Context.ProxyFactory.CreateEntityProxy(type, this.Context.ObjectFactory, classMap, ctorParams);
//			}
			return obj;
		}

		public virtual Type GetType(Type type)
		{
			Type returnType;
			if (!(typeof(IInterceptable).IsAssignableFrom(type)))
			{
				IClassMap classMap = this.Context.DomainMap.MustGetClassMap(type);
				returnType = Context.ProxyFactory.GetEntityProxyType(type, classMap);
			}
			else
			{
				returnType = type;
			}
			return returnType;
		}

		
		public static Type GetBaseType(object obj)
		{
			return GetBaseType(obj.GetType() );
		}

		public static Type GetBaseType(Type type)
		{
			while (typeof(IInterceptable).IsAssignableFrom(type))
				type = type.BaseType ;
			return type;
		}
	}
}