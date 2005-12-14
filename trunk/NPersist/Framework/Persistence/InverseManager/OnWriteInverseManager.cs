//// *
//// * Copyright (C) 2005 Mats Helander
//// *
//// * This library is free software; you can redistribute it and/or modify it
//// * under the terms of the GNU Lesser General Public License 2.1 or later, as
//// * published by the Free Software Foundation. See the included license.txt
//// * or http://www.gnu.org/copyleft/lesser.html for details.
//// *
//// *
//
//using System;
//using System.Collections;
//using System.Diagnostics;
//using Puzzle.NPersist.Framework.Enumerations;
//using Puzzle.NPersist.Framework.Interfaces;
//using Puzzle.NPersist.Framework.Mapping;
//
//namespace Puzzle.NPersist.Framework.Persistence
//{
//	/// <summary>
//	/// Summary description for OnWriteInverseManager.
//	/// </summary>
//	public class OnWriteInverseManager : InverseManagerBase
//	{
//
//		private Hashtable inverseActions = new Hashtable();
//
//		public OnWriteInverseManager()
//		{		
//		}
//
//		
//		public override InverseManagerType InverseManagerType
//		{
//			get { return InverseManagerType.OnWrite; }
//			set { if (value != InverseManagerType.OnWrite ) { throw new Exception("Can't set inverse manager type of OnWrite inverse manager to anything else than OnWrite"); } } // do not localize
//		}
//		
//		public override void NotifyPropertyGet(object obj, string propertyName)
//		{
//			ArrayList actions = GetActionsForProperty(obj, propertyName);
//			foreach (InverseAction action in actions)
//			{
//				if (action.ActionType == InverseActionType.Add)
//				{
//					PerformAddAction(action);					
//				}
//				else if (action.ActionType == InverseActionType.Remove)
//				{
//					PerformRemoveAction(action);					
//				}
//				else if (action.ActionType == InverseActionType.Set)
//				{
//					PerformSetAction(action);					
//				}
//			}
//		}
//
//		public override void NotifyCreate(object obj)
//		{
//			;
//		}
//
//		public override void NotifyPersist(object obj)
//		{
//			;
//		}
//
//		public override void NotifyDelete(object obj)
//		{
//			;
//		}
//
//		//[DebuggerStepThrough()]
//		public override void NotifyPropertySet(object obj, string propertyName, object value)
//		{
//			DoNotifyPropertySet(obj, propertyName, value, null, false);
//		}
//
//		public override void NotifyPropertySet(object obj, string propertyName, object value, object oldValue)
//		{
//			DoNotifyPropertySet(obj, propertyName, value, oldValue, true);
//		}
//
//		//[DebuggerStepThrough()]
//		protected virtual void DoNotifyPropertySet(object obj, string propertyName, object value, object oldValue, bool hasOldValue)
//		{
//			IPropertyMap propertyMap = this.Context.DomainMap.MustGetClassMap(obj.GetType()).MustGetPropertyMap(propertyName);
//			if (propertyMap.NoInverseManagement)
//			{
//				return;
//			}
//			if (propertyMap.ReferenceType == ReferenceType.None)
//			{
//				return;
//			}
//			if (!(propertyMap.Inverse.Length > 0))
//			{
//				return;
//			}
//			if (propertyMap.IsReadOnly)
//			{
//				HandleReadOnlyPropertySet(obj, propertyMap, value, oldValue, hasOldValue);
//				return;
//			}
//			if (propertyMap.ReferenceType == ReferenceType.ManyToMany)
//			{
//				HandleManyManyPropertySet(obj, propertyMap, (IList) value, (IList) oldValue);				
//			}
//			if (propertyMap.ReferenceType == ReferenceType.ManyToOne)
//			{
//				//HandleManyOnePropertySet(obj, propertyMap, (IList) value, (IList) oldValue);				
//				//throw new Exception("For a OneMany/ManyOne relationship, the list side should always be marked in the mapping file as read-only and the reference side read-write!"); // do not localize			
//				HandleReadOnlyManyOnePropertySet(obj, propertyMap, (IList) value, (IList) oldValue);				
//			}
//			if (propertyMap.ReferenceType == ReferenceType.OneToMany)
//			{
//				if (!(hasOldValue))
//				{
//					oldValue = this.Context.ObjectManager.GetPropertyValue(obj, propertyMap.Name);
//				}
//				HandleOneManyPropertySet(obj, propertyMap, value, oldValue);				
//			}
//			if (propertyMap.ReferenceType == ReferenceType.OneToOne)
//			{
//				if (!(hasOldValue))
//				{
//					oldValue = this.Context.ObjectManager.GetPropertyValue(obj, propertyMap.Name);
//				}
//				HandleOneOnePropertySet(obj, propertyMap, value, oldValue);				
//			}
//
//		}
//
//		protected virtual void HandleManyManyPropertySet(object obj, IPropertyMap propertyMap, IList newList, IList oldList)
//		{
//			this.Context.LogManager.Info(this, "Managing inverse many-many property relationship synchronization", "Writing to object of type: " + obj.GetType().ToString() + ", Property: " + propertyMap.Name); // do not localize
//
//			PropertyStatus propStatus;
//			IPropertyMap invPropertyMap = propertyMap.GetInversePropertyMap();
//			if ( invPropertyMap == null) { return ;}
//			ArrayList added = GetListDiff(oldList, newList) ;
//			ArrayList removed = GetListDiff(newList, oldList) ;
//			IObjectManager om = this.Context.ObjectManager;
//			IList list;
//			IInterceptableList mList;
//			bool stackMute = false;
//			object value;
//			foreach (object iValue in added)
//			{
//				value = iValue;
//				propStatus = om.GetPropertyStatus(value, invPropertyMap.Name);
//				if (propStatus == PropertyStatus.NotLoaded)
//				{
//					value = iValue;
//					AddAction(InverseActionType.Add, value, invPropertyMap.Name, obj);					
//				}
//				else
//				{
//					//we still ensure so that the object is also always loaded...
//					om.EnsurePropertyIsLoaded(value, invPropertyMap);
//					list = (IList) om.GetPropertyValue(value, invPropertyMap.Name);
//					mList = list as IInterceptableList;
//					if (mList != null)
//					{
//						stackMute = mList.MuteNotify;
//						mList.MuteNotify = true;
//					}
//					list.Add(obj);
//					if (mList != null) { mList.MuteNotify = stackMute; }
//					om.SetUpdatedStatus(value, invPropertyMap.Name, true);
//				}
//			}			
//			foreach (object iValue in removed)
//			{
//				value = iValue;
//				propStatus = om.GetPropertyStatus(value, invPropertyMap.Name);
//				if (propStatus == PropertyStatus.NotLoaded)
//				{
//					value = iValue;
//					AddAction(InverseActionType.Remove, value, invPropertyMap.Name, obj);					
//				}
//				else
//				{
//					//we still ensure so that the object is also always loaded...
//					om.EnsurePropertyIsLoaded(value, invPropertyMap);
//					list = (IList) om.GetPropertyValue(value, invPropertyMap.Name);
//					mList = list as IInterceptableList;
//					if (mList != null)
//					{
//						stackMute = mList.MuteNotify;
//						mList.MuteNotify = true;
//					}
//					list.Remove(obj);
//					if (mList != null) { mList.MuteNotify = stackMute; }
//					om.SetUpdatedStatus(value, invPropertyMap.Name, true);
//				}
//			}
//		}
//
//		protected virtual void HandleOneManyPropertySet(object obj, IPropertyMap propertyMap, object value, object oldValue)
//		{
//			this.Context.LogManager.Info(this, "Managing inverse one-many property relationship synchronization", "Writing to object of type: " + obj.GetType().ToString() + ", Property: " + propertyMap.Name); // do not localize
//
//			PropertyStatus propStatus;
//			IPropertyMap invPropertyMap = propertyMap.GetInversePropertyMap();
//			if ( invPropertyMap == null) { return ;}
//			IObjectManager om = this.Context.ObjectManager;
//			IList list;
//			IInterceptableList mList;
//			bool stackMute = false;
//
//			if (value != null)
//			{
//				propStatus = om.GetPropertyStatus(value, invPropertyMap.Name);
//				if (propStatus == PropertyStatus.NotLoaded)
//				{
//					AddAction(InverseActionType.Add, value, invPropertyMap.Name, obj);					
//				}
//				else
//				{
//					om.EnsurePropertyIsLoaded(value, invPropertyMap);
//					list = (IList) om.GetPropertyValue(value, invPropertyMap.Name);
//					mList = list as IInterceptableList;
//					if (mList != null)
//					{
//						stackMute = mList.MuteNotify;
//						mList.MuteNotify = true;
//					}
//					list.Add(obj);
//					if (mList != null) { mList.MuteNotify = stackMute; }
//					om.SetUpdatedStatus(value, invPropertyMap.Name, true);
//				}				
//			}
//
//			if (oldValue != null)
//			{
//				propStatus = om.GetPropertyStatus(oldValue, invPropertyMap.Name);
//				if (propStatus == PropertyStatus.NotLoaded)
//				{
//					AddAction(InverseActionType.Remove, oldValue, invPropertyMap.Name, obj);					
//				}
//				else
//				{
//					om.EnsurePropertyIsLoaded(oldValue, invPropertyMap);
//					list = (IList) om.GetPropertyValue(oldValue, invPropertyMap.Name);
//					mList = list as IInterceptableList;
//					if (mList != null)
//					{
//						stackMute = mList.MuteNotify;
//						mList.MuteNotify = true;
//					}
//					list.Remove(obj);
//					if (mList != null) { mList.MuteNotify = stackMute; }
//				}				
//			}
//		}
//
//		protected virtual void HandleOneOnePropertySet(object obj, IPropertyMap propertyMap, object value, object oldValue)
//		{
//			this.Context.LogManager.Info(this, "Managing inverse one-one property relationship synchronization", "Writing to object of type: " + obj.GetType().ToString() + ", Property: " + propertyMap.Name); // do not localize
//
//			PropertyStatus propStatus;
//
//			IPropertyMap invPropertyMap = propertyMap.GetInversePropertyMap();
//			if ( invPropertyMap == null) { return ;}
//			IObjectManager om = this.Context.ObjectManager;
//			if (value != null)
//			{
//				propStatus = om.GetPropertyStatus(value, invPropertyMap.Name);
//				if (propStatus == PropertyStatus.NotLoaded)
//				{
//					AddAction(InverseActionType.Set, value, invPropertyMap.Name, obj);					
//				}
//				else
//				{
//					om.EnsurePropertyIsLoaded(value, invPropertyMap);
//					om.SetPropertyValue(value, invPropertyMap.Name, obj);
//					om.SetNullValueStatus(value, invPropertyMap.Name, false);
//					om.SetUpdatedStatus(value, invPropertyMap.Name, true);
//				}
//			}
//
//			if (oldValue != null)
//			{
//				propStatus = om.GetPropertyStatus(value, invPropertyMap.Name);
//				if (propStatus == PropertyStatus.NotLoaded)
//				{
//					AddAction(InverseActionType.Set, value, invPropertyMap.Name, null);					
//				}
//				else
//				{
//					om.EnsurePropertyIsLoaded(oldValue, invPropertyMap);
//					om.SetPropertyValue(oldValue, invPropertyMap.Name, null);
//					om.SetNullValueStatus(oldValue, invPropertyMap.Name, false);
//					om.SetUpdatedStatus(oldValue, invPropertyMap.Name, true);
//				}
//			}
//		}
//
//		protected virtual void AddAction(InverseActionType actionType, object obj, string propertyName, object value)
//		{
//			if (value == null)
//				this.Context.LogManager.Debug(this, "Caching inverse action", "Action type: " + actionType.ToString() + ", Object of type: " + obj.GetType().ToString() + ", Property: " + propertyName + ", Value: null"); // do not localize
//			else
//				this.Context.LogManager.Debug(this, "Caching inverse action", "Action type: " + actionType.ToString() + ", Object of type: " + obj.GetType().ToString() + ", Property: " + propertyName + ", Value type: " + value.GetType().ToString()  ); // do not localize
//
//			InverseAction action = new InverseAction() ;
//			action.ActionType = actionType ;
//			action.Obj = obj;
//			action.PropertyName = propertyName;
//			action.Value = value;
//			Hashtable objActions;
//			ArrayList propActions;
//			if (!(inverseActions.ContainsKey(obj)))
//			{
//				inverseActions[obj] = new Hashtable();
//			}
//			objActions = (Hashtable) inverseActions[obj] ;
//			if (!(objActions.ContainsKey(propertyName)))
//			{
//				objActions[propertyName] = new ArrayList();
//			}
//			propActions = (ArrayList) objActions[propertyName] ;
//			propActions.Add(action);			
//		}
//
//		protected virtual ArrayList GetActionsForProperty(object obj, string propertyName)
//		{
//			Hashtable objActions;
//			ArrayList propActions;
//			if (!(inverseActions.ContainsKey(obj)))
//			{
//				inverseActions[obj] = new Hashtable();
//			}
//			objActions = (Hashtable) inverseActions[obj] ;
//			if (!(objActions.ContainsKey(propertyName)))
//			{
//				objActions[propertyName] = new ArrayList();
//			}
//			propActions = (ArrayList) objActions[propertyName] ;
//			objActions.Remove(propertyName);
//			if (objActions.Count < 1)
//			{
//				inverseActions.Remove(obj);				
//			}
//			return propActions;			
//		}
//
//		protected virtual void PerformAddAction(InverseAction action)
//		{
//			object obj = action.Obj;
//			string propertyName = action.PropertyName;
//			object value = action.Value;
//			IPropertyMap propertyMap = this.Context.DomainMap.MustGetClassMap(obj.GetType()).MustGetPropertyMap(propertyName);
//			IObjectManager om = this.Context.ObjectManager;
//			IList list;
//			IInterceptableList mList;
//			bool stackMute = false;
//			om.EnsurePropertyIsLoaded(obj, propertyMap);
//			list = (IList) om.GetPropertyValue(obj, propertyName);
//			mList = list as IInterceptableList;
//			if (mList != null)
//			{
//				stackMute = mList.MuteNotify;
//				mList.MuteNotify = true;
//			}
//			list.Add(value);
//			if (mList != null) { mList.MuteNotify = stackMute; }
//			om.SetUpdatedStatus(obj, propertyName, true);
//
//			this.Context.LogManager.Debug(this, "Performed cached inverse action", "Action type: " + action.ActionType.ToString() + ", Wrote to object of type: " + obj.GetType().ToString() + ", Property: " + propertyName + ", Value object type: " + value.GetType().ToString()  ); // do not localize
//		}
//
//		protected virtual void PerformRemoveAction(InverseAction action)
//		{
//			object obj = action.Obj;
//			string propertyName = action.PropertyName;
//			object value = action.Value;
//			IPropertyMap propertyMap = this.Context.DomainMap.MustGetClassMap(obj.GetType()).MustGetPropertyMap(propertyName);
//			IObjectManager om = this.Context.ObjectManager;
//			IList list;
//			IInterceptableList mList;
//			bool stackMute = false;
//			om.EnsurePropertyIsLoaded(obj, propertyMap);
//			list = (IList) om.GetPropertyValue(obj, propertyName);
//			mList = list as IInterceptableList;
//			if (mList != null)
//			{
//				stackMute = mList.MuteNotify;
//				mList.MuteNotify = true;
//			}
//			list.Remove(value);
//			if (mList != null) { mList.MuteNotify = stackMute; }
//			om.SetUpdatedStatus(obj, propertyName, true);
//
//			this.Context.LogManager.Debug(this, "Performed cached inverse action", "Action type: " + action.ActionType.ToString() + ", Wrote to object of type: " + obj.GetType().ToString() + ", Property: " + propertyName + ", Value object type: " + value.GetType().ToString()  ); // do not localize
//		}
//
//		protected virtual void PerformSetAction(InverseAction action)
//		{
//			object obj = action.Obj;
//			string propertyName = action.PropertyName;
//			object value = action.Value;
//			IPropertyMap propertyMap = this.Context.DomainMap.MustGetClassMap(obj.GetType()).MustGetPropertyMap(propertyName);
//			IObjectManager om = this.Context.ObjectManager;
//			om.EnsurePropertyIsLoaded(obj, propertyMap);
//			om.SetPropertyValue(obj, propertyName, value);
//			om.SetNullValueStatus(obj, propertyName, false);
//			om.SetUpdatedStatus(obj, propertyName, true);
//
//			if (value == null)
//				this.Context.LogManager.Debug(this, "Performed cached inverse action", "Action type: " + action.ActionType.ToString() + ", Wrote to object of type: " + obj.GetType().ToString() + ", Property: " + propertyName + ", Value: null"); // do not localize
//			else
//				this.Context.LogManager.Debug(this, "Performed cached inverse action", "Action type: " + action.ActionType.ToString() + ", Wrote to object of type: " + obj.GetType().ToString() + ", Property: " + propertyName + ", Value object type: " + value.GetType().ToString()  ); // do not localize
//
//		}
//
//
//	}
//}
