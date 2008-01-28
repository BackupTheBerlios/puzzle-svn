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
using Puzzle.NPersist.Framework.BaseClasses;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.EventArguments;
using Puzzle.NPersist.Framework.Exceptions;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.NPersist.Framework.Proxy;
using Puzzle.NPersist.Framework.Querying;
using Puzzle.NCore.Framework.Logging;
using Puzzle.NCore.Framework.Collections;
using System.Reflection;
using System.Globalization;

namespace Puzzle.NPersist.Framework.Persistence
{
	public class IdentityMap : ContextChild, IIdentityMap
	{

		public virtual IObjectCache GetObjectCache()
		{
			return this.Context.GetObjectCache(); 
		}

		public virtual void UnRegisterCreatedObject(object obj)
		{
			if (obj == null)
				throw new NullReferenceException("Can't unregister null object!"); // do not localize

			IContext ctx = this.Context;
			
			IObjectCache cache = GetObjectCache();

			cache.AllObjects.Remove(obj);

			KeyStruct key = GetKey(obj);
			cache.LoadedObjects.Remove(key);
			//m_objectStatusLookup.Remove(key);
			ctx.ObjectManager.SetObjectStatus(obj, ObjectStatus.NotRegistered);

            LogMessage message = new LogMessage("Unregistered created object");
            LogMessage verbose = new LogMessage("Type: {0}, Key: {1}" , obj.GetType(), key );

			ctx.LogManager.Info(this,message ,verbose ); // do not localize
		}

		public virtual void RegisterCreatedObject(object obj)
		{
			if (obj == null)
				throw new NullReferenceException("Can't register null object as created!"); // do not localize

			IContext ctx = this.Context;

            KeyStruct key = GetKey(obj);

			IObjectCache cache = GetObjectCache();

			object result = cache.LoadedObjects[key];
			if (result != null)
				throw new IdentityMapException("An object with the key " + key + " is already registered in the identity map!");

			result = cache.UnloadedObjects[key];
			if (result != null)
				throw new IdentityMapException("An object with the key " + key + " is already registered in the identity map!");

			//ctx.PersistenceManager.InitializeObject(obj);				
			
			if (cache.AllObjects != null)
				cache.AllObjects.Add(obj);

			cache.LoadedObjects[key] = obj;
			//m_objectStatusLookup[obj] = ObjectStatus.Clean;
			//ctx.ObjectManager.SetObjectStatus(obj, ObjectStatus.Clean);

            LogMessage message = new LogMessage("Registered created object");
            LogMessage verbose = new LogMessage( "Type: {0}, Key: {1}" , obj.GetType(), key );
			ctx.LogManager.Info(this, message,verbose); // do not localize
		}

		public virtual void RegisterLoadedObject(object obj)
		{
			if (obj == null)
				throw new NullReferenceException("Can't register null object as loaded!"); // do not localize

			IContext ctx = this.Context;

            KeyStruct key = GetKey(obj);

			IObjectCache cache = GetObjectCache();

			object result = cache.UnloadedObjects[key];
			if (result != null)
				cache.UnloadedObjects[key] = null;
			else
			{
				result = cache.LoadedObjects[key];
				if (result == null)
				{
					//ctx.PersistenceManager.InitializeObject(obj);				

					if (cache.AllObjects != null)
						cache.AllObjects.Add(obj);
				}
			}
			cache.LoadedObjects[key] = obj;
			this.Context.ReadOnlyObjectCacheManager.SaveObject(obj);
			
			if (this.Context.GetObjectStatus(obj) != ObjectStatus.Dirty)
			{
				//m_objectStatusLookup[obj] = ObjectStatus.Clean;
				ctx.ObjectManager.SetObjectStatus(obj, ObjectStatus.Clean);						
			}
            LogMessage message = new LogMessage("Registered loaded object");
            LogMessage verbose = new LogMessage("Type: {0}, Key: {1}" , obj.GetType(), key );

            ctx.LogManager.Info(this, message, verbose); // do not localize
		}

		public virtual void RegisterLazyLoadedObject(object obj)
		{
			if (obj == null)
				throw new NullReferenceException("Can't register null object as lazy loaded!"); // do not localize

			IContext ctx = this.Context;

			//ctx.PersistenceManager.InitializeObject(obj);

			IObjectCache cache = GetObjectCache();

			if (cache.AllObjects != null)
				cache.AllObjects.Add(obj);
            KeyStruct key = GetKey(obj);
			
			cache.UnloadedObjects[key] = obj;
			//m_objectStatusLookup[obj] = ObjectStatus.NotLoaded;
			ctx.ObjectManager.SetObjectStatus(obj, ObjectStatus.NotLoaded);

            LogMessage message = new LogMessage("Registered lazy loaded object");
            LogMessage verbose = new LogMessage("Type: {0}, Key: {1}" , obj.GetType(),key);

            ctx.LogManager.Info(this, message, verbose); // do not localize
		}

		/// <summary>
		/// This method assumes that the previous identity was a temporary, 
		/// GUID id assigned before the id was set up
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="previousIdentity"></param>
		public virtual void UpdateIdentity(object obj, string previousIdentity)
		{
			if (obj == null)
			{
				throw new NullReferenceException("Can't update temporary identity for null object!"); // do not localize
			}
            KeyStruct key = GetKey(obj);
            KeyStruct prevKey = GetOldKey(obj, previousIdentity);

			IObjectCache cache = GetObjectCache();

			object result = cache.LoadedObjects[prevKey];
			if (result != null)
			{
				cache.LoadedObjects[key] = obj;
				cache.LoadedObjects[prevKey] = null;
			}
			else
			{
				result = cache.UnloadedObjects[prevKey];
				if (result != null)
				{
					cache.UnloadedObjects[key] = obj;
					cache.UnloadedObjects[prevKey] = null;
				}				
			}
            LogMessage message = new LogMessage("Updated identity");
            LogMessage verbose = new LogMessage("Type: {0}, New Key: {1}, Previous Key: {2}" , obj.GetType(), key , prevKey);

			this.Context.LogManager.Debug(this, message, verbose); // do not localize
		}

		public virtual void UpdateIdentity(object obj, string previousIdentity, string newIdentity)
		{
			if (obj == null)
			{
				throw new NullReferenceException("Can't update temporary identity for null object!"); // do not localize
			}
            KeyStruct key = GetKey(obj, newIdentity);
            KeyStruct prevKey = GetOldKey(obj, previousIdentity);

			IObjectCache cache = GetObjectCache( );

			object result = cache.LoadedObjects[prevKey];
			if (result != null)
			{
				cache.LoadedObjects[key] = obj;
				cache.LoadedObjects[prevKey] = null;
			}
			else
			{
				result = cache.UnloadedObjects[prevKey];
				if (result != null)
				{
					cache.UnloadedObjects[key] = obj;
					cache.UnloadedObjects[prevKey] = null;
				}				
			}

            LogMessage message = new LogMessage("Updated identity");
            LogMessage verbose = new LogMessage("Type: {0}, New Key: {1}, Previous Key: {2}", obj.GetType(), key , prevKey);
			this.Context.LogManager.Debug(this,message , verbose); // do not localize
		}

        public virtual object GetObject(object identity, Type type, bool lazy)
		{
			return GetObject(identity, type, lazy, false);			
		}

        public virtual object GetObject(object identity, Type type, bool lazy, bool ignoreObjectNotFound)
		{
			//type = this.Context.AssemblyManager.GetType(type);
            type = AssemblyManager.GetBaseType(type);
            //string key = type.ToString() + "." + identity;
			KeyStruct key = GetKey(type, identity);
			object obj;
			ObjectCancelEventArgs e;

			IObjectCache cache = GetObjectCache();

			obj = cache.LoadedObjects[key];
			if (obj != null)
			{
				e = new ObjectCancelEventArgs(obj);
				this.Context.EventManager.OnGettingObject(this, e);
				if (e.Cancel)
				{
					return null;
				}
			}
			else
			{
				obj = cache.UnloadedObjects[key];
				if (obj != null)
				{
					e = new ObjectCancelEventArgs(obj);
					this.Context.EventManager.OnGettingObject(this, e);
					if (e.Cancel)
					{
						return null;
					}
				}
				else
				{
					obj = this.Context.AssemblyManager.CreateInstance(type);
					this.Context.ObjectManager.SetObjectIdentity(obj, key);
					e = new ObjectCancelEventArgs(obj);
					this.Context.EventManager.OnGettingObject(this, e);
					if (e.Cancel)
					{
						return null;
					}
					if (lazy)
					{
						RegisterLazyLoadedObject(obj);
						cache.UnloadedObjects[key] = obj;
					}
					else
					{
						string strIdentity = ToStringIdentity(identity);
						LoadObject(strIdentity, ref obj, ignoreObjectNotFound, type, key);
					}
				}				
			}
			ObjectEventArgs e2 = new ObjectEventArgs(obj);
			this.Context.EventManager.OnGotObject(this, e2);
			return obj;
		}

		public void LoadObject(ref object obj, bool ignoreObjectNotFound)
		{
			//Type type = this.Context.AssemblyManager.GetType(obj.GetType());
            Type type = AssemblyManager.GetBaseType(obj.GetType());
            string identity = this.Context.ObjectManager.GetObjectIdentity(obj);
            KeyStruct key = GetKey(type, identity);
			LoadObject(identity, ref obj, ignoreObjectNotFound, type, key);			
		}

        private void LoadObject(string identity, ref object obj, bool ignoreObjectNotFound, Type type, KeyStruct key)
		{
			IClassMap classMap = this.Context.DomainMap.MustGetClassMap(obj.GetType() );
			IObjectCache cache = GetObjectCache();

			if (classMap.IsReadOnly)
			{
				if (this.Context.ReadOnlyObjectCacheManager.LoadObject(obj))
				{
					cache.LoadedObjects[key] = obj;
					return;					
				}
			}

			if (classMap.LoadSpan.Length > 0)
			{
				IList listToFill = new ArrayList();
				NPathQuery query = this.Context.GetLoadObjectNPathQuery(obj, classMap.LoadSpan);
				this.Context.PersistenceEngine.LoadObjects( query, listToFill );
				if (listToFill.Count < 1)
				{
					if (ignoreObjectNotFound == false)
					{
						throw new ObjectNotFoundException("Object of type " + type.ToString() + " and with identity '" + identity + "' not found!"); // do not localize							
					}
					obj = null;
				}
				else if (listToFill.Count > 1)
				{
					//throw new WtfException("I thought you said your primary keys were unique, no??")
					obj = null;
				}
				else
				{
					obj = listToFill[0];
					cache.LoadedObjects[key] = obj;						
				}							
			}
			else
			{
				this.Context.PersistenceEngine.LoadObject(ref obj);
				if (obj == null)
				{
					if (ignoreObjectNotFound == false)
					{
						throw new ObjectNotFoundException("Object of type " + type.ToString() + " and with identity '" + identity + "' not found!"); // do not localize							
					}
				}
				else
				{
					cache.LoadedObjects[key] = obj;						
				}							
			}
		}

		public virtual bool HasObject(object obj)
		{
			string identity = this.Context.ObjectManager.GetObjectIdentity(obj);
			if (this.Context.ObjectManager.HasIdentity(obj))
				return HasObject(identity, obj.GetType() );

			Type type = AssemblyManager.GetBaseType(obj.GetType());
			KeyStruct key = GetOldKey(obj, identity);

			return HasObject(identity, type, key);				
		}

		public virtual bool HasObject(string identity, Type type)
		{
            type = AssemblyManager.GetBaseType(type);
            //type = this.Context.AssemblyManager.GetType(type);
            KeyStruct key = GetKey(type, identity);

			return HasObject(identity, type, key);
		}

		protected virtual bool HasObject(string identity, Type type, KeyStruct key)
		{
			object obj;

			IObjectCache cache = GetObjectCache();

			obj = cache.LoadedObjects[key];
			if (obj != null)
			{
				return true;
			}
			else
			{
				obj = cache.UnloadedObjects[key];
				if (obj != null)
				{
					return true;
				}				
			}
			return false;
		}



		public virtual object GetObjectByKey(string keyPropertyName, object keyValue, Type type)
		{
			return GetObjectByKey(keyPropertyName, keyValue, type, false);			
		}

		public virtual object GetObjectByKey(string keyPropertyName, object keyValue, Type type, bool ignoreObjectNotFound)
		{
            type = AssemblyManager.GetBaseType(type);
            //type = this.Context.AssemblyManager.GetType(type);
			object obj;
			obj = this.Context.AssemblyManager.CreateInstance(type);
			ObjectCancelEventArgs e = new ObjectCancelEventArgs(obj);
			this.Context.EventManager.OnGettingObject(this, e);
			if (e.Cancel)
			{
				return null;
			}
			this.Context.ObjectManager.SetPropertyValue(obj, keyPropertyName, keyValue);
			//this.Context.SqlEngineManager.LoadObjectByKey(obj, keyPropertyName, keyValue);
			this.Context.PersistenceEngine.LoadObjectByKey(ref obj, keyPropertyName, keyValue);
			if (obj == null)
			{
				if (ignoreObjectNotFound == false)
				{
					throw new ObjectNotFoundException("Object not found!"); // do not localize					
				}
			}
			else
			{
//				identity = this.Context.ObjectManager.GetObjectIdentity(obj);
//				key = type.ToString() + "." + identity;
                IList idKeyParts = this.Context.ObjectManager.GetObjectIdentityKeyParts(obj);
                KeyStruct key = GetKey(type, idKeyParts);
				IObjectCache cache = GetObjectCache();
				obj = cache.LoadedObjects[key];
//				if (m_hashLoadedObjects.ContainsKey(key))
//				{
//					obj = m_hashLoadedObjects[key];
//				}
				ObjectEventArgs e2 = new ObjectEventArgs(obj);
				this.Context.EventManager.OnGotObject(this, e2);				
			}
			return obj;
		}

		public virtual void RemoveObject(object obj)
		{
			if (obj == null)
			{
				throw new NullReferenceException("Can't remove null object!"); // do not localize
			}
            KeyStruct key = GetKey(obj);

			IObjectCache cache = GetObjectCache();

			cache.LoadedObjects[key] = null;
			cache.UnloadedObjects[key] = null;
			if (cache.AllObjects != null)
			{
				cache.AllObjects.Remove(obj);
			}
			//m_objectStatusLookup[obj] = null;
			RemoveAllReferencesToObject(obj);
		}

		//This might work better if we skip CascadeDelete props :-)

		protected virtual void RemoveAllReferencesToObject(object obj)
		{
//			IClassMap classMap = this.Context.DomainMap.GetClassMap(obj.GetType() );
//			object refObj;
//			IObjectManager om = this.Context.ObjectManager ;
//			foreach (object checkObject in GetObjects() )
//			{
//				IClassMap checkClassMap = this.Context.DomainMap.GetClassMap(checkObject.GetType() );
//				foreach (IPropertyMap propertyMap in checkClassMap.GetAllPropertyMaps() )
//				{
//					if (propertyMap.ReferenceType != ReferenceType.None)
//					{
//						if (propertyMap.GetReferencedClassMap() == classMap)
//						{
//							if (propertyMap.GetInversePropertyMap() == null)
//							{
//								if (checkObject != obj)
//								{
//									if (propertyMap.IsCollection)
//									{
//								
//									}
//									else
//									{
//										refObj = om.GetPropertyValue( checkObject, propertyMap.Name );
//										if (refObj != null)
//										{
//											if (refObj == obj)
//											{
//												om.SetOriginalPropertyValue( checkObject, propertyMap.Name, null );
//												om.SetPropertyValue( checkObject, propertyMap.Name, null );
//											}									
//										}
//									}																	
//								}
//							}
//						}
//					}
//				}
//
//			}
		}

        protected virtual KeyStruct GetKey(object obj)
		{
			if (obj == null)
			{
				throw new NullReferenceException("Can't create key for null object!"); // do not localize
			}

			IIdentityHelper identityHelper = obj as IIdentityHelper;
			if (identityHelper != null)
			{
                if (identityHelper.HasKeyStruct())
					return identityHelper.GetKeyStruct();
			}

			Type type = AssemblyManager.GetBaseType(obj);
            //KeyStruct key = new KeyStruct(GetKeyParts(type, this.Context.ObjectManager.GetObjectIdentity(obj)));
			KeyStruct key = new KeyStruct(GetKeyParts(type, this.Context.ObjectManager.GetObjectIdentityKeyParts(obj)));
			//string key = type.ToString() + "." + this.Context.ObjectManager.GetObjectIdentity(obj);

			if (identityHelper != null)
			{
                //Only cache the keyStruct if the identity 
                //has been stored (which means it is no longer
                //a temporary identity)
				if (identityHelper.HasIdentityKeyParts())
					identityHelper.SetKeyStruct(key);
			}
            return key;
		}

        protected virtual KeyStruct GetKey(object obj, object identity)
		{
			if (obj == null)
			{
				throw new NullReferenceException("Can't create key for null object!"); // do not localize
			}
			Type type = AssemblyManager.GetBaseType(obj);
            return new KeyStruct(GetKeyParts(type, identity));
			//return type.ToString() + "." + identity;
//			return obj.GetType().ToString() + "." + identity;
		}

        protected virtual KeyStruct GetKey(Type type, object identity)
		{
			Type fixedType = AssemblyManager.GetBaseType(type);
            return new KeyStruct(GetKeyParts(fixedType, identity));
            //return fixedType.ToString() + "." + identity;
			//			return obj.GetType().ToString() + "." + identity;
		}

		protected virtual KeyStruct GetOldKey(object obj, string previousIdentity)
		{
			if (obj == null)
			{
				throw new NullReferenceException("Can't create key for null object!"); // do not localize
			}
			Type type = AssemblyManager.GetBaseType(obj);
			return new KeyStruct(GetOldKeyParts(type, previousIdentity));
		}

        private object[] GetKeyParts(Type type, object identity)
        {
			if (typeof(string).IsAssignableFrom(identity.GetType()))
				return GetKeyParts(type, (string)identity);

            if (typeof(IList).IsAssignableFrom(identity.GetType()))
                return GetKeyParts(type, (IList) identity);

            object[] keyParts = new object[2];
            keyParts[0] = type;
            keyParts[1] = identity;
            return keyParts;
        }

        private object[] GetKeyParts(Type type, IList identity)
        {
            object[] keyParts = new object[identity.Count + 1];
            keyParts[0] = type;
            identity.CopyTo(keyParts, 1);
            return keyParts;
        }

        private object[] GetKeyParts(Type type, string identity)
        {
            IClassMap classMap = this.Context.DomainMap.MustGetClassMap(type);
            IList idProperties = classMap.GetIdentityPropertyMaps();
            if (idProperties.Count < 1)
                throw new NPersistException(string.Format("Type {0} has no known identity properties!",type.ToString()));
            if (idProperties.Count > 1)
                return GetKeyParts(type, identity, classMap, idProperties);

            object[] keyParts = new object[2];
            keyParts[0] = type;
            keyParts[1] = this.Context.ObjectManager.ConvertValueToType(type, ((IPropertyMap) idProperties[0]), identity);
            return keyParts;
        }

        private object[] GetKeyParts(Type type, string identity, IClassMap classMap, IList idProperties)
        {
            IList idKeyParts = this.Context.ObjectManager.ParseObjectIdentityKeyParts(classMap, idProperties, type, identity);
            object[] keyParts = new object[idKeyParts.Count + 1];
            keyParts[0] = type;
            idKeyParts.CopyTo(keyParts, 1);
            return keyParts;
        }

		private object[] GetOldKeyParts(Type type, string identity)
		{
			object[] keyParts = new object[2];
			keyParts[0] = type;
			keyParts[1] = identity;
			return keyParts;
		}

        private string ToStringIdentity(object identity)
        {
			if (typeof(string).IsAssignableFrom(identity.GetType()))
                return (string) identity;

			if (typeof(IList).IsAssignableFrom(identity.GetType()))
            {
                string strIdentity = "";
                foreach (object value in (IList) identity)
                    strIdentity += Convert.ToString(value, CultureInfo.InvariantCulture);
                return strIdentity;
            }

            return Convert.ToString(identity, CultureInfo.InvariantCulture);
        }

		public virtual IList GetObjects()
		{
			IObjectCache cache = GetObjectCache() ;
			IList cachedObjects = GetCacheObjects(cache);
			return cachedObjects;
		}

		private static IList GetCacheObjects(IObjectCache cache)
		{
			if (cache.AllObjects != null)
			{
				return cache.AllObjects;
			}
			else
			{
				IList objects = new ArrayList() ;
				foreach (object obj in cache.LoadedObjects.Values)
				{
					objects.Add(obj);
				}
				foreach (object obj in cache.UnloadedObjects.Values)
				{
					objects.Add(obj);
				}
				return objects;
			}
		}

        public virtual void Clear()
        {
			IObjectCache cache = GetObjectCache() ;
            cache.Clear();
        }

		public virtual object TryGetObject(string identity, Type type)
		{
			type = this.Context.AssemblyManager.GetType(type);
			string key = type.ToString() + "." + identity;
			IObjectCache cache = GetObjectCache();
			return cache.LoadedObjects[key];
		}

	}
}