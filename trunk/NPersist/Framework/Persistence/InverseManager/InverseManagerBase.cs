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
using System.Diagnostics;
using Puzzle.NPersist.Framework.BaseClasses;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.NCore.Framework.Logging;

namespace Puzzle.NPersist.Framework.Persistence
{
	/// <summary>
	/// Summary description for InverseManagerBase.
	/// </summary>
	public abstract class InverseManagerBase : ContextChild, IInverseManager
	{
		protected InverseManagerBase()
		{
		}

        public abstract void NotifyPropertyGet(object obj, string propertyName);

		public abstract void NotifyPropertySet(object obj, string propertyName, object value);

		public abstract void NotifyPropertySet(object obj, string propertyName, object value, object oldValue);

		public abstract void NotifyCreate(object obj);

		public abstract void NotifyPersist(object obj);

		public abstract void NotifyDelete(object obj);

        public abstract void NotifyCommitted(object obj);

		public virtual void RemoveAllReferencesToObject(object obj)
		{
			RemoveInverseReferences(obj);
			RemoveNonInverseReferences(obj);
			RemoveInsertedReferences(obj);
		}


		public virtual void RemoveNonInverseReferences(object obj)
		{
			IDomainMap domainMap = this.Context.DomainMap; 
			IClassMap classMap = domainMap.MustGetClassMap(obj.GetType() );
			IList classMaps = this.Context.DomainMap.GetClassMapsWithUniDirectionalReferenceTo(classMap, true);
			NullifyUniReferences(obj, classMap, classMaps);
			//NullifyReferencesInCache(obj, classMap, classMaps);
		}


		protected virtual void RemoveInsertedReferences(object obj)
		{
			IDomainMap domainMap = this.Context.DomainMap; 
			IClassMap classMap = domainMap.MustGetClassMap(obj.GetType() );

			IUnitOfWork uow = this.Context.UnitOfWork;

			foreach(object test in uow.GetCreatedObjects())
			{
				NullifyReferencesInInsertedObject(obj, classMap, test);
			}
		}

		protected virtual void NullifyReferencesInInsertedObject(object obj, IClassMap classMap, object refering)
		{
			IDomainMap domainMap = this.Context.DomainMap; 
			IClassMap referingClassMap = domainMap.MustGetClassMap(refering.GetType() );

			IObjectManager om = this.Context.ObjectManager ;

			foreach (IPropertyMap refPropertyMap in referingClassMap.GetAllPropertyMaps())
			{
				if (!(refPropertyMap.ReferenceType == ReferenceType.None))
				{
					NullifyReferenceInInsertedObject(obj, refering, refPropertyMap, om);
				}
			}

		}

		private void NullifyReferenceInInsertedObject(object obj, object refering, IPropertyMap refPropMap, IObjectManager om)
		{
			bool stackMute = false;
			IInterceptableList mList;
			IList refList;
			object thisObj;

			if (refPropMap.IsCollection)
			{
				if (!(this.Context.GetPropertyStatus(refering, refPropMap.Name) == PropertyStatus.NotLoaded))
				{
					refList = ((IList) (om.GetPropertyValue(refering, refPropMap.Name)));
					if (refList.Contains(obj))
					{
						mList = refList as IInterceptableList;					
						if (mList != null)
						{
							stackMute = mList.MuteNotify;
							mList.MuteNotify = true;
						}
						refList.Remove(obj);
						if (mList != null) { mList.MuteNotify = stackMute; }

						om.SetUpdatedStatus(refering, refPropMap.Name, true);
					}

				}
			}
			else
			{
				if (!(this.Context.GetPropertyStatus(refering, refPropMap.Name) == PropertyStatus.NotLoaded))
				{
					thisObj = this.Context.ObjectManager.GetPropertyValue(refering, refPropMap.Name);
					if (thisObj != null)
					{
						if (thisObj == obj)
						{
							om.SetPropertyValue(refering, refPropMap.Name, null);
							om.SetUpdatedStatus(refering, refPropMap.Name, true);
						}
					}
				}
			}
		}

		protected virtual void NullifyUniReferences(object obj, IClassMap classMap, IList classMapsWithUniRefs)
		{
			if (classMapsWithUniRefs.Count < 1) 
				return;

			IDomainMap domainMap = this.Context.DomainMap;
			IPersistenceEngine pe = this.Context.PersistenceEngine;
			IAssemblyManager am = this.Context.AssemblyManager;
			foreach (IClassMap classMapWithUniRef in classMapsWithUniRefs)
			{
				Type classWithUniRef = am.GetTypeFromClassMap(classMapWithUniRef);
				IList objectsWithUniRefs = pe.GetObjectsOfClassWithUniReferencesToObject(classWithUniRef, obj);				
				IList uniRefPropertyMaps = classMapWithUniRef.GetUniDirectionalReferencesTo(classMap, true);
				foreach(object test in objectsWithUniRefs)
				{
					IClassMap testClassMap = domainMap.MustGetClassMap(test.GetType());
					NullifyUniReferencesInObject(obj, classMap, test, testClassMap, uniRefPropertyMaps);
				}
			}
		}

		protected virtual void NullifyUniReferencesInObject(object obj, IClassMap classMap, object refering, IClassMap referingClassMap, IList uniRefPropertyMaps)
		{
			IObjectManager om = this.Context.ObjectManager ;
			foreach (IPropertyMap uniRefPropertyMap in uniRefPropertyMaps)
			{
				NullifyUniReference(obj, refering, uniRefPropertyMap, om);
			}
		}

		private void NullifyUniReference(object obj, object refering, IPropertyMap uniRefPropertyMap, IObjectManager om)
		{
			bool stackMute = false;
			IInterceptableList mList;
			IList refList;
			object thisObj;

			if (uniRefPropertyMap.IsCollection)
			{
				if (!(this.Context.GetPropertyStatus(refering, uniRefPropertyMap.Name) == PropertyStatus.NotLoaded))
				{
					refList = ((IList) (om.GetPropertyValue(refering, uniRefPropertyMap.Name)));
					if (refList.Contains(obj))
					{
						mList = refList as IInterceptableList;					
						if (mList != null)
						{
							stackMute = mList.MuteNotify;
							mList.MuteNotify = true;
						}
						refList.Remove(obj);
						if (mList != null) { mList.MuteNotify = stackMute; }
						om.SetUpdatedStatus(refering, uniRefPropertyMap.Name, true);

					}
				}
			}
			else
			{
				if (!(this.Context.GetPropertyStatus(refering, uniRefPropertyMap.Name) == PropertyStatus.NotLoaded))
				{
					thisObj = this.Context.ObjectManager.GetPropertyValue(refering, uniRefPropertyMap.Name);
					if (thisObj != null)
					{
						if (thisObj == obj)
						{
							this.Context.ObjectManager.SetPropertyValue(refering, uniRefPropertyMap.Name, null);
							om.SetUpdatedStatus(refering, uniRefPropertyMap.Name, true);
						}
					}
				}
			}
		}

		//Follow all bi-directional references from the
		//object, setting the nullable, clean back references to null 
		public virtual void RemoveInverseReferences(object obj)
		{
            LogMessage message = new LogMessage("Removing inverse references");
            LogMessage verbose = new LogMessage(obj.GetType());
			this.Context.LogManager.Info(this, message, verbose); // do not localize
			IObjectManager om = this.Context.ObjectManager;
			IClassMap classMap = this.Context.DomainMap.MustGetClassMap(obj.GetType());
			IPropertyMap invPropertyMap;
			foreach (IPropertyMap propertyMap in classMap.GetAllPropertyMaps())
			{
				if (!(propertyMap.ReferenceType == ReferenceType.None))
				{
					if (propertyMap.Inverse.Length > 0)
					{
						invPropertyMap = propertyMap.GetInversePropertyMap();
						if (invPropertyMap != null)
						{
							if (invPropertyMap.ReferenceType == ReferenceType.ManyToOne || 
								invPropertyMap.ReferenceType == ReferenceType.ManyToMany  || 
								invPropertyMap.GetIsNullable() )
							{								
								NullifyInverseReference(propertyMap, obj, invPropertyMap, om);
							}
						}
					}
				}
			}
		}

		private void NullifyInverseReference(IPropertyMap propertyMap, object obj, IPropertyMap invPropertyMap, IObjectManager om)
		{
			bool stackMute = false;
			IInterceptableList mList;
			IList refList;
			IList list;
			object thisObj;
			object refObj;

			//Ensure that the property is loaded
			if (this.Context.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.NotLoaded)
			{
				this.Context.LoadProperty(obj, propertyMap.Name);						
			}

			if (propertyMap.IsCollection)
			{
				list = (IList) om.GetPropertyValue(obj, propertyMap.Name);
				if (list == null)
				{
					list = this.Context.ListManager.CreateList(obj, propertyMap);
				}
				if (list != null)
				{
					if (invPropertyMap.IsCollection)
					{
						foreach (object itemRefObj in list)
						{
							//Ensure inverse is loaded
							PropertyStatus invPropertyStatus = this.Context.GetPropertyStatus(itemRefObj, invPropertyMap.Name);
							if (invPropertyStatus == PropertyStatus.NotLoaded)
							{
								this.Context.LoadProperty(itemRefObj, invPropertyMap.Name);
							}

							refList = ((IList) (om.GetPropertyValue(itemRefObj, invPropertyMap.Name)));
							if (refList.Contains(obj))
							{
								mList = refList as IInterceptableList;					
								if (mList != null)
								{
									stackMute = mList.MuteNotify;
									mList.MuteNotify = true;
								}
								refList.Remove(obj);
								if (mList != null) { mList.MuteNotify = stackMute; }
								om.SetUpdatedStatus(itemRefObj, invPropertyMap.Name, true);
							}
						}
					}
					else
					{
						foreach (object itemRefObj in list)
						{
							//Ensure inverse is loaded
							PropertyStatus invPropertyStatus = this.Context.GetPropertyStatus(itemRefObj, invPropertyMap.Name);
							if (invPropertyStatus == PropertyStatus.NotLoaded)
							{
								this.Context.LoadProperty(itemRefObj, invPropertyMap.Name);
							}

							thisObj = om.GetPropertyValue(itemRefObj, invPropertyMap.Name);
							if (thisObj != null)
							{
								if (thisObj == obj)
								{
									om.SetPropertyValue(itemRefObj, invPropertyMap.Name, null);
									om.SetUpdatedStatus(itemRefObj, invPropertyMap.Name, true);
								}
							}
						}
					}
				}
			}
			else
			{
				refObj = om.GetPropertyValue(obj, propertyMap.Name);
				if (refObj != null)
				{
					PropertyStatus invPropertyStatus = this.Context.GetPropertyStatus(refObj, invPropertyMap.Name);
					//Ensure inverse is loaded
					if (invPropertyStatus == PropertyStatus.NotLoaded)
					{
						this.Context.LoadProperty(refObj, invPropertyMap.Name);
					}
					if (invPropertyMap.IsCollection)
					{												
						refList = ((IList) (om.GetPropertyValue(refObj, invPropertyMap.Name)));
						if (refList.Contains(obj))
						{
							mList = refList as IInterceptableList;					
							if (mList != null)
							{
								stackMute = mList.MuteNotify;
								mList.MuteNotify = true;
							}
							refList.Remove(obj);
							if (mList != null) {mList.MuteNotify = stackMute;}
							om.SetUpdatedStatus(refObj, invPropertyMap.Name, true);
						}

					}
					else
					{
						//only update back ref if it is actually pointing at me
						thisObj = om.GetPropertyValue(refObj, invPropertyMap.Name);
						if (thisObj != null)
						{
							if (thisObj == obj)
							{
								om.SetPropertyValue(refObj, invPropertyMap.Name, null);
								om.SetUpdatedStatus(refObj, invPropertyMap.Name, true);
							}
						}
					}

				}
			}
		}

		protected virtual void HandleSlavePropertySet(object obj, string propertyName, object value, object oldValue, bool hasOldValue)
		{
			IPropertyMap propertyMap = this.Context.DomainMap.MustGetClassMap(obj.GetType()).MustGetPropertyMap(propertyName);
			HandleSlavePropertySet(obj, propertyMap, value, oldValue, hasOldValue);
		}

		protected virtual void HandleSlavePropertySet(object obj, IPropertyMap propertyMap, object value, object oldValue, bool hasOldValue)
		{
			if (propertyMap.ReferenceType == ReferenceType.ManyToMany)
			{
				HandleSlaveManyManyPropertySet(obj, propertyMap, (IList) value, (IList) oldValue);				
			}
			if (propertyMap.ReferenceType == ReferenceType.ManyToOne)
			{
				HandleSlaveManyOnePropertySet(obj, propertyMap, (IList) value, (IList) oldValue);				
			}
			if (propertyMap.ReferenceType == ReferenceType.OneToMany)
			{
				throw new Exception("For a OneMany/ManyOne relationship, the list side should always be marked in the mapping file as read-only and the reference side read-write!"); // do not localize				
			}
			if (propertyMap.ReferenceType == ReferenceType.OneToOne)
			{
				if (!(hasOldValue))
				{
					oldValue = this.Context.ObjectManager.GetPropertyValue(obj, propertyMap.Name);
				}
				HandleSlaveOneOnePropertySet(obj, propertyMap, value, oldValue);				
			}
			
		}

		protected virtual ArrayList GetListDiff(IList list1, IList list2)
		{
			ArrayList diff = new ArrayList() ;
			if (list2 != null)
			{
				if (list1 == null)
				{
					foreach (object oldValue in list2)
					{
						diff.Add(oldValue);
					}										
				}
				else
				{
					foreach (object oldValue in list2)
					{
						if (!(list1.Contains(oldValue)))
						{
							diff.Add(oldValue);
						}
					}					
				}
			}
			return diff;
		}

		protected virtual void HandleSlaveManyManyPropertySet(object obj, IPropertyMap propertyMap, IList newList, IList oldList)
		{
            LogMessage message = new LogMessage("Managing inverse many-many property relationship synchronization");
            LogMessage verbose = new LogMessage("Writing to object of type: {0}, Property: {1}", obj.GetType(), propertyMap.Name);

			this.Context.LogManager.Info(this,message, verbose); // do not localize

			IPropertyMap invPropertyMap = propertyMap.GetInversePropertyMap();
			if ( invPropertyMap == null) { return ;}
			ArrayList added = GetListDiff(oldList, newList) ;
			ArrayList removed = GetListDiff(newList, oldList) ;
			IObjectManager om = this.Context.ObjectManager;
			IUnitOfWork uow = this.Context.UnitOfWork;
			IList list;
			IInterceptableList mList;
			bool stackMute = false;
			object value;
			foreach (object iValue in added)
			{
				value = iValue;
				om.EnsurePropertyIsLoaded(value, invPropertyMap);
				list = (IList) om.GetPropertyValue(value, invPropertyMap.Name);
				mList = list as IInterceptableList;
				if (mList != null)
				{
					stackMute = mList.MuteNotify;
					mList.MuteNotify = true;
				}
				list.Add(obj);
				if (mList != null) { mList.MuteNotify = stackMute; }
				uow.RegisterDirty(value);					
				om.SetUpdatedStatus(value, invPropertyMap.Name,  true);					
			}			
			foreach (object iValue in removed)
			{
				value = iValue;
				om.EnsurePropertyIsLoaded(value, invPropertyMap);
				list = (IList) om.GetPropertyValue(value, invPropertyMap.Name);
				mList = list as IInterceptableList;
				if (mList != null)
				{
					stackMute = mList.MuteNotify;
					mList.MuteNotify = true;
				}
				list.Remove(obj);
				if (mList != null) { mList.MuteNotify = stackMute; }
				uow.RegisterDirty(value);					
				om.SetUpdatedStatus(value, invPropertyMap.Name,  true);					
			}
		}	

		protected virtual void HandleSlaveManyOnePropertySet(object obj, IPropertyMap propertyMap, IList newList, IList oldList)
		{
            LogMessage message = new LogMessage("Managing inverse many-one property relationship synchronization");
            LogMessage verbose = new LogMessage("Writing to object of type: {0}, Property: {1}", obj.GetType(), propertyMap.Name);
			this.Context.LogManager.Info(this, message,verbose); // do not localize

			IPropertyMap invPropertyMap = propertyMap.GetInversePropertyMap();
			if ( invPropertyMap == null) { return ;}
			ArrayList added = GetListDiff(oldList, newList) ;
			ArrayList removed = GetListDiff(newList, oldList) ;
			IObjectManager om = this.Context.ObjectManager;
			IUnitOfWork uow = this.Context.UnitOfWork;
			IList list;
			IInterceptableList mList;
			object value;
			object oldObj;
			bool stackMute = false;
			foreach (object iValue in added)
			{
				value = iValue;
				om.EnsurePropertyIsLoaded(value, invPropertyMap);
				oldObj = om.GetPropertyValue(value, invPropertyMap.Name);
				if (!(oldObj == obj))
				{
					om.SetPropertyValue(value, invPropertyMap.Name, obj);
					om.SetNullValueStatus(value, invPropertyMap.Name, obj == null);
					om.SetUpdatedStatus(value, invPropertyMap.Name, true);
                    message = new LogMessage("Wrote back-reference in inverse property");
                    verbose = new LogMessage("Wrote to object of type: {0}, Inverse Property: {1}", value.GetType(),invPropertyMap.Name);

					this.Context.LogManager.Debug(this,message , verbose); // do not localize
					uow.RegisterDirty(value);			
					if (oldObj != null)
					{
						om.EnsurePropertyIsLoaded(oldObj, propertyMap);
						list = (IList) om.GetPropertyValue(oldObj, propertyMap.Name);
						mList = list as IInterceptableList;					
						if (mList != null)
						{
							stackMute = mList.MuteNotify;
							mList.MuteNotify = true;
						}
						list.Remove(value);
						if (mList != null) { mList.MuteNotify = stackMute; }
						uow.RegisterDirty(oldObj);					
						om.SetUpdatedStatus(oldObj, invPropertyMap.Name, true);
                        message = new LogMessage("Removed back-reference in inverse property");
                        verbose = new LogMessage("Wrote to object of type: {0}, Inverse Property: {1}" , oldObj.GetType(), propertyMap.Name);

						this.Context.LogManager.Debug(this, message, verbose); // do not localize
					}					
				}
			}			
			foreach (object iValue in removed)
			{
				value = iValue;
				om.EnsurePropertyIsLoaded(value, invPropertyMap);
				oldObj = om.GetPropertyValue(value, invPropertyMap.Name);
				om.SetPropertyValue(value, invPropertyMap.Name, null);
				om.SetNullValueStatus(value, invPropertyMap.Name, true);
				om.SetUpdatedStatus(value, invPropertyMap.Name, true);
				uow.RegisterDirty(value);					
				if (oldObj != null)
				{
					om.EnsurePropertyIsLoaded(oldObj, propertyMap);
					list = (IList) om.GetPropertyValue(oldObj, propertyMap.Name);
					mList = list as IInterceptableList;					
					if (mList != null)
					{
						stackMute = mList.MuteNotify;
						mList.MuteNotify = true;
					}
					list.Remove(value);
					if (mList != null) { mList.MuteNotify = stackMute; }
					uow.RegisterDirty(oldObj);					
					om.SetUpdatedStatus(oldObj, invPropertyMap.Name, true);
				}					
			}
		}	

		protected virtual void HandleSlaveOneOnePropertySet(object obj, IPropertyMap propertyMap, object value, object oldValue)
		{
            LogMessage message = new LogMessage("Managing inverse one-one property relationship synchronization");
            LogMessage verbose = new LogMessage("Writing to object of type: {0}, Property: {1}" , obj.GetType(),propertyMap.Name);

			this.Context.LogManager.Info(this, message, verbose); // do not localize

			IPropertyMap invPropertyMap = propertyMap.GetInversePropertyMap();
			if ( invPropertyMap == null) { return ;}
			IObjectManager om = this.Context.ObjectManager;
			IUnitOfWork uow = this.Context.UnitOfWork;
			if (value != null)
			{
				om.EnsurePropertyIsLoaded(value, invPropertyMap);
				om.SetPropertyValue(value, invPropertyMap.Name, obj);
				om.SetNullValueStatus(value, invPropertyMap.Name, false);
				om.SetUpdatedStatus(value, invPropertyMap.Name, true);
				uow.RegisterDirty(value);
                message = new LogMessage("Wrote back-reference to inverse property");
                verbose = new LogMessage( "Wrote to object of type: {0}, Inverse Property: {1}" , value.GetType(), invPropertyMap.Name);
				this.Context.LogManager.Debug(this,message ,verbose); // do not localize
			}

			if (oldValue != null)
			{
				om.EnsurePropertyIsLoaded(oldValue, invPropertyMap);
				om.SetPropertyValue(oldValue, invPropertyMap.Name, null);
				om.SetNullValueStatus(oldValue, invPropertyMap.Name, false);
				om.SetUpdatedStatus(oldValue, invPropertyMap.Name, true);
				uow.RegisterDirty(oldValue);
                message = new LogMessage("Wrote null to inverse property");
                verbose = new LogMessage("Wrote to object of type: {0}, Inverse Property: " , oldValue.GetType(),invPropertyMap.Name);
				this.Context.LogManager.Debug(this, message,verbose); // do not localize
			}
		}	

		public virtual void NotifyPropertyLoad(object obj, IPropertyMap propertyMap, object value)
		{
		}

        public virtual void Clear()
        {

        }

	}
}
