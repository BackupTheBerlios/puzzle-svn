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
using Puzzle.NAspect.Framework;
using Puzzle.NPersist.Framework.BaseClasses;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.NPersist.Framework.Proxy;

namespace Puzzle.NPersist.Framework.Persistence
{
	/// <summary>
	/// Summary description for ListManager.
	/// </summary>
	public class ListManager : ContextChild, IListManager
	{
		public ListManager()
		{
		
		}

		public virtual IList CreateList(object obj, IPropertyMap propertyMap)
		{
			return CreateList(obj, propertyMap.Name);
		}

		public virtual IList CreateList(object obj, string propertyName)
		{
			Type listType = obj.GetType().GetProperty(propertyName).PropertyType;
			return CreateList(listType, obj, propertyName);
		}

		public virtual IList CreateList(Type listType, object obj, IPropertyMap propertyMap)
		{
			return CreateList(listType, obj, propertyMap.Name);
		}

		public virtual IList CreateList(Type listType, object obj, string propertyName)
		{
			IList newList = null;
			IInterceptableList mList;
#if NET2
            if (listType == typeof(System.Collections.Generic.IList<>))
            {
                Type subType = listType.GetGenericArguments ()[0];
                

                Type genericType = typeof(InterceptableGenericsList<>).MakeGenericType(subType);

                mList = (IInterceptableList) Activator.CreateInstance(genericType);
				mList.Interceptable = (IInterceptable) obj;
				mList.PropertyName = propertyName;
				newList = mList;				
            }
            else if (listType == typeof(IList))
#else
            if (listType == typeof(IList))
#endif			
			{
				mList = (IInterceptableList) Activator.CreateInstance(typeof(InterceptableList));
				mList.Interceptable = (IInterceptable) obj;
				mList.PropertyName = propertyName;
				newList = mList;
			}
			else if (typeof(IList).IsAssignableFrom(listType))
			{
				if (listType.IsInterface)
				{
					throw new Exception("List property type error! Can't specyify list property type specify as interface other than IList. Please specify property type as IList or a concrete class."); // do not localize
				}
				if (listType.IsAbstract)
				{
					throw new Exception("List property type error! Can't specyify list property type as abstract class. Please specify property type as IList or a concrete class."); // do not localize
				}
				if (typeof(IInterceptableList).IsAssignableFrom(listType))
				{
					mList = (IInterceptableList) Activator.CreateInstance(listType);
					mList.Interceptable = (IInterceptable) obj;
					mList.PropertyName = propertyName;
					newList = mList;
				}
				else
				{
					mList = Context.ProxyFactory.CreateListProxy(listType, this.Context.ObjectFactory);
					mList.Interceptable = (IInterceptable) obj;
					mList.PropertyName = propertyName;
					newList = mList;					
				}
			}
			else
			{
				throw new Exception("List property type error! List property type must implement IList interface."); // do not localize
			}
			return newList;
		}

//		public virtual IList CreateList(object obj, IPropertyMap propertyMap)
//		{
//			IInterceptableList list;
//			list = (IInterceptableList) Activator.CreateInstance(typeof(InterceptableList));
//			list.Interceptable = (IInterceptable) obj;
//			list.PropertyName = propertyMap.Name;
//			return (IList) list;
//		}

		public virtual IList CloneList(object obj, IPropertyMap propertyMap, IList orgList)
		{
			//IList newList = CreateList(orgList.GetType(), obj, propertyMap);
			Type t = obj.GetType() ;
			Type listType = t.GetProperty(propertyMap.Name).PropertyType;

			if (listType == typeof(IList))
				listType = typeof(ArrayList);


			IList newList = Context.ProxyFactory.CreateListProxy(listType,Context.ObjectFactory,new object[0] ) ;

			
			//ORGINALRADEN SOM IAF GÅR ATT SÄTTA BP PÅ!!!
			//IList newList = (IList) Activator.CreateInstance(orgList.GetType());	
			IInterceptableList mList;
			bool stackMute = false;

			mList = newList as IInterceptableList;
			if (mList != null)
			{
				stackMute = mList.MuteNotify;
				mList.MuteNotify = true;
				mList.Interceptable = (IInterceptable) obj;
				mList.PropertyName = propertyMap.Name;
			}
			foreach (object item in orgList)
			{
				newList.Add(item);
			}
			if (mList != null)
			{
				mList.MuteNotify = stackMute;
			}
			return newList;
		}

		public virtual void SetupListProperties(object obj)
		{
			IClassMap classMap = this.Context.DomainMap.MustGetClassMap(obj.GetType());
			IList value;
			IObjectManager om = this.Context.ObjectManager;
			foreach (IPropertyMap propertyMap in classMap.GetAllPropertyMaps())
			{
				if (propertyMap.IsCollection)
				{
					value = (IList) om.GetPropertyValue(obj, propertyMap.Name);
					if (value == null)
					{
						om.SetPropertyValue(obj, propertyMap.Name, CreateList(obj, propertyMap));
						//list should /not/ have original value or it will not be considered NotLoaded
						//om.SetOriginalPropertyValue(obj, propertyMap.Name, CreateList(obj, propertyMap));
					}
				}				
				
			}

		}

		public bool CompareLists(IList newList, IList oldList)
		{
			if (newList == null || oldList == null)
			{
				if (!((newList == null && oldList == null)))
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			if (!(oldList.Count == newList.Count))
			{
				return false;
			}
			foreach (object value in oldList)
			{
				if (!(newList.Contains(value)))
				{
					return false;
				}
			}
			foreach (object value in newList)
			{
				if (!(oldList.Contains(value)))
				{
					return false;
				}
			}
			return true;
		}

	}
}
