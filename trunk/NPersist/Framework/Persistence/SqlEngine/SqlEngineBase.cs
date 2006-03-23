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
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.EventArguments;
using Puzzle.NPersist.Framework.Exceptions;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.NPersist.Framework.Querying;
using Puzzle.NPersist.Framework.Sql.Dom;
using Puzzle.NPersist.Framework.Sql.Visitor;
using Puzzle.NPersist.Framework.Utility;

//

namespace Puzzle.NPersist.Framework.Persistence
{
	public class SqlEngineBase : ISqlEngine
	{
		private ISqlEngineManager m_SqlEngineManager;
		private string m_DateDelimiter = "'";

		public IContext Context { 
			get
			{
				if (m_SqlEngineManager != null) 
					return m_SqlEngineManager.Context; 
				return null;
			}
		}

		#region Template

		#region Property  AutoIncreaserStrategy
		
		private AutoIncreaserStrategy autoIncreaserStrategy = AutoIncreaserStrategy.SelectNewIdentity;
		
		public virtual AutoIncreaserStrategy AutoIncreaserStrategy
		{
			get { return this.autoIncreaserStrategy; }
			set { this.autoIncreaserStrategy = value; }
		}
		
		#endregion

		#region Property  SelectNewIdentity
		
		private string selectNewIdentity = "SELECT @@IDENTITY;";
		
		public virtual string SelectNewIdentity
		{
			get { return this.selectNewIdentity; }
			set { this.selectNewIdentity = value; }
		}
		
		#endregion

		#region Method  SelectNextSequence
				
		public virtual string GetSelectNextSequence(string sequenceName)
		{
			return "SELECT " + sequenceName + ".nextval FROM dual";
		}
		
		#endregion

		#region Property  StatementDelimiter
		
		private string statementDelimiter = ";";
		
		public string StatementDelimiter
		{
			get { return this.statementDelimiter; }
			set { this.statementDelimiter = value; }
		}
		
		#endregion

		#endregion

		#region CRUD

		public virtual string DateDelimiter
		{
			get { return m_DateDelimiter; }
			set { m_DateDelimiter = value; }
		}

		public virtual ISqlEngineManager SqlEngineManager
		{
			get { return m_SqlEngineManager; }
			set { m_SqlEngineManager = value; }
		}

		public virtual void LoadObject(ref object obj)
		{
			this.SqlEngineManager.Context.LogManager.Info(this, "Loading object by id", "Type: " + obj.GetType().ToString()); // do not localize
			LoadObjectByIdOrKey(ref obj, "", "");
		}

		public virtual void LoadObjectByKey(ref object obj, string keyPropertyName, object keyValue)
		{
			this.SqlEngineManager.Context.LogManager.Info(this, "Loading object by key property", "Type: " + obj.GetType().ToString() + ", Property: " + keyPropertyName + ", Value: " + keyValue); // do not localize
			LoadObjectByIdOrKey(ref obj, keyPropertyName, keyValue);
		}

		public virtual void InsertObject(object obj, IList stillDirty)
		{
			this.SqlEngineManager.Context.LogManager.Info(this, "Inserting object", "Type: " + obj.GetType().ToString()); // do not localize

			ArrayList propertyNames = new ArrayList();
			IList parameters = new ArrayList() ;
			ArrayList nonPrimaryPropertyMaps = new ArrayList();
			ArrayList collectionPropertyMaps = new ArrayList();
			ArrayList sqlAndParamsList = new ArrayList();
			int rowsAffected;
			IPropertyMap autoProp;
			IContext ctx = m_SqlEngineManager.Context;
			ObjectCancelEventArgs e = new ObjectCancelEventArgs(obj);
			ctx.EventManager.OnInsertingObject(this, e);
			if (e.Cancel)
			{
				return;
			}
			IObjectManager om = ctx.ObjectManager;
			IListManager lm = ctx.ListManager;
			IClassMap classMap = ctx.DomainMap.MustGetClassMap(obj.GetType());
			IDataSource ds = ctx.DataSourceManager.GetDataSource(obj);
			if (classMap.HasSingleIdAutoIncreaser() && this.AutoIncreaserStrategy == AutoIncreaserStrategy.SelectNextSequence)
			{
				autoProp = classMap.GetAutoIncreasingIdentityPropertyMap();
				IColumnMap seqColMap = autoProp.MustGetColumnMap();
				string seqName = seqColMap.Sequence ;
				if (seqName.Length < 1)
					throw new Exception("The column " + seqColMap.Name + " must have a sequence name associated with it (sequence=\"my_seq_name\" in the xml map file) since the column is marked as an autoincreaser and the SelectNextSequence AutoIncreasingStrategy is used!");

				string sqlSeq = GetSelectNextSequence(seqName);
				object newId = ctx.SqlExecutor.ExecuteScalar(sqlSeq, ds, new ArrayList() );					
				UpdateAutoIncIdProperty(om, obj, classMap, newId);
				
			}			
			string sql = GetInsertStatement(obj, propertyNames, stillDirty, nonPrimaryPropertyMaps, collectionPropertyMaps, parameters);
			if (classMap.HasSingleIdAutoIncreaser() && this.AutoIncreaserStrategy == AutoIncreaserStrategy.SelectNewIdentity)
			{
				sql += this.StatementDelimiter + this.SelectNewIdentity;

				object[,] result = (object[,]) ctx.SqlExecutor.ExecuteArray(sql, ds, parameters);					
				if (Util.IsArray(result))
				{
					object newId = result[0, 0];
					UpdateAutoIncIdProperty(om, obj, classMap, newId);
				}
				else
				{
					throw new FailedFetchingDbGeneratedValueException("Could not find auto-increasing ID for new object!"); // do not localize
				}
			}
			else
			{
				rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, parameters);
				if (rowsAffected < 1)
				{
					throw new RowNotInsertedException("A new row was not inserted in the data source for a new object."); // do not localize
				}
			}
			parameters.Clear();
//			foreach (string propName in propertyNames)
//			{
//				if (om.GetNullValueStatus(obj, propName))
//				{
//					om.SetOriginalPropertyValue(obj, propName, System.DBNull.Value);						
//				}
//				else
//				{						
//					om.SetOriginalPropertyValue(obj, propName, om.GetPropertyValue(obj, propName));
//				}
//			}
			InsertNonPrimaryProperties(obj, nonPrimaryPropertyMaps, stillDirty);
			foreach (IPropertyMap propertyMap in collectionPropertyMaps)
			{
				sqlAndParamsList.Clear();
				GetInsertCollectionPropertyStatements(obj, propertyMap, sqlAndParamsList, stillDirty);
				ds = ctx.DataSourceManager.GetDataSource(obj, propertyMap.Name);
				foreach (SqlStatementAndDbParameters sqlAndParams in sqlAndParamsList)
				{
					sql = sqlAndParams.SqlStatement;
					rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, sqlAndParams.DbParameters);
					if (rowsAffected < 1)
					{
						throw new RowNotInsertedException("A new row was not inserted in the data source for a collection property of a new object."); // do not localize
					}
				}
			}
			if (stillDirty.Count > 0)
			{
				ctx.UnitOfWork.RegisterDirty(obj);
			}
			ctx.InverseManager.NotifyCreate(obj);
			ObjectEventArgs e2 = new ObjectEventArgs(obj);
			ctx.EventManager.OnInsertedObject(this, e2);
		}

		public virtual void UpdateAutoIncIdProperty(IObjectManager om, object obj, IClassMap classMap, object newId)
		{
			IContext ctx = m_SqlEngineManager.Context;
			string prevId;
			string autoPropName;
			IPropertyMap autoProp;
			object autoID = null;
			prevId = om.GetObjectIdentity(obj);
			autoProp = classMap.GetAutoIncreasingIdentityPropertyMap();
			autoPropName = autoProp.Name;					
			PropertyInfo propInfo = obj.GetType().GetProperty(autoProp.Name);
	
			if (propInfo.PropertyType == typeof(System.Int64))
			{
				autoID = Convert.ToInt64(newId);						
			}
			else if (propInfo.PropertyType == typeof(System.Int16))
			{
				autoID = Convert.ToInt16(newId);						
			}
			else if (propInfo.PropertyType == typeof(System.Double))
			{
				autoID = Convert.ToDouble(newId);						
			}
			else if (propInfo.PropertyType == typeof(System.Decimal))
			{
				autoID = Convert.ToDecimal(newId);						
			}
			else
			{
				autoID = Convert.ToInt32(newId);						
			}					
			om.SetPropertyValue(obj, autoPropName, autoID);
			//om.SetOriginalPropertyValue(obj, autoPropName, autoID);
			om.SetNullValueStatus(obj, autoPropName, false);
			ctx.IdentityMap.UpdateIdentity(obj, prevId);
		}


		public virtual void UpdateObject(object obj, IList stillDirty)
		{
			this.SqlEngineManager.Context.LogManager.Info(this, "Updating object", "Type: " + obj.GetType().ToString()); // do not localize
			IList parameters = new ArrayList() ;
			IContext ctx = m_SqlEngineManager.Context;
			IObjectManager om = ctx.ObjectManager;
			IListManager lm = ctx.ListManager;
			ArrayList propertyMaps = new ArrayList();
			ArrayList nonPrimaryPropertyMaps = new ArrayList();
			ArrayList collectionPropertyMaps = new ArrayList();
			ArrayList sqlAndParamsList = new ArrayList();
			IDataSource ds;
			int rowsAffected;
			ObjectCancelEventArgs e = new ObjectCancelEventArgs(obj);
			ctx.EventManager.OnUpdatingObject(this, e);
			if (e.Cancel)
			{
				return;
			}
			IClassMap classMap = ctx.DomainMap.MustGetClassMap(obj.GetType());
			string sql = GetUpdateStatement(obj, propertyMaps, stillDirty, nonPrimaryPropertyMaps, collectionPropertyMaps, parameters);
			if (sql != "")
			{
				ds = ctx.DataSourceManager.GetDataSource(obj);
				rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, parameters);
				if (rowsAffected < 1)
				{
					if (!(SqlEngineManager.Context.PersistenceManager.GetUpdateOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, classMap) == OptimisticConcurrencyBehaviorType.Disabled))
					{
						throw new OptimisticConcurrencyException("An optimistic concurrency exception occurred when updating the row for object " + obj.GetType().ToString() + om.GetObjectKeyOrIdentity(obj) + ". The row may have been modified or deleted by another thread or user.\r\n", obj); // do not localize
					}
				}
//				foreach (IPropertyMap propertyMap in propertyMaps)
//				{
//					if (om.GetNullValueStatus(obj, propertyMap.Name))
//					{
//						om.SetOriginalPropertyValue(obj, propertyMap.Name, System.DBNull.Value);						
//					}
//					else
//					{						
//						om.SetOriginalPropertyValue(obj, propertyMap.Name, om.GetPropertyValue(obj, propertyMap.Name));						
//					}
//				}
			}
			parameters.Clear();
			UpdateNonPrimaryProperties(obj, nonPrimaryPropertyMaps, stillDirty);
			foreach (IPropertyMap propertyMap in collectionPropertyMaps)
			{
				sqlAndParamsList.Clear();
				GetUpdateCollectionPropertyStatements(obj, propertyMap, sqlAndParamsList, stillDirty);
				ds = ctx.DataSourceManager.GetDataSource(obj, propertyMap.Name);
				foreach (SqlStatementAndDbParameters sqlAndParams in sqlAndParamsList)
				{
					sql = sqlAndParams.SqlStatement;
					rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, sqlAndParams.DbParameters );
					if (rowsAffected < 1)
					{
						if (!(SqlEngineManager.Context.PersistenceManager.GetUpdateOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, classMap) == OptimisticConcurrencyBehaviorType.Disabled))
						{
							throw new OptimisticConcurrencyException("An optimistic concurrency exception occurred when updating a collection property for object " + obj.GetType().ToString() + om.GetObjectKeyOrIdentity(obj) + ". The property may have been modified by another thread or user.", obj); // do not localize
						}
					}
				}
			}
			ctx.InverseManager.NotifyPersist(obj);
			ObjectEventArgs e2 = new ObjectEventArgs(obj);
			ctx.EventManager.OnUpdatedObject(this, e2);
		}

		public virtual void RemoveObject(object obj)
		{
			this.SqlEngineManager.Context.LogManager.Info(this, "Removing object", "Type: " + obj.GetType().ToString()); // do not localize
			IList parameters = new ArrayList() ;
			IContext ctx = m_SqlEngineManager.Context;
			ObjectCancelEventArgs e = new ObjectCancelEventArgs(obj);
			ctx.EventManager.OnRemovingObject(this, e);
			if (e.Cancel)
			{
				return;
			}
			//RemoveAllReferencesToObject(obj);
			RemoveNonPrimaryProperties(obj);
			RemoveCollectionProperties(obj);
			string sql = GetDeleteStatement(obj, parameters);
			IDataSource ds = ctx.DataSourceManager.GetDataSource(obj);
			int rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, parameters);
			parameters.Clear();
			IClassMap classMap = ctx.DomainMap.MustGetClassMap(obj.GetType());
			if (rowsAffected < 1)
			{
				if (!(SqlEngineManager.Context.PersistenceManager.GetDeleteOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, classMap) == OptimisticConcurrencyBehaviorType.Disabled))
				{
					throw new OptimisticConcurrencyException("An optimistic concurrency exception occurred when deleting the row for object " + obj.GetType().ToString() + this.Context.ObjectManager.GetObjectKeyOrIdentity(obj) + " in the data source. The row may have been modified by another thread or user or is already deleted.", obj); // do not localize
				}
			}
			ctx.InverseManager.NotifyDelete(obj);
			ObjectEventArgs e2 = new ObjectEventArgs(obj);
			ctx.EventManager.OnRemovedObject(this, e2);
		}

		public virtual void NullifyReferencesToObject(object obj)
		{
			IDomainMap domainMap = m_SqlEngineManager.Context.DomainMap;
			IClassMap classMap = domainMap.MustGetClassMap(obj.GetType());
			ITableMap myTableMap;
			ITableMap primTableMap;
			myTableMap = classMap.GetTableMap();
			if (myTableMap != null)
			{
				foreach (ITableMap tableMap in myTableMap.SourceMap.TableMaps)
				{
					foreach (IColumnMap columnMap in tableMap.ColumnMaps)
					{
						if (columnMap.IsForeignKey)
						{
							primTableMap = columnMap.GetPrimaryKeyTableMap();
							if (primTableMap == myTableMap)
							{
								RemoveReferencesToObjectInTable(obj, classMap, myTableMap, tableMap);
								break;
							}									
						}
					}
				}
			}
		}

		protected void RemoveCollectionProperties(object obj)
		{
			IList parameters = new ArrayList() ;
			IContext ctx = m_SqlEngineManager.Context;
			IDomainMap domainMap = ctx.DomainMap;
			IClassMap classMap = domainMap.MustGetClassMap(obj.GetType());
			string sql;
			IDataSource ds;
			//TODO: Remove unused?
			int rowsAffected;
			foreach (IPropertyMap propertyMap in classMap.GetAllPropertyMaps())
			{
				if (propertyMap.IsCollection)
				{
					if (propertyMap.ReferenceType.Equals(ReferenceType.ManyToMany) || (!propertyMap.IsReadOnly && !propertyMap.IsSlave))
					{
						//if (!(propertyMap.ReferenceType == ReferenceType.ManyToMany))
						//{
							sql = GetRemoveCollectionPropertyStatement(obj, propertyMap, parameters);
							ds = ctx.DataSourceManager.GetDataSource(propertyMap.GetSourceMap());
							rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, parameters);
						//}
					}
				}
			}
		}

		protected virtual void RemoveNonPrimaryProperties(object obj)
		{
			IList parameters = new ArrayList() ;
			ArrayList propertyMaps = new ArrayList();
			Hashtable hashTables = new Hashtable();
			ITableMap tableMap;
			IDataSource ds = null;
			string key;
			string sql;
			int rowsAffected;
			IContext ctx = m_SqlEngineManager.Context;

			IClassMap classMap = ctx.DomainMap.MustGetClassMap(obj.GetType());
			tableMap = classMap.GetTableMap();
			foreach (IPropertyMap propertyMap in classMap.GetAllPropertyMaps())
			{
				if (!propertyMap.IsReadOnly && !propertyMap.IsSlave)
				{
					if (!(propertyMap.IsCollection))
					{
						if (!(propertyMap.GetTableMap() == tableMap))
						{
							if (!(propertyMap.InheritInverseMappings))
							{
								propertyMaps.Add(propertyMap);
							}
						}
					}
				}
			}
			foreach (IPropertyMap propertyMap in propertyMaps)
			{
				tableMap = propertyMap.GetTableMap();
				key = tableMap.SourceMap.Name + "." + tableMap.Name;
				if (!(hashTables.ContainsKey(key)))
				{
					hashTables[key] = new ArrayList();
				}
				((ArrayList) (hashTables[key])).Add(propertyMap);
			}
			foreach (ArrayList propertyList in hashTables.Values)
			{
				parameters.Clear() ;
				sql = GetRemoveNonPrimaryStatement(obj, propertyList, parameters);
				foreach (IPropertyMap propertyMap in propertyList)
				{
					ds = ctx.DataSourceManager.GetDataSource(obj, propertyMap.Name);
					break;
				}
				rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, parameters);
				if (rowsAffected < 1)
				{
					if (!(SqlEngineManager.Context.PersistenceManager.GetDeleteOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, classMap) == OptimisticConcurrencyBehaviorType.Disabled))
					{
						throw new OptimisticConcurrencyException("An optimistic concurrency exception occurred when updating the row in a non-primary table for object " + obj.GetType().ToString() + this.Context.ObjectManager.GetObjectKeyOrIdentity(obj) + " in the data source. The row may have been modified or deleted by another thread or user.", obj); // do not localize
					}
				}
			}
		}

		protected virtual void RemoveReferencesToObjectInTable(object obj, IClassMap classMap, ITableMap myTableMap, ITableMap tableMap)
		{
			this.SqlEngineManager.Context.LogManager.Debug(this, "Removing references to object in table", "Type: " + obj.GetType().ToString() + ", Table: " + tableMap.Name); // do not localize

			IList parameters = new ArrayList() ;
			IDomainMap domainMap = classMap.DomainMap;
			ITableMap primTableMap;
			ArrayList propertyMaps = new ArrayList();
			string sql;
			IContext ctx = m_SqlEngineManager.Context;
			IDataSource ds;
			//TODO: Remove unused?
			int rowsAffected;
			foreach (IColumnMap columnMap in tableMap.ColumnMaps)
			{
				if (columnMap.IsForeignKey) {
					primTableMap = columnMap.GetPrimaryKeyTableMap();
					if (primTableMap == myTableMap) {
						foreach (IPropertyMap propertyMap in domainMap.GetPropertyMapsForColumn(columnMap, true)) {
							if (!(propertyMaps.Contains(propertyMap))) {
								propertyMaps.Add(propertyMap);
							}
						}
					}
				}
			}
			ds = ctx.DataSourceManager.GetDataSource(tableMap.SourceMap);
			foreach (IPropertyMap propertyMap in propertyMaps)
			{
				parameters.Clear() ;
				sql = GetRemoveReferencesToObjectInTableStatement(obj, classMap, myTableMap, tableMap, propertyMap, parameters);
				if (sql.Length > 0)
				{
					//TODO: Remove unused?
					rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, parameters);
				}
			}
		}

		public virtual void LoadProperty(object obj, string propertyName)
		{
			this.SqlEngineManager.Context.LogManager.Info(this, "Loading property", "Property: " + propertyName + ", Object Type: " + obj.GetType().ToString()); // do not localize
			IList parameters;
			object value;
			object orgValue;
			IContext ctx = m_SqlEngineManager.Context;
			PropertyCancelEventArgs e = new PropertyCancelEventArgs(obj, propertyName);
			ctx.EventManager.OnLoadingProperty(this, e);
			if (e.Cancel)
			{
				return;
			}
			IClassMap classMap = ctx.DomainMap.MustGetClassMap(obj.GetType());
			IPropertyMap propertyMap = classMap.MustGetPropertyMap(propertyName);
			if (propertyMap.IsCollection)
			{
				LoadCollectionProperty(obj, propertyMap);
				return;
			}
			if (!(propertyMap.ReferenceType == ReferenceType.None))
			{
				LoadReferenceProperty(obj, propertyMap);
				return;
			}
			if (!(propertyMap.GetTableMap() == classMap.GetTableMap()))
			{
				LoadNonPrimaryProperty(obj, propertyMap);
				return;
			}
			parameters = new ArrayList() ;
			IObjectManager om = ctx.ObjectManager;
			IPersistenceManager pm = ctx.PersistenceManager;
			string sql = GetSelectPropertyStatement(obj, propertyName, parameters);
			IDataSource ds = ctx.DataSourceManager.GetDataSource(obj);
			object[,] result = (object[,]) ctx.SqlExecutor.ExecuteArray(sql, ds, parameters);
			if (Util.IsArray(result))
			{
				orgValue = result[0, 0];
				value = pm.ManageLoadedValue(obj, propertyMap, orgValue);
				om.SetPropertyValue(obj, propertyName, value);
				om.SetOriginalPropertyValue(obj, propertyName, orgValue);
				om.SetNullValueStatus(obj, propertyName, DBNull.Value.Equals(orgValue));
			}
			else
			{
				throw new ObjectNotFoundException("Object not found!"); // do not localize
			}
			PropertyEventArgs e2 = new PropertyEventArgs(obj, propertyName);
			ctx.EventManager.OnLoadedProperty(this, e2);
		}

		protected virtual void LoadCollectionProperty(object obj, IPropertyMap propertyMap)
		{
			IList parameters;
			object value;
			Type itemType = null;
			IList list;
			IInterceptableList mList;
			bool stackMute = false;
			IContext ctx = m_SqlEngineManager.Context;

			if (!(propertyMap.ReferenceType == ReferenceType.None))
			{
				itemType = m_SqlEngineManager.Context.AssemblyManager.MustGetTypeFromClassMap(propertyMap.GetReferencedClassMap());
				LoadReferenceCollectionProperty(obj, propertyMap, itemType);
				return;
			}
			parameters = new ArrayList() ;
			IList propList = m_SqlEngineManager.Context.ListManager.CreateList(obj, propertyMap);
			IObjectManager om = ctx.ObjectManager;
			IListManager lm = ctx.ListManager;
			IPersistenceManager pm = ctx.PersistenceManager;
			string sql = GetSelectCollectionPropertyStatement(obj, propertyMap.Name, parameters);
			IDataSource ds = ctx.DataSourceManager.GetDataSource(obj);
			object[,] result = (object[,]) ctx.SqlExecutor.ExecuteArray(sql, ds, parameters);
			if (Util.IsArray(result))
			{
				mList = propList as IInterceptableList;
				if (mList != null)
				{
					stackMute = mList.MuteNotify;
					mList.MuteNotify = true;
				}
				for (int row = 0; row <= result.GetUpperBound(1); row++)
				{
					value = result[0, row];
					if (!(propertyMap.ReferenceType == ReferenceType.None))
					{
						if (Convert.IsDBNull(value))
						{
							value = null;
						}
						else
						{
							value = pm.ManageReferenceValue(obj, propertyMap.Name, value);
						}
					}
					propList.Add(value);
				}
				if (mList != null)
					mList.MuteNotify = stackMute ;

				om.SetPropertyValue(obj, propertyMap.Name, propList);
				list = lm.CloneList(obj, propertyMap, propList);
				om.SetOriginalPropertyValue(obj, propertyMap.Name, list);
			}
			else
			{
				throw new ObjectNotFoundException("Object not found!"); // do not localize
			}
		}

		protected virtual void LoadNonPrimaryProperty(object obj, IPropertyMap propertyMap)
		{
			IList parameters = new ArrayList() ;
			string propName;
			object orgValue;
			object value;
			ArrayList propertyNames = new ArrayList();
			IContext ctx = m_SqlEngineManager.Context;
			IClassMap classMap = ctx.DomainMap.MustGetClassMap(obj.GetType());
			IObjectManager om = ctx.ObjectManager;
			IPersistenceManager pm = ctx.PersistenceManager;
			string sql = GetSelectNonPrimaryPropertyStatement(obj, propertyMap, propertyNames, parameters);
			IDataSource ds = ctx.DataSourceManager.GetDataSource(obj);
			object[,] result = (object[,]) ctx.SqlExecutor.ExecuteArray(sql, ds, parameters);
			if (Util.IsArray(result))
			{
				for (int row = 0; row <= result.GetUpperBound(1); row++)
				{
					for (int col = 0; col <= result.GetUpperBound(0); col++)
					{
						propName = (string) propertyNames[col];
						orgValue = result[col, row];
						propertyMap = classMap.MustGetPropertyMap(propName);
						value = pm.ManageLoadedValue(obj, propertyMap, orgValue);
						om.SetPropertyValue(obj, propName, value);
						om.SetOriginalPropertyValue(obj, propName, orgValue);
						om.SetNullValueStatus(obj, propName, DBNull.Value.Equals(orgValue));
					}
				}
			}
			else
			{
				throw new ObjectNotFoundException("Row for non-primary property '" + classMap.Name + "." + propertyMap.Name + "' not found!"); // do not localize
			}
		}

		protected virtual void LoadReferenceProperty(object obj, IPropertyMap propertyMap)
		{
			IList parameters = new ArrayList() ;
			object orgValue;
			IList idColumns = new ArrayList();
			IList typeColumns = new ArrayList();
			Hashtable hashPropertyColumnMap = new Hashtable();
			IContext ctx = m_SqlEngineManager.Context;
			
			IObjectManager om = ctx.ObjectManager;

			string sql = GetSelectSingleReferencePropertyStatement(obj, propertyMap, idColumns, typeColumns, hashPropertyColumnMap, parameters);
			Type propType = obj.GetType().GetProperty(propertyMap.Name).PropertyType;
			IList refObjects;
			refObjects = m_SqlEngineManager.Context.GetObjectsBySql(sql, propType, idColumns, typeColumns, hashPropertyColumnMap, parameters);
			orgValue = DBNull.Value;
			foreach (object refObject in refObjects)
			{
				om.SetPropertyValue(obj, propertyMap.Name, refObject);
				orgValue = refObject;
				break;
			}
			om.SetOriginalPropertyValue(obj, propertyMap.Name, orgValue);
			if (Convert.IsDBNull(orgValue))
			{
				om.SetNullValueStatus(obj, propertyMap.Name, true);
			}
		}

		protected virtual void LoadReferenceCollectionProperty(object obj, IPropertyMap propertyMap, Type itemType)
		{
			if (propertyMap.ReferenceType == ReferenceType.ManyToMany)
			{
				LoadManyManyCollectionProperty(obj, propertyMap, itemType);
			}
			else
			{
				LoadManyOneCollectionProperty(obj, propertyMap, itemType);
			}
		}

		protected virtual void LoadManyOneCollectionProperty(object obj, IPropertyMap propertyMap, Type itemType)
		{
			IList parameters = new ArrayList() ;
			IList propList = m_SqlEngineManager.Context.ListManager.CreateList(obj, propertyMap);
			IList refObjects;
			IList idColumns = new ArrayList();
			IList typeColumns = new ArrayList();
			Hashtable hashPropertyColumnMap = new Hashtable();
			IList list;
			IInterceptableList mList;
			bool stackMute = false;
			IContext ctx = m_SqlEngineManager.Context;
			
			IObjectManager om = ctx.ObjectManager;
			IListManager lm = ctx.ListManager;
			
			string sql = GetSelectManyOnePropertyStatement(obj, propertyMap, idColumns, typeColumns, hashPropertyColumnMap, parameters);
			refObjects = m_SqlEngineManager.Context.GetObjectsBySql(sql, itemType, idColumns, typeColumns, hashPropertyColumnMap, parameters);
			mList = propList as IInterceptableList;
			if (mList != null)
			{
				stackMute = mList.MuteNotify;
				mList.MuteNotify = true;
			}
			foreach (object refObject in refObjects)
			{
				propList.Add(refObject);
			}
			if (mList != null)
			{
				mList.MuteNotify = stackMute ;
			}
			om.SetPropertyValue(obj, propertyMap.Name, propList);
			list = lm.CloneList(obj, propertyMap, propList);
			om.SetOriginalPropertyValue(obj, propertyMap.Name, list);
		}

		protected virtual void LoadManyManyCollectionProperty(object obj, IPropertyMap propertyMap, Type itemType)
		{
			IList parameters = new ArrayList() ;
			IList propList;
			bool stackMute = false;
			IList list;
			propList = (IList) m_SqlEngineManager.Context.ObjectManager.GetPropertyValue(obj, propertyMap.Name);
			if (propList == null)
			{
				propList = m_SqlEngineManager.Context.ListManager.CreateList(obj, propertyMap);
			}
			IList refObjects;
			IList idColumns = new ArrayList();
			IList typeColumns = new ArrayList();
			Hashtable hashPropertyColumnMap = new Hashtable();
			IContext ctx = m_SqlEngineManager.Context;
			IInterceptableList mList;

			IObjectManager om = ctx.ObjectManager;
			IListManager lm = ctx.ListManager;
			
			string sql = GetSelectManyManyPropertyStatement(obj, propertyMap, idColumns, typeColumns, hashPropertyColumnMap, parameters);
			refObjects = m_SqlEngineManager.Context.GetObjectsBySql(sql, itemType, idColumns, typeColumns, hashPropertyColumnMap, parameters);
			mList = propList as IInterceptableList;
			if (mList != null)
			{
				stackMute = mList.MuteNotify;
				mList.MuteNotify = true;
			}
			foreach (object refObject in refObjects)
			{
				propList.Add(refObject);
			}
			if (mList != null)
			{
				mList.MuteNotify = stackMute;
			}
			om.SetPropertyValue(obj, propertyMap.Name, propList);
			list = lm.CloneList(obj, propertyMap, propList);
			om.SetOriginalPropertyValue(obj, propertyMap.Name, list);
		}

		protected virtual void LoadObjectByIdOrKey(ref object obj, string keyPropertyName, object keyValue)
		{
			IList parameters = new ArrayList() ;
			string propName;
			object orgValue;
			object value;
			ArrayList propertyNames = new ArrayList();
			IContext ctx = m_SqlEngineManager.Context;
			ObjectCancelEventArgs e = new ObjectCancelEventArgs(obj);
			ctx.EventManager.OnLoadingObject(this, e);
			if (e.Cancel)
			{
				return;
			}
			IObjectManager om = ctx.ObjectManager;
			IPersistenceManager pm = ctx.PersistenceManager;
			IClassMap classMap = ctx.DomainMap.MustGetClassMap(obj.GetType());
			IPropertyMap propertyMap;
			string sql = GetSelectStatement(obj, propertyNames, keyPropertyName, keyValue, parameters);
			IDataSource ds = ctx.DataSourceManager.GetDataSource(obj);
			object[,] result = (object[,]) ctx.SqlExecutor.ExecuteArray(sql, ds, parameters);
			parameters.Clear();
			if (Util.IsArray(result))
			{
				
				for (int row = 0; row <= result.GetUpperBound(1); row++)
				{
					for (int col = 0; col <= result.GetUpperBound(0); col++)
					{
						propName = (string) propertyNames[col];
						propertyMap = classMap.MustGetPropertyMap(propName);
						if (propertyMap.GetAllColumnMaps().Count < 2)
						{
							orgValue = result[col, row];
							value = pm.ManageLoadedValue(obj, propertyMap, orgValue);
							om.SetPropertyValue(obj, propName, value);
							if (propertyMap.ReferenceType == ReferenceType.None)
								om.SetOriginalPropertyValue(obj, propName, orgValue);
							else
								om.SetOriginalPropertyValue(obj, propName, value);
							om.SetNullValueStatus(obj, propName, DBNull.Value.Equals(orgValue));
						}
						else
						{
//							orgValue = result[col, row];
//							value = pm.ManageLoadedValue(obj, propertyMap, orgValue);
//							om.SetPropertyValue(obj, propName, value);
//							om.SetOriginalPropertyValue(obj, propName, orgValue);							
						}
					}
				}
                ctx.IdentityMap.RegisterLoadedObject(obj);
			}
			else
			{
				//throw new ObjectNotFoundException("Object not found!"); // do not localize
				obj = null;
			}
			ObjectEventArgs e2 = new ObjectEventArgs(obj);
			ctx.EventManager.OnLoadedObject(this, e2);
		}

		protected virtual void InsertNonPrimaryProperties(object obj, ArrayList nonPrimaryPropertyMaps, IList stillDirty)
		{
			IList parameters = new ArrayList() ;
			Hashtable hashTables = new Hashtable();
			ITableMap tableMap;
			IDataSource ds = null;
			string key;
			string sql;
			int rowsAffected;
			ArrayList propertyList;
			IContext ctx = m_SqlEngineManager.Context;
			IObjectManager om = ctx.ObjectManager;
			foreach (IPropertyMap propertyMap in nonPrimaryPropertyMaps)
			{
				tableMap = propertyMap.GetTableMap();
				key = tableMap.SourceMap.Name + "." + tableMap.Name;
				if (!(hashTables.ContainsKey(key)))
				{
					hashTables[key] = new ArrayList();
				}
				((ArrayList) (hashTables[key])).Add(propertyMap);
			}
			foreach (ArrayList iPropertyList in hashTables.Values)
			{
				propertyList = iPropertyList;
				foreach (IPropertyMap propertyMap in propertyList)
				{
					ds = ctx.DataSourceManager.GetDataSource(obj, propertyMap.Name);
					break;
				}
				parameters.Clear() ;
				sql = GetInsertNonPrimaryStatement(obj, propertyList, stillDirty, parameters);
				if (!(sql == ""))
				{
					rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, parameters);
					if (rowsAffected < 1)
					{
						throw new RowNotInsertedException("A new row was not inserted in the data source in a non-primary table for a new object."); // do not localize
					}
//					foreach (IPropertyMap propertyMap in propertyList)
//					{
//						if (om.GetNullValueStatus(obj, propertyMap.Name))
//						{
//							om.SetOriginalPropertyValue(obj, propertyMap.Name, System.DBNull.Value);						
//						}
//						else
//						{						
//							om.SetOriginalPropertyValue(obj, propertyMap.Name, om.GetPropertyValue(obj, propertyMap.Name));
//						}
//					}
				}
			}
		}

		protected virtual void UpdateNonPrimaryProperties(object obj, ArrayList nonPrimaryPropertyMaps, IList stillDirty)
		{
			IList parameters = new ArrayList() ;
			Hashtable hashTables = new Hashtable();
			ITableMap tableMap;
			IDataSource ds = null;
			string key;
			string sql;
			int rowsAffected;
			ArrayList propertyList;
			IContext ctx = m_SqlEngineManager.Context;
			IClassMap classMap = ctx.DomainMap.MustGetClassMap(obj.GetType());
			foreach (IPropertyMap propertyMap in nonPrimaryPropertyMaps)
			{
				tableMap = propertyMap.GetTableMap();
				key = tableMap.SourceMap.Name + "." + tableMap.Name;
				if (!(hashTables.ContainsKey(key)))
				{
					hashTables[key] = new ArrayList();
				}
				((ArrayList) (hashTables[key])).Add(propertyMap);
			}
			foreach (ArrayList iPropertyList in hashTables.Values)
			{
				propertyList = iPropertyList;
				parameters.Clear() ;
                foreach (IPropertyMap propertyMap in propertyList)
                {
                    ds = ctx.DataSourceManager.GetDataSource(obj, propertyMap.Name);
                    break;
                }
				sql = GetUpdateNonPrimaryStatement(obj, propertyList, stillDirty, parameters);
				if (sql != "")
				{
                    if (ds == null)
                        throw new NPersistException(string.Format("Datasource may not be null (No property found)"));// do not localize
                
					rowsAffected = ctx.SqlExecutor.ExecuteNonQuery(sql, ds, parameters);
					if (rowsAffected < 1)
					{
						if (!(SqlEngineManager.Context.PersistenceManager.GetUpdateOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, classMap) == OptimisticConcurrencyBehaviorType.Disabled))
						{
							throw new OptimisticConcurrencyException("An optimistic concurrency exception occurred when updating the row in a non-primary table for object " + obj.GetType().ToString() + this.Context.ObjectManager.GetObjectKeyOrIdentity(obj) + " in the data source. The row may have been modified or deleted by another thread or user.", obj ); // do not localize
						}
					}
//					foreach (IPropertyMap propertyMap in propertyList)
//					{
//						if (om.GetNullValueStatus(obj, propertyMap.Name))
//						{
//							om.SetOriginalPropertyValue(obj, propertyMap.Name, System.DBNull.Value);						
//						}
//						else
//						{						
//							om.SetOriginalPropertyValue(obj, propertyMap.Name, om.GetPropertyValue(obj, propertyMap.Name));
//						}
//					}
				}
			}
		}

		protected virtual string WrapValue(object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap)
		{
			string compareOp = "";
			return WrapValue(obj, propertyMap, value, columnMap, ref compareOp, false);
		}

		protected virtual string WrapValue(object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap, bool noNullStatusCheck)
		{
			string compareOp = "";
			return WrapValue(obj, propertyMap, value, columnMap, ref compareOp, noNullStatusCheck);
		}

		protected virtual string WrapValue(object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap, ref string compareOp)
		{
			return WrapValue(obj, propertyMap, value, columnMap, ref compareOp, false);
		}

		protected virtual string WrapValue(object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap, ref string compareOp, bool noNullStatusCheck)
		{
			string dateFormat;
			DbType dataType = columnMap.DataType;
			IPropertyMap idPropertyMap;
			IObjectManager om = m_SqlEngineManager.Context.ObjectManager;
			IClassMap refClassMap;
			IColumnMap forColMap;
			IClassMap realRefClassMap;
			compareOp = "=";
			if (Convert.IsDBNull(value) || value == null)
			{
				compareOp = "Is";
				return "NULL";
			}
			if (!(noNullStatusCheck))
			{
				if (om.GetNullValueStatus(obj, propertyMap.Name))
				{
					compareOp = "Is";
					return "NULL";		
				}				
			}
			if (propertyMap != null)
			{
				if (!(propertyMap.ReferenceType == ReferenceType.None))
				{
					if (value == null)
					{
						compareOp = "Is";
						return "NULL";
					}
					else
					{
						if (om.GetObjectStatus(value) == ObjectStatus.UpForCreation)
						{
							compareOp = "Is";
							return "NULL";
						}
						else
						{
							refClassMap = propertyMap.GetReferencedClassMap();
							forColMap = columnMap.GetPrimaryKeyColumnMap();
							dataType = forColMap.DataType;

							if (refClassMap.GetTypeColumnMap() != null && refClassMap.GetTypeColumnMap() == forColMap)
							{
								realRefClassMap = refClassMap.DomainMap.MustGetClassMap(value.GetType());
								value = realRefClassMap.TypeValue;
							}
							else
							{
								idPropertyMap = refClassMap.GetPropertyMapForColumnMap(forColMap);
								value = om.GetPropertyValue(value, idPropertyMap.Name);
							}
						}
					}
				}
			}
			if (dataType == DbType.AnsiString || dataType == DbType.AnsiStringFixedLength || dataType == DbType.String || dataType == DbType.StringFixedLength)
			{
				if (columnMap.Precision == 0 || columnMap.Precision >= 4000)
				{
					compareOp = "LIKE";
				}
				return "'" + Convert.ToString(value).Replace("'", "''") + "'";
			}
			else if (dataType == DbType.Date || dataType == DbType.DateTime || dataType == DbType.Time)
			{
				dateFormat = columnMap.Format;
				if (dateFormat == "")
				{
					dateFormat = "yyyy-MM-dd HH:mm:ss"; // do not localize
				}
				return DateDelimiter + Convert.ToDateTime(value).ToString(dateFormat) + DateDelimiter;
			}
			else if (dataType == DbType.Byte || dataType == DbType.Decimal || dataType == DbType.Double || dataType == DbType.Int16 || dataType == DbType.Int32 || dataType == DbType.Int64 || dataType == DbType.SByte || dataType == DbType.Single || dataType == DbType.UInt16 || dataType == DbType.UInt32 || dataType == DbType.UInt64 || dataType == DbType.VarNumeric)
			{
				return Convert.ToString(value).Replace(",", ".");
			}
			else if (dataType == DbType.Boolean)
			{
				return WrapBoolean((bool) value);
			}
			else if (dataType == DbType.Currency)
			{
				return Convert.ToString(value).Replace(",", ".");
			}
			else if (dataType == DbType.Binary)
			{
				return "";
			}
			else if (dataType == DbType.Guid)
			{
				return "'" + Convert.ToString(value).Replace("'", "''") + "'";
			}
			else if (dataType == DbType.Object)
			{
				return "";
			}
			else
			{
				return Convert.ToString(value);
			}
		}

		
		protected virtual void AddParameter(IList parameters, string paramName, object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap)
		{
			string compareOp = "";
			AddParameter(parameters, paramName, obj, propertyMap, value, columnMap, ref compareOp, false);
		}
		
		protected virtual void AddParameter(IList parameters, string paramName, object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap, bool noNullStatusCheck)
		{
			string compareOp = "";
			AddParameter(parameters, paramName, obj, propertyMap, value, columnMap, ref compareOp, noNullStatusCheck);
		}

		protected virtual void AddParameter(IList parameters, string paramName, object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap, ref string compareOp)
		{
			AddParameter(parameters, paramName, obj, propertyMap, value, columnMap, ref compareOp, false);
		}

		protected virtual void AddParameter(IList parameters, string paramName, object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap, ref string compareOp, bool noNullStatusCheck)
		{
			DbType dataType = columnMap.DataType;
			IPropertyMap idPropertyMap;
			IObjectManager om = m_SqlEngineManager.Context.ObjectManager;
			IClassMap refClassMap;
			IColumnMap forColMap;
			IClassMap realRefClassMap;
			IQueryParameter param = new QueryParameter(paramName, columnMap.DataType) ;
			parameters.Add(param);
			compareOp = "=";
			if (Convert.IsDBNull(value) || value == null)
			{
				//compareOp = "Is";
				param.Value = DBNull.Value;
				return;
			}
			if (!(noNullStatusCheck))
			{
				if (om.GetNullValueStatus(obj, propertyMap.Name))
				{
					//compareOp = "Is";
					param.Value = DBNull.Value;
					return;
				}				
			}
			if (propertyMap != null)
			{
				if (!(propertyMap.ReferenceType == ReferenceType.None))
				{
					if (value == null)
					{
						//compareOp = "Is";
						param.Value = DBNull.Value;
						return;
					}
					else
					{
						if (om.GetObjectStatus(value) == ObjectStatus.UpForCreation)
						{
							//compareOp = "Is";
							param.Value = DBNull.Value;
							return;
						}
						else
						{
							refClassMap = propertyMap.GetReferencedClassMap();
							forColMap = columnMap.GetPrimaryKeyColumnMap();
							dataType = forColMap.DataType;
							if (refClassMap.GetTypeColumnMap() != null && refClassMap.GetTypeColumnMap() == forColMap)
							{
								realRefClassMap = refClassMap.DomainMap.MustGetClassMap(value.GetType());
								value = realRefClassMap.TypeValue;
							}
							else
							{
								idPropertyMap = refClassMap.GetPropertyMapForColumnMap(forColMap);
								value = om.GetPropertyValue(value, idPropertyMap.Name);
							}
						}
					}
				}
			}
//			if (dataType == DbType.AnsiString || dataType == DbType.AnsiStringFixedLength || dataType == DbType.String || dataType == DbType.StringFixedLength)
//			{
//				if (columnMap.Precision == 0 || columnMap.Precision >= 4000)
//				{
//					compareOp = "LIKE";
//				}
//			}
			param.Value = value;
		}

		
		protected virtual SqlParameter AddSqlParameter(SqlStatement sqlStatement, IList parameters, string paramName, object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap)
		{
			return AddSqlParameter(sqlStatement, parameters, paramName, obj, propertyMap, value, columnMap, false);
		}

		protected virtual SqlParameter AddSqlParameter(SqlStatement sqlStatement, IList parameters, string paramName, object obj, IPropertyMap propertyMap, object value, IColumnMap columnMap, bool noNullStatusCheck)
		{
			DbType dataType = columnMap.DataType;
			IPropertyMap idPropertyMap;
			IObjectManager om = m_SqlEngineManager.Context.ObjectManager;
			IClassMap refClassMap;
			IColumnMap forColMap;
			IClassMap realRefClassMap;
			IQueryParameter param = new QueryParameter(paramName, columnMap.DataType) ;
			parameters.Add(param);
			SqlParameter sqlParameter = sqlStatement.AddSqlParameter(paramName, dataType);
			if (Convert.IsDBNull(value) || value == null)
			{
				param.Value = DBNull.Value;
			}
			if (!(noNullStatusCheck))
			{
				if (om.GetNullValueStatus(obj, propertyMap.Name))
				{
					param.Value = DBNull.Value;
					sqlParameter.Value = DBNull.Value;
					return sqlParameter;
				}				
			}
			if (propertyMap != null)
			{
				if (!(propertyMap.ReferenceType == ReferenceType.None))
				{
					if (value == null)
					{
						param.Value = DBNull.Value;
						sqlParameter.Value = DBNull.Value;
						return sqlParameter;
					}
					else
					{
						if (om.GetObjectStatus(value) == ObjectStatus.UpForCreation)
						{
							param.Value = DBNull.Value;
							sqlParameter.Value = DBNull.Value;
							return sqlParameter;
						}
						else
						{
							refClassMap = propertyMap.GetReferencedClassMap();
							forColMap = columnMap.GetPrimaryKeyColumnMap();
							dataType = forColMap.DataType;
							if (refClassMap.GetTypeColumnMap() != null && refClassMap.GetTypeColumnMap() == forColMap)
							{
								realRefClassMap = refClassMap.DomainMap.MustGetClassMap(value.GetType());
								value = realRefClassMap.TypeValue;
							}
							else
							{
								idPropertyMap = refClassMap.GetPropertyMapForColumnMap(forColMap);
								value = om.GetPropertyValue(value, idPropertyMap.Name);
							}
						}
					}
				}
			}
			param.Value = value;
			sqlParameter.Value = value;
			return sqlParameter;
		}


		protected virtual string WrapBoolean(bool value)
		{
			if (value)
			{
				return "1";
			}
			else
			{
				return "0";
			}
		}

		#endregion

		#region Sql Generation

		protected virtual string GetInsertStatement(object obj, ArrayList propertyNames, IList stillDirty, ArrayList nonPrimaryPropertyMaps, ArrayList collectionPropertyMaps, IList parameters)
		{
			IDomainMap domainMap;
			IClassMap classMap;
			IColumnMap columnMap;
			ITableMap tableMap;
			object refObj;
			IObjectManager om;
			bool ignore;
			IColumnMap typeColMap;
			string paramName;
			domainMap = m_SqlEngineManager.Context.DomainMap;
			classMap = domainMap.MustGetClassMap(obj.GetType());
			om = m_SqlEngineManager.Context.ObjectManager;
			tableMap = classMap.GetTableMap();

			SqlInsertStatement insert = new SqlInsertStatement(tableMap.SourceMap);
			SqlTableAlias table = insert.GetSqlTableAlias(tableMap);
			
			insert.SqlInsertClause.SqlTable = table.SqlTable;

			foreach (IPropertyMap propertyMap in classMap.GetAllPropertyMaps())
			{
                if (!propertyMap.IsSlave)
                {
                    if (propertyMap.IsCollection)
                    {
                        if (!propertyMap.IsReadOnly)
                            collectionPropertyMaps.Add(propertyMap);
                    }
                    else
                    {
                        ignore = false;
                        if (propertyMap.GetColumnMap().IsAutoIncrease && this.AutoIncreaserStrategy == AutoIncreaserStrategy.SelectNewIdentity)
                            ignore = true;

                        //HACK: roger fixed the timestamp bug
                        if (propertyMap.GetColumnMap().SpecificDataType == "TIMESTAMP")
                            ignore = true;

                        else if (!(propertyMap.ReferenceType == ReferenceType.None))
                        {
                            refObj = om.GetPropertyValue(obj, propertyMap.Name);
                            if (refObj != null)
                            {
                                if (om.GetObjectStatus(refObj) == ObjectStatus.UpForCreation)
                                {
                                    ignore = true;
                                    stillDirty.Add(propertyMap);
                                }
                            }
                        }
                        if (!(ignore))
                        {
                            if (!(propertyMap.GetTableMap() == tableMap))
                            {
                                nonPrimaryPropertyMaps.Add(propertyMap);
                            }
                            else
                            {
                                if (!(ignore))
                                {
                                    columnMap = propertyMap.GetColumnMap();
                                    SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);

                                    paramName = GetParameterName(propertyMap);
                                    SqlParameter param = AddSqlParameter(insert, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);

                                    insert.AddSqlColumnAndValue(column, param);

                                    propertyNames.Add(propertyMap.Name);

                                    foreach (IColumnMap iColumnMap in propertyMap.GetAdditionalColumnMaps())
                                    {
                                        columnMap = iColumnMap;
                                        column = table.GetSqlColumnAlias(columnMap);

                                        paramName = GetParameterName(propertyMap, columnMap);
                                        param = AddSqlParameter(insert, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);

                                        insert.AddSqlColumnAndValue(column, param);
                                    }
                                }
                            }
                        }
                    }
                }				
			}
			typeColMap = classMap.GetTypeColumnMap();
			if (typeColMap != null)
			{
				SqlColumnAlias typeColumn = table.GetSqlColumnAlias(typeColMap);								

				paramName = GetParameterName(classMap, "Type_");
				SqlParameter param = AddSqlParameter(insert, parameters, paramName, obj, null, classMap.TypeValue, typeColMap, true);

				insert.AddSqlColumnAndValue(typeColumn, param);
			}
			return GenerateSql(insert);
		}

		protected virtual string GetSelectStatement(object obj, ArrayList propertyNames, IList parameters)
		{
			return GetSelectStatement(obj, propertyNames, "", "", parameters);
		}

        //NOTE: shouldnt this also load related tables in inheritance scenarios? , eg classtable inheritance?
		protected virtual string GetSelectStatement(object obj, ArrayList propertyNames, string keyPropertyName, object keyValue, IList parameters)
		{
			IPropertyMap propertyMap;
			IClassMap classMap;
			ITableMap tableMap;
			IObjectManager om;
			IColumnMap typeColumnMap;
			string paramName = "";


			classMap = m_SqlEngineManager.Context.DomainMap.MustGetClassMap(obj.GetType());

			tableMap = classMap.GetTableMap();
			ISourceMap sourceMap = tableMap.SourceMap;

			SqlSelectStatement select = new SqlSelectStatement(sourceMap) ; 
			SqlTableAlias table = select.GetSqlTableAlias(tableMap);

			foreach (IPropertyMap iPropertyMap in classMap.GetAllPropertyMaps())
			{
				propertyMap = iPropertyMap;
				if (propertyMap.GetTableMap() == tableMap)
				{
					//if (!((propertyMap.IsCollection || (propertyMap.ReferenceType != ReferenceType.None && !(propertyMap.IsIdentity)))))
					if (!(propertyMap.IsCollection || propertyMap.GetAllColumnMaps().Count > 1))
					{
						if (!(propertyMap.LazyLoad))
						{
							IColumnMap columnMap = propertyMap.GetColumnMap();
							SqlColumnAlias column = select.GetSqlColumnAlias(columnMap);
							select.SqlSelectClause.AddSqlAliasSelectListItem(column);
							propertyNames.Add(propertyMap.Name);
						}
					}
				}
			}
			select.SqlFromClause.AddSqlAliasTableSource(table);
			om = m_SqlEngineManager.Context.ObjectManager;
			if (keyPropertyName.Length > 0)
			{
				propertyMap = classMap.MustGetPropertyMap(keyPropertyName);
				IColumnMap columnMap = propertyMap.GetColumnMap();

				SqlColumnAlias column = select.GetSqlColumnAlias(columnMap);
				SqlSearchCondition search = select.SqlWhereClause.GetSqlSearchCondition();

				paramName = GetParameterName(propertyMap, "Id_");

				SqlParameter param = AddSqlParameter(select, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);

				search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
			}
			else
			{
				foreach (IPropertyMap iPropertyMap in classMap.GetIdentityPropertyMaps())
				{
					propertyMap = iPropertyMap;
					IColumnMap columnMap = propertyMap.GetColumnMap();

					SqlColumnAlias column = select.GetSqlColumnAlias(columnMap);
					SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition();

					paramName = GetParameterName(propertyMap, "Id_");
					SqlParameter param;
					
					if (om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty)
						param = AddSqlParameter(select, parameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
					else
						param = AddSqlParameter(select, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);

					search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
				}
				typeColumnMap = classMap.GetTypeColumnMap();
				if (typeColumnMap != null)
				{
					SqlColumnAlias column = select.GetSqlColumnAlias(typeColumnMap);
					paramName = GetParameterName(classMap, "Type_");
					SqlParameter param = AddSqlParameter(select, parameters, paramName, obj, null, classMap.TypeValue, typeColumnMap, true);
					SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition();

					search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
				}
			}
			return GenerateSql(select);;
		}



		protected virtual string GetUpdateStatement(object obj, ArrayList propertyMaps, IList stillDirty, ArrayList nonPrimaryPropertyMaps, ArrayList collectionPropertyMaps, IList parameters)
		{
			IClassMap classMap;
			IPropertyMap propertyMap;
			ITableMap tableMap;
			IObjectManager om;
			IPersistenceManager pm;
			string compareOp = "";
			string wrappedValue;
			bool hadDirty = false;
			OptimisticConcurrencyBehaviorType propOptBehavior;
			PropertyStatus propStatus;
			bool ignore = false;
			object refObj;
			string paramName = "";
			IList orgParameters = new ArrayList(); 
			classMap = m_SqlEngineManager.Context.DomainMap.MustGetClassMap(obj.GetType());
			om = m_SqlEngineManager.Context.ObjectManager;
			pm = m_SqlEngineManager.Context.PersistenceManager;
			tableMap = classMap.GetTableMap();

			
			SqlUpdateStatement update = new SqlUpdateStatement(tableMap.SourceMap) ;
			SqlTableAlias table = update.GetSqlTableAlias(tableMap);
			SqlSearchCondition search;
			SqlParameter param;

			update.SqlUpdateClause.SqlTable = table.SqlTable;

			bool hadStillDirty = false;
			if (stillDirty.Count > 0)
				 hadStillDirty = true;
 
			foreach (IPropertyMap iPropertyMap in classMap.GetAllPropertyMaps())
			{
				propertyMap = iPropertyMap;
				if (!propertyMap.IsReadOnly && !propertyMap.IsSlave)
				{
					propOptBehavior = pm.GetUpdateOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, propertyMap);
					propStatus = om.GetPropertyStatus(obj, propertyMap.Name);						
					if (hadStillDirty)
					{
						if (stillDirty.Contains(propertyMap))
						{
							propStatus = PropertyStatus.Dirty ;													
						}
						else
						{
							if (propStatus == PropertyStatus.Dirty)
								propStatus = PropertyStatus.Clean ;
						}
					}
					if (propStatus != PropertyStatus.NotLoaded)
					{
						if (propStatus == PropertyStatus.Dirty || propOptBehavior == OptimisticConcurrencyBehaviorType.IncludeWhenLoaded)
						{
							if (propertyMap.IsCollection)
							{
								if (propStatus == PropertyStatus.Dirty)
								{
									collectionPropertyMaps.Add(propertyMap);
								}
							}
							else
							{
								ignore = false;
								if (!(propertyMap.ReferenceType == ReferenceType.None))
								{
									refObj = om.GetPropertyValue(obj, propertyMap.Name);
									if (refObj != null)
									{
										ObjectStatus refObjectStatus = om.GetObjectStatus(refObj);
										if (refObjectStatus == ObjectStatus.UpForCreation)
										{
											ignore = true;
											if (!(stillDirty.Contains(propertyMap)))
												stillDirty.Add(propertyMap);
										}
									}
								}
								if (!(ignore))
								{
									if (!(propertyMap.GetTableMap() == tableMap))
									{
										if (propStatus == PropertyStatus.Dirty)
										{
											nonPrimaryPropertyMaps.Add(propertyMap);
										}
									}
									else
									{
										bool first = true;
										foreach (IColumnMap columnMap in propertyMap.GetAllColumnMaps() )
										{
											SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);
											if (propStatus == PropertyStatus.Dirty)
											{
												if (first)
													paramName = GetParameterName(propertyMap);
												else
													paramName = GetParameterName(propertyMap, columnMap);

												param = AddSqlParameter(update, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);
												update.AddSqlColumnAndValue(column, param);
										
												propertyMaps.Add(propertyMap);
												hadDirty = true;

												if (hadStillDirty)
													stillDirty.Remove(propertyMap);
											}
											if (!(propOptBehavior == OptimisticConcurrencyBehaviorType.Disabled))
											{
												if (om.HasOriginalValues(obj, propertyMap.Name))
												{
													//Hack: For some reason it doesn't work to match NULL in parameterized queries...?
													search = update.SqlWhereClause.GetNextSqlSearchCondition();
												
													wrappedValue = WrapValue(obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, ref compareOp, true);
													if (wrappedValue == "NULL" && compareOp == "Is")
													{
														search.GetSqlIsNullPredicate(column);
													}
													else if (compareOp == "LIKE")
													{
														if (first)
															paramName = GetParameterName(propertyMap, "Org_");
														else
															paramName = GetParameterName(propertyMap, columnMap, "Org_");
														param = AddSqlParameter(update, orgParameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
														search.GetSqlLikePredicate(column, param);
													}
													else
													{
														if (first)
															paramName = GetParameterName(propertyMap, "Org_");
														else
															paramName = GetParameterName(propertyMap, columnMap, "Org_");
														param = AddSqlParameter(update, orgParameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
														search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
													}

													//TODO: The rest of the columns??
												}
											}	
											first = false;
										}
									}
								}
							}
						}
						
					}
				}
			}
			if (!(hadDirty))
				return "";

			foreach (IPropertyMap iPropertyMap in classMap.GetIdentityPropertyMaps())
			{
				propertyMap = iPropertyMap;
				IColumnMap columnMap = propertyMap.GetColumnMap();
				SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);

				paramName = GetParameterName(propertyMap, "Id_");
				if (om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty)
					param = AddSqlParameter(update, orgParameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), propertyMap.GetColumnMap(), true);
				else
					param = AddSqlParameter(update, orgParameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), propertyMap.GetColumnMap());

				search = update.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
			}

			foreach (IQueryParameter orgParameter in orgParameters)
				parameters.Add(orgParameter);

			return GenerateSql(update);
		}


		protected virtual string GetDeleteStatement(object obj, IList parameters)
		{
			IClassMap classMap;
			IPropertyMap propertyMap;
			ITableMap tableMap;
			IObjectManager om;
			IPersistenceManager pm;
			string compareOp = "";
			string paramName = "";
			string wrappedValue = "";
			OptimisticConcurrencyBehaviorType propOptBehavior;
			classMap = m_SqlEngineManager.Context.DomainMap.MustGetClassMap(obj.GetType());

			om = m_SqlEngineManager.Context.ObjectManager;
			pm = m_SqlEngineManager.Context.PersistenceManager;
			tableMap = classMap.GetTableMap();

			SqlDeleteStatement delete = new SqlDeleteStatement(tableMap.SourceMap) ; 
			SqlTableAlias table = delete.GetSqlTableAlias(tableMap);

			delete.SqlFromClause.AddSqlAliasTableSource(table);

			if (!(pm.GetDeleteOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, classMap) == OptimisticConcurrencyBehaviorType.Disabled))
			{
				foreach (IPropertyMap iPropertyMap in classMap.GetAllPropertyMaps())
				{
					propertyMap = iPropertyMap;
					if (!(propertyMap.IsIdentity))
					{
						if (!propertyMap.IsReadOnly && !propertyMap.IsSlave)
						{
							if (!(propertyMap.IsCollection))
							{
								if (propertyMap.GetTableMap() == tableMap)
								{
									propOptBehavior = pm.GetDeleteOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, propertyMap);
									if (propOptBehavior == OptimisticConcurrencyBehaviorType.IncludeWhenLoaded || (propOptBehavior == OptimisticConcurrencyBehaviorType.IncludeWhenDirty && om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty))
									{
										if (om.HasOriginalValues(obj, propertyMap.Name))
										{
											bool first = true;
											foreach (IColumnMap columnMap in propertyMap.GetAllColumnMaps() )
											{
												SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);
												SqlSearchCondition search = delete.SqlWhereClause.GetNextSqlSearchCondition();

												//Hack: For some reason it doesn't work to match NULL in parameterized queries...?
												wrappedValue = WrapValue(obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, ref compareOp, true);
												if (wrappedValue == "NULL" && compareOp == "Is")
												{					
													search.GetSqlIsNullPredicate(column);
												}
												else if (compareOp == "LIKE")
												{
													if (first)
														paramName = GetParameterName(propertyMap, "Org_");
													else
														paramName = GetParameterName(propertyMap, columnMap, "Org_");
													SqlParameter param = AddSqlParameter(delete, parameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
													search.GetSqlLikePredicate(column, param);
												}
												else
												{
													if (first)
														paramName = GetParameterName(propertyMap, "Org_");
													else
														paramName = GetParameterName(propertyMap, columnMap, "Org_");
													SqlParameter param = AddSqlParameter(delete, parameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, false);
													search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
												}
												first = false;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			foreach (IPropertyMap iPropertyMap in classMap.GetIdentityPropertyMaps())
			{
				propertyMap = iPropertyMap;
				SqlSearchCondition search = delete.SqlWhereClause.GetNextSqlSearchCondition();
				IColumnMap columnMap = propertyMap.GetColumnMap();
				SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);
				SqlParameter param;

				paramName = GetParameterName(propertyMap, "Id_");
				if (om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty)
				{
					param = AddSqlParameter(delete, parameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), propertyMap.GetColumnMap(), true);
				}
				else
				{
					param = AddSqlParameter(delete, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), propertyMap.GetColumnMap());
				}
				search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
			}
			return GenerateSql(delete);
		}



		protected virtual string GetSelectPropertyStatement(object obj, string propertyName, IList parameters)
		{
			IClassMap classMap;
			IPropertyMap propertyMap;
			IColumnMap columnMap;
			IColumnMap idColumnMap;
			IPropertyMap idPropertyMap;
			ITableMap tableMap;
			IObjectManager om;
			string paramName = "";
			classMap = m_SqlEngineManager.Context.DomainMap.MustGetClassMap(obj.GetType());

			propertyMap = classMap.MustGetPropertyMap(propertyName);
			tableMap = propertyMap.GetTableMap();

			SqlSelectStatement select = new SqlSelectStatement(tableMap.SourceMap) ; 
			SqlTableAlias table = select.GetSqlTableAlias(tableMap);

			SqlColumnAlias column = table.GetSqlColumnAlias(propertyMap.GetColumnMap());

			select.SqlSelectClause.AddSqlAliasSelectListItem(column);
			select.SqlFromClause.AddSqlAliasTableSource(table);

			om = m_SqlEngineManager.Context.ObjectManager;
			if (!(propertyMap.GetTableMap() == classMap.GetTableMap()))
			{
				idColumnMap = propertyMap.GetIdColumnMap();
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				SqlColumnAlias idColumn = table.GetSqlColumnAlias(idColumnMap);

				paramName = GetParameterName(idPropertyMap, "Id_");
				SqlParameter param = AddSqlParameter(select, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap);

				SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);

				foreach (IColumnMap iIdColumnMap in propertyMap.GetAdditionalIdColumnMaps())
				{
					idColumnMap = iIdColumnMap;
					idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());

					idColumn = table.GetSqlColumnAlias(idColumnMap);

					paramName = GetParameterName(propertyMap, idColumnMap, "Id_");
					if (!(classMap.GetTypeColumnMap() == null && classMap.GetTypeColumnMap() == idColumnMap.GetPrimaryKeyColumnMap()))
						param = AddSqlParameter(select, parameters, paramName, obj, null, classMap.TypeValue, idColumnMap, true);
					else
						param = AddSqlParameter(select, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);

					search = select.SqlWhereClause.GetNextSqlSearchCondition();
					search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);
				}
			}
			else
			{
				foreach (IPropertyMap iPropertyMap in classMap.GetIdentityPropertyMaps())
				{
					propertyMap = iPropertyMap;
					columnMap = propertyMap.GetColumnMap();

					SqlColumnAlias idColumn = table.GetSqlColumnAlias(columnMap);
					SqlParameter param ;

					paramName = GetParameterName(propertyMap, "Id_");
					if (om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty)
						param = AddSqlParameter(select, parameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
					else
						param = AddSqlParameter(select, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);

					SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition();
					search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);
				}
			}
			return GenerateSql(select);
		}

		protected virtual string GetSelectNonPrimaryPropertyStatement(object obj, IPropertyMap propertyMap, ArrayList propertyNames, IList parameters)
		{
			IClassMap classMap;
			IColumnMap columnMap;
			IColumnMap idColumnMap;
			IPropertyMap idPropertyMap;
			IPropertyMap addPropertyMap;
			ITableMap tableMap;
			string paramName = "";
			IContext ctx = m_SqlEngineManager.Context;
			IObjectManager om = ctx.ObjectManager;
			classMap = m_SqlEngineManager.Context.DomainMap.MustGetClassMap(obj.GetType());

			tableMap = propertyMap.GetTableMap();

			SqlSelectStatement select = new SqlSelectStatement(tableMap.SourceMap) ; 
			SqlTableAlias table = select.GetSqlTableAlias(tableMap);

			columnMap = propertyMap.GetColumnMap();
			SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);

			select.SqlSelectClause.AddSqlAliasSelectListItem(column);
			propertyNames.Add(propertyMap.Name);

			foreach (IPropertyMap iAddPropertyMap in classMap.GetAllPropertyMaps())
			{
				addPropertyMap = iAddPropertyMap;
				if (addPropertyMap.GetTableMap() == tableMap)
				{
					if (!(addPropertyMap == propertyMap))
					{
						if (om.GetPropertyStatus(obj, addPropertyMap.Name) == PropertyStatus.NotLoaded)
						{
							if (!((addPropertyMap.IsCollection || (addPropertyMap.ReferenceType != ReferenceType.None && !(addPropertyMap.IsIdentity)))))
							{
								if (!(addPropertyMap.LazyLoad))
								{
									columnMap = addPropertyMap.GetColumnMap();
									column = table.GetSqlColumnAlias(columnMap);

									select.SqlSelectClause.AddSqlAliasSelectListItem(column);
									propertyNames.Add(addPropertyMap.Name);
								}
							}
						}
					}
				}
			}

			select.SqlFromClause.AddSqlAliasTableSource(table);

			if (!(propertyMap.GetTableMap() == classMap.GetTableMap()))
			{
				idColumnMap = propertyMap.GetIdColumnMap();
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());

				SqlColumnAlias idColumn = table.GetSqlColumnAlias(idColumnMap);

				paramName = GetParameterName(propertyMap, "Id_");
				SqlParameter param = AddSqlParameter(select, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);

				SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);

				foreach (IColumnMap iIdColumnMap in propertyMap.GetAdditionalIdColumnMaps())
				{
					idColumnMap = iIdColumnMap;
					idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());

					idColumn = table.GetSqlColumnAlias(idColumnMap);

					paramName = GetParameterName(propertyMap, idColumnMap, "Id_");
					if (!(classMap.GetTypeColumnMap() == null && classMap.GetTypeColumnMap() == idColumnMap.GetPrimaryKeyColumnMap()))
						param = AddSqlParameter(select, parameters, paramName, obj, null, classMap.TypeValue, idColumnMap, true);
					else
						param = AddSqlParameter(select, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);

					search = select.SqlWhereClause.GetNextSqlSearchCondition();
					search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);
				}
			}
			else
			{
				foreach (IPropertyMap iPropertyMap in classMap.GetIdentityPropertyMaps())
				{
					propertyMap = iPropertyMap;
					columnMap = propertyMap.GetColumnMap();

					SqlColumnAlias idColumn = table.GetSqlColumnAlias(columnMap);

					paramName = GetParameterName(propertyMap, "Id_");
					SqlParameter param;

					if (om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty)
						param = AddSqlParameter(select, parameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
					else
						param = AddSqlParameter(select, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);

					SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition();
					search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);
				}
			}
			return GenerateSql(select);
		}

		protected virtual string GetSelectCollectionPropertyStatement(object obj, string propertyName, IList parameters)
		{
			IClassMap classMap;
			IPropertyMap propertyMap;
			IColumnMap idColumnMap;
			IPropertyMap idPropertyMap;
			ITableMap tableMap;
			IObjectManager om;
			string paramName = "";

			classMap = m_SqlEngineManager.Context.DomainMap.MustGetClassMap(obj.GetType());

			propertyMap = classMap.MustGetPropertyMap(propertyName);
			tableMap = propertyMap.GetTableMap();

			ISourceMap sourceMap = tableMap.SourceMap;
			SqlSelectStatement select = new SqlSelectStatement(sourceMap) ; 
			SqlTableAlias table = select.GetSqlTableAlias(tableMap);

			IColumnMap columnMap = propertyMap.GetColumnMap();
			SqlColumnAlias column = select.GetSqlColumnAlias(columnMap); 

			select.SqlSelectClause.AddSqlAliasSelectListItem(column);
			select.SqlFromClause.AddSqlAliasTableSource(table);

			om = m_SqlEngineManager.Context.ObjectManager;
			idColumnMap = propertyMap.GetIdColumnMap();
			idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
			SqlColumnAlias idColumn = select.GetSqlColumnAlias(idColumnMap); 

			paramName = GetParameterName(propertyMap, "Id_");
			
			SqlParameter param = AddSqlParameter(select, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);
			SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition();
			search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);

			foreach (IColumnMap iIdColumnMap in propertyMap.GetAdditionalIdColumnMaps())
			{
				idColumnMap = iIdColumnMap;
				idColumn = select.GetSqlColumnAlias(idColumnMap); 
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				paramName = GetParameterName(propertyMap, idColumnMap, "Id_");
				if (!(classMap.GetTypeColumnMap() == null && classMap.GetTypeColumnMap() == idColumnMap.GetPrimaryKeyColumnMap()))
				{
					param = AddSqlParameter(select, parameters, paramName, obj, null, classMap.TypeValue, idColumnMap, true);
				}
				else
				{
					param = AddSqlParameter(select, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);
				}
				search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);
			}
			return GenerateSql(select);
		}


		protected virtual void GetInsertCollectionPropertyStatements(object obj, IPropertyMap propertyMap, ArrayList sqlAndParamsList, IList stillDirty)
		{
			IObjectManager om = m_SqlEngineManager.Context.ObjectManager;
			IListManager lm = m_SqlEngineManager.Context.ListManager;
			IList newList = (IList) om.GetPropertyValue(obj, propertyMap.Name);
			string sql = "";
			IList parameters;
			SqlStatementAndDbParameters sqlAndParams;
		
			if (!(propertyMap.ReferenceType == ReferenceType.None))
			{
				foreach (object value in newList)
				{
					if (om.GetObjectStatus(value) == ObjectStatus.UpForCreation)
					{
						stillDirty.Add(propertyMap);
						return;
					}
				}
			}
			foreach (object value in newList)
			{
				parameters = new ArrayList() ;
				sqlAndParams = new SqlStatementAndDbParameters() ;
				sql = (GetInsertCollectionValueStatement(obj, propertyMap, value, parameters));
				sqlAndParams.SqlStatement = sql;
				sqlAndParams.DbParameters = parameters;
				sqlAndParamsList.Add(sqlAndParams);
			}
		}

		protected virtual void GetUpdateCollectionPropertyStatements(object obj, IPropertyMap propertyMap, ArrayList sqlAndParamsList, IList stillDirty)
		{
			IObjectManager om = m_SqlEngineManager.Context.ObjectManager;
			IList newList = (IList) om.GetPropertyValue(obj, propertyMap.Name);
			IList oldList = (IList) om.GetOriginalPropertyValue(obj, propertyMap.Name);
			SqlStatementAndDbParameters sqlAndParams;
			string sql;
			IList parameters;

			if (!(propertyMap.ReferenceType == ReferenceType.None))
			{
				foreach (object value in newList)
				{
					if (om.GetObjectStatus(value) == ObjectStatus.UpForCreation)
					{
						if (!(stillDirty.Contains(propertyMap)))
							stillDirty.Add(propertyMap);
						return;
					}
				}
			}
			if (oldList != null)
			{
				foreach (object value in oldList)
				{
					if (!(newList.Contains(value)))
					{
						sqlAndParams = new SqlStatementAndDbParameters() ;
						parameters = new ArrayList() ;
						sql = GetRemoveCollectionValueStatement(obj, propertyMap, value, parameters);
						sqlAndParams.SqlStatement = sql;
						sqlAndParams.DbParameters = parameters;
						sqlAndParamsList.Add(sqlAndParams);
					}
				}
			}
			if (newList != null)
			{
				foreach (object value in newList)
				{
					if (!(oldList.Contains(value)))
					{
						sqlAndParams = new SqlStatementAndDbParameters() ;
						parameters = new ArrayList() ;
						sql = GetInsertCollectionValueStatement(obj, propertyMap, value, parameters);
						sqlAndParams.SqlStatement = sql;
						sqlAndParams.DbParameters = parameters;
						sqlAndParamsList.Add(sqlAndParams);
					}
				}
			}
			if (stillDirty.Contains(propertyMap))
				stillDirty.Remove(propertyMap);

		}

		protected virtual string GetRemoveCollectionValueStatement(object obj, IPropertyMap propertyMap, object value, IList parameters)
		{
			IClassMap classMap;
			IColumnMap columnMap;
			IColumnMap idColumnMap;
			IPropertyMap idPropertyMap;
			ITableMap tableMap;
			string paramName = "";
			IObjectManager om = m_SqlEngineManager.Context.ObjectManager;
			classMap = propertyMap.ClassMap;
			tableMap = propertyMap.GetTableMap();

			
			SqlDeleteStatement delete = new SqlDeleteStatement(tableMap.SourceMap) ; 
			SqlTableAlias table = delete.GetSqlTableAlias(tableMap);

			delete.SqlFromClause.AddSqlAliasTableSource(table);

			idColumnMap = propertyMap.GetIdColumnMap();
			idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
			SqlColumnAlias idColumn = table.GetSqlColumnAlias(idColumnMap);

			paramName = GetParameterName(idPropertyMap);
			SqlParameter param = AddSqlParameter(delete, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);

			SqlSearchCondition search = delete.SqlWhereClause.GetNextSqlSearchCondition();
			search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param );

			foreach (IColumnMap iIdColumnMap in propertyMap.GetAdditionalIdColumnMaps())
			{
				idColumnMap = iIdColumnMap;
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				idColumn = table.GetSqlColumnAlias(idColumnMap);

				paramName = GetParameterName(idPropertyMap, idColumnMap);
				if (!(classMap.GetTypeColumnMap() == null && classMap.GetTypeColumnMap() == idColumnMap.GetPrimaryKeyColumnMap()))
				{
					param = AddSqlParameter(delete, parameters, paramName, obj, null, classMap.TypeValue, idColumnMap, true);
				}
				else
				{
					param = AddSqlParameter(delete, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);
				}

				search = delete.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param );
				
			}
			columnMap = propertyMap.GetColumnMap();
			SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);

			paramName = GetParameterName(propertyMap, "Org_");
			param = AddSqlParameter(delete, parameters, paramName, obj, propertyMap, value, columnMap, true);

			search = delete.SqlWhereClause.GetNextSqlSearchCondition();
			search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param );

			return GenerateSql(delete);
		}

		protected virtual string GetInsertCollectionValueStatement(object obj, IPropertyMap propertyMap, object value, IList parameters)
		{
			IClassMap classMap;
			IColumnMap columnMap;
			IColumnMap idColumnMap;
			IPropertyMap idPropertyMap;
			ITableMap tableMap;
			IObjectManager om = m_SqlEngineManager.Context.ObjectManager;
			string paramName;
			classMap = propertyMap.ClassMap;
			tableMap = propertyMap.GetTableMap();

			SqlInsertStatement insert = new SqlInsertStatement(tableMap.SourceMap);
			SqlTableAlias table = insert.GetSqlTableAlias(tableMap);

			insert.SqlInsertClause.SqlTable = table.SqlTable;

			idColumnMap = propertyMap.GetIdColumnMap();
			idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
			SqlColumnAlias idColumn = table.GetSqlColumnAlias(idColumnMap);

			paramName = GetParameterName(idPropertyMap);
			SqlParameter param = AddSqlParameter(insert, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap);
			insert.AddSqlColumnAndValue(idColumn, param);
			foreach (IColumnMap iIdColumnMap in propertyMap.GetAdditionalIdColumnMaps())
			{
				idColumnMap = iIdColumnMap;
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				idColumn = table.GetSqlColumnAlias(idColumnMap);

				paramName = GetParameterName(propertyMap, idColumnMap);

				if (!(classMap.GetTypeColumnMap() == null && classMap.GetTypeColumnMap() == idColumnMap.GetPrimaryKeyColumnMap()))
					param = AddSqlParameter(insert, parameters, paramName, obj, null, classMap.TypeValue, idColumnMap, true);
				else
					param = AddSqlParameter(insert, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap);

				insert.AddSqlColumnAndValue(idColumn, param);

			}
			columnMap = propertyMap.GetColumnMap();
			SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);
			paramName = GetParameterName(propertyMap, columnMap);
			param = AddSqlParameter(insert, parameters, paramName, obj, propertyMap, value, columnMap, true);
			insert.AddSqlColumnAndValue(column, param);

			foreach (IColumnMap iColumnMap in propertyMap.GetAdditionalColumnMaps())
			{
				columnMap = iColumnMap;
				column = table.GetSqlColumnAlias(columnMap);
				paramName = GetParameterName(propertyMap, columnMap);
				param = AddSqlParameter(insert, parameters, paramName, obj, propertyMap, value, columnMap, true);
				insert.AddSqlColumnAndValue(column, param);
			}
			return GenerateSql(insert);
		}

		protected virtual string GetInsertNonPrimaryStatement(object obj, ArrayList propertyMaps, IList stillDirty, IList parameters)
		{
			IClassMap classMap = null;
			IPropertyMap firstPropertyMap = null;
			IPropertyMap propertyMap;
			IColumnMap columnMap;
			ITableMap tableMap = null;
			IObjectManager om;
			IColumnMap idColumnMap = null;
			IPropertyMap idPropertyMap = null;
			bool ignore;
			object refObj;
			string paramName = "";
			bool hadDirty = false;
			om = m_SqlEngineManager.Context.ObjectManager;
			Hashtable addedPropertyMaps = new Hashtable() ;
			foreach (IPropertyMap iPropertyMap in propertyMaps)
			{
				propertyMap = iPropertyMap;
				firstPropertyMap = propertyMap;
				classMap = propertyMap.ClassMap;
				tableMap = propertyMap.GetTableMap();
				idColumnMap = propertyMap.GetIdColumnMap();
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				break;
			}
			SqlInsertStatement insert = new SqlInsertStatement(tableMap.SourceMap);
			SqlTableAlias table = insert.GetSqlTableAlias(tableMap);
			SqlColumnAlias idColumn = table.GetSqlColumnAlias(idColumnMap);

			insert.SqlInsertClause.SqlTable = table.SqlTable;

			paramName = GetParameterName(idPropertyMap);
			SqlParameter param = AddSqlParameter(insert, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);
			insert.AddSqlColumnAndValue(idColumn, param);

			foreach (IColumnMap iIdColumnMap in firstPropertyMap.GetAdditionalIdColumnMaps())
			{
				idColumnMap = iIdColumnMap;
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				idColumn = table.GetSqlColumnAlias(idColumnMap);

				paramName = GetParameterName(firstPropertyMap, idColumnMap);					
				
				if (!(classMap.GetTypeColumnMap() == null && classMap.GetTypeColumnMap() == idColumnMap.GetPrimaryKeyColumnMap()))
					param = AddSqlParameter(insert, parameters, paramName, obj, null, classMap.TypeValue, idColumnMap, true);
				else
					param = AddSqlParameter(insert, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);

				insert.AddSqlColumnAndValue(idColumn, param);
			}
			foreach (IPropertyMap iPropertyMap in propertyMaps)
			{
				propertyMap = iPropertyMap;
				ignore = false;
				if (propertyMap.GetColumnMap().IsAutoIncrease)
					ignore = true;
				else if (!(propertyMap.ReferenceType == ReferenceType.None))
				{
					refObj = om.GetPropertyValue(obj, propertyMap.Name);
					if (refObj != null)
					{
						if (om.GetObjectStatus(refObj) == ObjectStatus.UpForCreation)
						{
							ignore = true;
							stillDirty.Add(propertyMap);
						}
					}
				}
				if (!(ignore))
				{
					columnMap = propertyMap.GetColumnMap();
					SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);

					paramName = GetParameterName(propertyMap);
					param = AddSqlParameter(insert, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);
					insert.AddSqlColumnAndValue(column, param);
					
					foreach (IColumnMap iColumnMap in propertyMap.GetAdditionalColumnMaps())
					{
						columnMap = iColumnMap;
						column = table.GetSqlColumnAlias(columnMap);

						paramName = GetParameterName(propertyMap, columnMap);
						param = AddSqlParameter(insert, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);
						insert.AddSqlColumnAndValue(column, param);
					}
					addedPropertyMaps[propertyMap] = propertyMap;
					hadDirty = true;
				}
			}

			IList remove = new ArrayList(); 
			foreach (IPropertyMap removePropertyMap in propertyMaps)
			{
				if (addedPropertyMaps.ContainsKey(removePropertyMap))
				{
					remove.Add(removePropertyMap);
				}
			}
			foreach (IPropertyMap removePropertyMap in remove)
			{
				propertyMaps.Remove(removePropertyMap);
			}
			if (!hadDirty)
				return "";
			
			return GenerateSql(insert);
		}

		protected virtual string GetUpdateNonPrimaryStatement(object obj, ArrayList propertyMaps, IList stillDirty, IList parameters)
		{
			IClassMap classMap = null;
			IPropertyMap firstPropertyMap = null;
			IPropertyMap propertyMap;
			ITableMap tableMap = null;
			string compareOp = "";
			string wrappedValue;
			IObjectManager om;
			IPersistenceManager pm;
			IColumnMap idColumnMap = null;
			IPropertyMap idPropertyMap = null;
			OptimisticConcurrencyBehaviorType propOptBehavior;
			PropertyStatus propStatus;
			bool hadDirty = false;
			bool ignore;
			object refObj;
			string paramName = "";
			IList orgParameters = new ArrayList() ;
			Hashtable addedPropertyMaps = new Hashtable() ;
			om = m_SqlEngineManager.Context.ObjectManager;
			pm = m_SqlEngineManager.Context.PersistenceManager;
			foreach (IPropertyMap iPropertyMap in propertyMaps)
			{
				propertyMap = iPropertyMap;
				firstPropertyMap = propertyMap;
				classMap = propertyMap.ClassMap;
				tableMap = propertyMap.GetTableMap();
				idColumnMap = propertyMap.GetIdColumnMap();
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				break;
			}

			SqlUpdateStatement update = new SqlUpdateStatement(tableMap.SourceMap) ;
			SqlTableAlias table = update.GetSqlTableAlias(tableMap);
			SqlColumnAlias idColumn = table.GetSqlColumnAlias(idColumnMap);
			SqlSearchCondition search;
			SqlParameter param;

			update.SqlUpdateClause.SqlTable = table.SqlTable;

			bool hadStillDirty = false;
			if (stillDirty.Count > 0)
				hadStillDirty = true;

			foreach (IPropertyMap iPropertyMap in propertyMaps)
			{
				propertyMap = iPropertyMap;
				propOptBehavior = pm.GetUpdateOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, propertyMap);
				propStatus = om.GetPropertyStatus(obj, propertyMap.Name);
				if (hadStillDirty)
				{
					if (stillDirty.Contains(propertyMap))
					{
						propStatus = PropertyStatus.Dirty ;													
					}
					else
					{
						if (propStatus == PropertyStatus.Dirty)
							propStatus = PropertyStatus.Clean ;
					}
				}
				bool first = true;
				foreach (IColumnMap columnMap in propertyMap.GetAllColumnMaps())
				{
					SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);
					if (propStatus == PropertyStatus.Dirty)
					{
						ignore = false;
						if (!(propertyMap.ReferenceType == ReferenceType.None))
						{
							refObj = om.GetPropertyValue(obj, propertyMap.Name);
							if (refObj != null)
							{
								ObjectStatus refObjectStatus = om.GetObjectStatus(refObj);
								if (refObjectStatus == ObjectStatus.UpForCreation)
								{
									ignore = true;
									if (!(stillDirty.Contains(propertyMap)))
										stillDirty.Add(propertyMap) ;
								}
							}
						}
						if (!(ignore))
						{
							paramName = GetParameterName(propertyMap);
							param = AddSqlParameter(update, parameters, paramName, obj, propertyMap, om.GetPropertyValue(obj, propertyMap.Name), columnMap);

							update.AddSqlColumnAndValue(column, param);
							hadDirty = true;

							addedPropertyMaps[propertyMap] = propertyMap;

							if (hadStillDirty)
								stillDirty.Remove(propertyMap);
						}
					}
					if (!(propOptBehavior == OptimisticConcurrencyBehaviorType.Disabled))
					{
						if (om.HasOriginalValues(obj, propertyMap.Name))
						{
							search = update.SqlWhereClause.GetNextSqlSearchCondition();
							//Hack: For some reason it doesn't work to match NULL in parameterized queries...?
							wrappedValue = WrapValue(obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, ref compareOp, true);
							if (wrappedValue == "NULL" && compareOp == "Is")
								search.GetSqlIsNullPredicate(column);
							else if (compareOp == "LIKE")
							{
								if (first)
									paramName = GetParameterName(propertyMap, "Org_");
								else
									paramName = GetParameterName(propertyMap, columnMap, "Org_");
								param = AddSqlParameter(update, parameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
								search.GetSqlLikePredicate(column, param);
							}
							else
							{
								paramName = GetParameterName(propertyMap, "Org_");
								param = AddSqlParameter(update, orgParameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
								search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
							}
						}
					}
					first = false;					
				}
			}
			if (!(hadDirty))
				return "";

			IList remove = new ArrayList(); 
			foreach (IPropertyMap removePropertyMap in propertyMaps)
			{
				if (addedPropertyMaps.ContainsKey(removePropertyMap))
				{
					remove.Add(removePropertyMap);
				}
			}
			foreach (IPropertyMap removePropertyMap in remove)
			{
				propertyMaps.Remove(removePropertyMap);
			}

			paramName = GetParameterName(idPropertyMap, "Id_");
			param = AddSqlParameter(update, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);

			search = update.SqlWhereClause.GetNextSqlSearchCondition();
			search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);
			
			foreach (IColumnMap iIdColumnMap in firstPropertyMap.GetAdditionalIdColumnMaps())
			{
				idColumnMap = iIdColumnMap;
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				idColumn = table.GetSqlColumnAlias(idColumnMap);

				paramName = GetParameterName(firstPropertyMap, idColumnMap, "Id_");
				if (!(classMap.GetTypeColumnMap() == null && classMap.GetTypeColumnMap() == idColumnMap.GetPrimaryKeyColumnMap()))
					param = AddSqlParameter(update, orgParameters, paramName, obj, null, classMap.TypeValue, idColumnMap, true);
				else
					param = AddSqlParameter(update, orgParameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap, true);

				search = update.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals, param);
			}

			
			foreach (IQueryParameter orgParameter in orgParameters)
				parameters.Add(orgParameter);

			return GenerateSql(update);
		}

		//Mats : Fixed non-primary ref prop bug by improving One-One backref identification
		protected virtual string GetSelectSingleReferencePropertyStatement(object obj, IPropertyMap propertyMap, IList idColumns, IList typeColumns, Hashtable hashPropertyColumnMap, IList parameters)
		{
			IColumnMap columnMap;
			IClassMap classMap;
			IPropertyMap refPropertyMap;
			ITableMap tableMap;
			IPropertyMap myPropertyMap;
			string colName;
			IColumnMap theColumnMap;
			IColumnMap forColumnMap;
			ITableMap forTableMap;
			IColumnMap addColumnMap;
			IColumnMap addForColumnMap;
			ITableMap addForTableMap;
			IColumnMap typeColumnMap;
			IDomainMap domainMap;
			bool isBackRef = false;
			string paramName = "";
			classMap = propertyMap.GetReferencedClassMap();
			if (classMap == null)
			{
				throw new MappingException("ClassMap '" + propertyMap.DataType + "' Not Found!"); // do not localize
			}
			if (propertyMap.ReferenceType == ReferenceType.OneToOne)
			{
				columnMap = propertyMap.GetColumnMap();
				if (columnMap != null)
					if (columnMap.TableMap == propertyMap.GetReferencedClassMap().GetTableMap())
						if (columnMap.IsPrimaryKey)
							isBackRef = true;
				columnMap = null;
			}

			
			SqlSelectStatement select; 
			SqlTableAlias table;
			SqlColumnAlias theColumn;
			SqlTableAlias forTable;
			SqlColumnAlias forColumn;

			if (isBackRef)
			{
				tableMap = propertyMap.ClassMap.GetTableMap();

				select = new SqlSelectStatement(tableMap.SourceMap) ; 
				table = select.GetSqlTableAlias(tableMap);


				//select.SqlSelectClause.AddSqlAliasSelectListItem(column);
				//select.SqlFromClause.AddSqlAliasTableSource(table);

				theColumnMap = propertyMap.GetIdColumnMap();
				//theColumn = table.GetSqlColumnAlias(theColumnMap);

				forTableMap = theColumnMap.TableMap;

				forColumnMap = theColumnMap;
				theColumnMap = forColumnMap.GetPrimaryKeyColumnMap();
				if (forTableMap == null)
					throw new MappingException("TableMap '" + theColumnMap.PrimaryKeyTable + "' Not Found!"); // do not localize
				if (forColumnMap == null)
					throw new MappingException("ColumnMap '" + theColumnMap.PrimaryKeyColumn + "' Not Found!"); // do not localize

				forTable = select.GetSqlTableAlias(forTableMap);
				forColumn = forTable.GetSqlColumnAlias(forColumnMap);
			}
			else
			{
				tableMap = propertyMap.GetTableMap();
				
				select = new SqlSelectStatement(tableMap.SourceMap) ; 
				table = select.GetSqlTableAlias(tableMap);

				theColumnMap = propertyMap.GetColumnMap();
				//theColumn = table.GetSqlColumnAlias(theColumnMap);

				forTableMap = theColumnMap.GetPrimaryKeyTableMap();
				forColumnMap = theColumnMap.GetPrimaryKeyColumnMap();
				if (forTableMap == null)
					throw new MappingException("TableMap '" + theColumnMap.PrimaryKeyTable + "' Not Found!"); // do not localize
				if (forColumnMap == null)
					throw new MappingException("ColumnMap '" + theColumnMap.PrimaryKeyColumn + "' Not Found!"); // do not localize

				forTable = select.GetSqlTableAlias(forTableMap);
				forColumn = forTable.GetSqlColumnAlias(forColumnMap);
			}

			IObjectManager om;
			foreach (IPropertyMap iRefPropertyMap in classMap.GetPrimaryPropertyMaps())
			{
				refPropertyMap = iRefPropertyMap;
				if (!(propertyMap.LazyLoad))
				{
					if (!((refPropertyMap.IsCollection || (refPropertyMap.ReferenceType != ReferenceType.None && !(refPropertyMap.IsIdentity)))))
					{
						IColumnMap refColumnMap = refPropertyMap.GetColumnMap();
						SqlColumnAlias refColumn = forTable.GetSqlColumnAlias(refColumnMap);
						colName = refColumnMap.Name;
						if (refPropertyMap.IsIdentity)
							idColumns.Add(colName);

						if (!(refPropertyMap.LazyLoad))
						{
							select.SqlSelectClause.AddSqlAliasSelectListItem(refColumn);
							hashPropertyColumnMap[refPropertyMap.Name] = colName;
						}
					}
				}
			}
			typeColumnMap = classMap.GetTypeColumnMap();
			if (typeColumnMap != null)
			{
				SqlColumnAlias typeColumn = forTable.GetSqlColumnAlias(typeColumnMap);
				typeColumns.Add(typeColumnMap.Name);
				select.SqlSelectClause.AddSqlAliasSelectListItem(typeColumn);
			}
			select.SqlFromClause.AddSqlAliasTableSource(table);
			SqlTableAlias selfTable ;

			//if (tableMap.SourceMap.Schema.ToLower(CultureInfo.InvariantCulture) == forTableMap.SourceMap.Schema.ToLower(CultureInfo.InvariantCulture) && tableMap.Name.ToLower(CultureInfo.InvariantCulture) == forTableMap.Name.ToLower(CultureInfo.InvariantCulture))
			if (tableMap == forTableMap)
			{
				selfTable = select.GetSqlTableAlias(tableMap, "NPersistSelfRefTable");
				select.SqlFromClause.AddSqlAliasTableSource(selfTable);				
			}
			else
			{
				selfTable = select.GetSqlTableAlias(tableMap);				
				select.SqlFromClause.AddSqlAliasTableSource(forTable);				
			}


			theColumn = selfTable.GetSqlColumnAlias(theColumnMap);

			SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition()  ;
			search.GetSqlComparePredicate(theColumn, SqlCompareOperatorType.Equals, forColumn);

			foreach (IColumnMap iAddColumnMap in propertyMap.GetAdditionalColumnMaps())
			{
				addColumnMap = iAddColumnMap;
				addForTableMap = addColumnMap.GetPrimaryKeyTableMap();
				addForColumnMap = addColumnMap.GetPrimaryKeyColumnMap();
				if (addForTableMap == null)
					throw new MappingException("TableMap '" + addColumnMap.PrimaryKeyTable + "' Not Found!"); // do not localize
				if (addForColumnMap == null)
					throw new MappingException("ColumnMap '" + addColumnMap.PrimaryKeyColumn + "' Not Found!"); // do not localize

				SqlColumnAlias addColumn = selfTable.GetSqlColumnAlias(addColumnMap);
				SqlColumnAlias addForColumn = forTable.GetSqlColumnAlias(addForColumnMap);

				search = select.SqlWhereClause.GetNextSqlSearchCondition()  ;
				search.GetSqlComparePredicate(addColumn, SqlCompareOperatorType.Equals, addForColumn);
			}

			om = m_SqlEngineManager.Context.ObjectManager;
			foreach (IPropertyMap iMyPropertyMap in propertyMap.ClassMap.GetIdentityPropertyMaps())
			{
				myPropertyMap = iMyPropertyMap;
				columnMap = myPropertyMap.GetColumnMap();

				SqlColumnAlias column = selfTable.GetSqlColumnAlias(columnMap);
				SqlParameter param;

				paramName = GetParameterName(myPropertyMap, "Id_");
				if (om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty)
					param = AddSqlParameter(select, parameters, paramName, obj, myPropertyMap, om.GetOriginalPropertyValue(obj, myPropertyMap.Name), columnMap, true);
				else
					param = AddSqlParameter(select, parameters, paramName, obj, myPropertyMap, om.GetPropertyValue(obj, myPropertyMap.Name), columnMap);

				search = select.SqlWhereClause.GetNextSqlSearchCondition()  ;
				search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
			}
			typeColumnMap = propertyMap.ClassMap.GetTypeColumnMap();
			if (typeColumnMap != null)
			{
				domainMap = propertyMap.ClassMap.DomainMap;
				SqlColumnAlias typeColumn = selfTable.GetSqlColumnAlias(typeColumnMap);
				
				paramName = GetParameterName(propertyMap.ClassMap, "Type_");
				SqlParameter param = AddSqlParameter(select, parameters, paramName, obj, null, domainMap.MustGetClassMap(obj.GetType()).TypeValue, typeColumnMap, true);

				search = select.SqlWhereClause.GetNextSqlSearchCondition()  ;
				search.GetSqlComparePredicate(typeColumn, SqlCompareOperatorType.Equals, param);
			}
			return GenerateSql(select);
		}

		protected virtual string GetSelectManyOnePropertyStatement(object obj, IPropertyMap propertyMap, IList idColumns, IList typeColumns, Hashtable hashPropertyColumnMap, IList parameters)
		{
			string colName = "";
			IColumnMap columnMap;
			IClassMap classMap;
			IPropertyMap refPropertyMap;
			ITableMap tableMap;
			ITableMap rootTableMap;
			IColumnMap idColumnMap;
			IPropertyMap myPropertyMap;
			IColumnMap myColumnMap;
			ITableMap myTableMap;
			IColumnMap addIdColumnMap;
			IColumnMap addMyColumnMap;
			ITableMap addMyTableMap;
			IColumnMap typeColumnMap;
			IPropertyMap orderByMap;
			string paramName = "";
			classMap = propertyMap.GetReferencedClassMap();
			if (classMap == null)
				throw new MappingException("ClassMap '" + propertyMap.DataType + "' Not Found!"); // do not localize

			IClassMap rootClassMap = classMap;
			rootTableMap = classMap.GetTableMap();
			tableMap = propertyMap.GetTableMap();

			if (tableMap != rootTableMap)
			{
				bool done = false;
				while (done == false && rootClassMap.InheritanceType != InheritanceType.ConcreteTableInheritance)
				{
					done = true;
					IClassMap super = rootClassMap.GetInheritedClassMap();
					if (super != null)
					{
						if (super.GetTableMap() == rootTableMap)
						{
							rootClassMap = super ; 
							done = false;
						}
					}
				}
				if (rootClassMap == null)
				{
					rootClassMap = classMap;
				}
			}

			SqlSelectStatement select = new SqlSelectStatement(tableMap.SourceMap) ; 
			SqlTableAlias table = select.GetSqlTableAlias(tableMap);

			idColumnMap = propertyMap.GetIdColumnMap();
			SqlColumnAlias idColumn = table.GetSqlColumnAlias(idColumnMap);

			myTableMap = idColumnMap.GetPrimaryKeyTableMap();
			myColumnMap = idColumnMap.GetPrimaryKeyColumnMap();

			if (myTableMap == null)
				throw new MappingException("TableMap '" + idColumnMap.PrimaryKeyTable + "' Not Found!"); // do not localize
			if (myColumnMap == null)
				throw new MappingException("ColumnMap '" + idColumnMap.PrimaryKeyColumn + "' Not Found!"); // do not localize

			SqlTableAlias rootTable = table;
			if (tableMap != rootTableMap)
			{
				rootTable = select.GetSqlTableAlias(rootTableMap, "NPersistRootTable");
			}

			SqlTableAlias myTable;
			SqlColumnAlias myColumn;

			IObjectManager om;
			orderByMap = propertyMap.GetOrderByPropertyMap();
			foreach (IPropertyMap iRefPropertyMap in rootClassMap.GetPrimaryPropertyMaps())
			{
				refPropertyMap = iRefPropertyMap;
				if (!((refPropertyMap.IsCollection || ((refPropertyMap.ReferenceType != ReferenceType.None && refPropertyMap.GetAdditionalColumnMaps().Count > 0) && !(refPropertyMap.IsIdentity)))))
				{
					IColumnMap refColumnMap = refPropertyMap.GetColumnMap();
					//SqlColumnAlias refColumn = table.GetSqlColumnAlias(refColumnMap);
					SqlColumnAlias refColumn = rootTable.GetSqlColumnAlias(refColumnMap);

					colName = refColumnMap.Name;
					if (refPropertyMap.IsIdentity)
						idColumns.Add(colName);

					if (!(refPropertyMap.LazyLoad))
					{
						select.SqlSelectClause.AddSqlAliasSelectListItem(refColumn);
						hashPropertyColumnMap[refPropertyMap.Name] = colName;
						if (refPropertyMap == orderByMap)
							select.SqlOrderByClause.AddSqlOrderByItem(refColumn);
					}
				}
			}

			select.SqlFromClause.AddSqlAliasTableSource(table);
			
			typeColumnMap = classMap.GetTypeColumnMap();
			if (typeColumnMap != null)
			{
				SqlColumnAlias typeColumn = rootTable.GetSqlColumnAlias(typeColumnMap);
				select.SqlSelectClause.AddSqlAliasSelectListItem(typeColumn);
				typeColumns.Add(typeColumnMap.Name);
			}

			if (table != rootTable)
			{
				select.SqlFromClause.AddSqlAliasTableSource(rootTable);			
			}

			//if (tableMap.SourceMap.Schema.ToLower(CultureInfo.InvariantCulture) == myTableMap.SourceMap.Schema.ToLower(CultureInfo.InvariantCulture) && tableMap.Name.ToLower(CultureInfo.InvariantCulture) == myTableMap.Name.ToLower(CultureInfo.InvariantCulture))
			if (myTableMap == tableMap)
				myTable = select.GetSqlTableAlias(myTableMap, "NPersistSelfRefTable");
			else
				myTable = select.GetSqlTableAlias(myTableMap, "NPersistOwnerTable");

			myColumn = myTable.GetSqlColumnAlias(myColumnMap);
			select.SqlFromClause.AddSqlAliasTableSource(myTable);

			SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition();
			search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals,  myColumn);

			foreach (IColumnMap iAddIdColumnMap in propertyMap.GetAdditionalIdColumnMaps())
			{
				addIdColumnMap = iAddIdColumnMap;
				addMyTableMap = addIdColumnMap.GetPrimaryKeyTableMap();
				addMyColumnMap = addIdColumnMap.GetPrimaryKeyColumnMap();
				if (addMyTableMap == null)
					throw new MappingException("TableMap '" + addIdColumnMap.PrimaryKeyTable + "' Not Found!"); // do not localize
				if (addMyColumnMap == null)
					throw new MappingException("ColumnMap '" + addIdColumnMap.PrimaryKeyColumn + "' Not Found!"); // do not localize

				SqlColumnAlias addIdColumn = table.GetSqlColumnAlias(addIdColumnMap);
				SqlColumnAlias addMyColumn = myTable.GetSqlColumnAlias(addMyColumnMap);
				
				search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(addIdColumn, SqlCompareOperatorType.Equals,  addMyColumn);
			}
			if (table != rootTable)
			{
				foreach (IPropertyMap testPropertyMap in classMap.GetAllPropertyMaps())
				{
					if (testPropertyMap.GetTableMap() == propertyMap.GetTableMap())
					{
						foreach (IColumnMap iAddIdColumnMap in testPropertyMap.GetAllIdColumnMaps())
						{
							addIdColumnMap = iAddIdColumnMap;

							SqlColumnAlias addIdColumn = table.GetSqlColumnAlias(addIdColumnMap);
							SqlColumnAlias rootColumn = rootTable.GetSqlColumnAlias(addIdColumnMap.GetPrimaryKeyColumnMap());
				
							search = select.SqlWhereClause.GetNextSqlSearchCondition();
							search.GetSqlComparePredicate(addIdColumn, SqlCompareOperatorType.Equals,  rootColumn);
						}												
						break;
					}
				}
			}
			om = m_SqlEngineManager.Context.ObjectManager;
			foreach (IPropertyMap iMyPropertyMap in propertyMap.ClassMap.GetIdentityPropertyMaps())
			{
				myPropertyMap = iMyPropertyMap;
				columnMap = myPropertyMap.GetColumnMap();
				SqlColumnAlias column = myTable.GetSqlColumnAlias(columnMap);
				SqlParameter param;
				paramName = GetParameterName(myPropertyMap, "Id_");
				if (om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty)
					param = AddSqlParameter(select, parameters, paramName, obj, myPropertyMap, om.GetOriginalPropertyValue(obj, myPropertyMap.Name), columnMap, true);
				else
					param = AddSqlParameter(select, parameters, paramName, obj, myPropertyMap, om.GetPropertyValue(obj, myPropertyMap.Name), columnMap);

				search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals,  param);				
			}
			typeColumnMap = propertyMap.ClassMap.GetTypeColumnMap();
			if (typeColumnMap != null)
			{
				SqlColumnAlias typeColumn = myTable.GetSqlColumnAlias(typeColumnMap);
				paramName = GetParameterName(propertyMap.ClassMap, "Type_");
				SqlParameter param = AddSqlParameter(select, parameters, paramName, obj, null, propertyMap.ClassMap.TypeValue, typeColumnMap, true);

				search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(typeColumn, SqlCompareOperatorType.Equals,  param);				
			}

			return GenerateSql(select);
		}

		protected virtual string GetSelectManyManyPropertyStatement(object obj, IPropertyMap propertyMap, IList idColumns, IList typeColumns, Hashtable hashPropertyColumnMap, IList parameters)
		{
			IColumnMap columnMap;
			IClassMap classMap;
			IPropertyMap refPropertyMap;
			ITableMap tableMap;
			ITableMap joinTableMap;
			string colName;
			IObjectManager om;
			IColumnMap idColumnMap;
			IPropertyMap myPropertyMap;
			IColumnMap myColumnMap;
			ITableMap myTableMap;
			IColumnMap colColumnMap;
			IColumnMap forColumnMap;
			ITableMap forTableMap;
			IColumnMap addColumnMap;
			IColumnMap addIdColumnMap;
			IColumnMap addMyColumnMap;
			ITableMap addMyTableMap;
			IColumnMap typeColumnMap;
			IPropertyMap orderByMap;
			string paramName = "";
			classMap = propertyMap.GetReferencedClassMap();
			if (classMap == null)
				throw new MappingException("ClassMap '" + propertyMap.DataType + "' Not Found!"); // do not localize

			tableMap = classMap.GetTableMap();
			SqlSelectStatement select = new SqlSelectStatement(tableMap.SourceMap) ; 
			SqlTableAlias table = select.GetSqlTableAlias(tableMap);

			joinTableMap = propertyMap.GetTableMap();
			SqlTableAlias joinTable = select.GetSqlTableAlias(joinTableMap);

			idColumnMap = propertyMap.GetIdColumnMap();
			SqlColumnAlias idColumn = joinTable.GetSqlColumnAlias(idColumnMap);

			myTableMap = idColumnMap.GetPrimaryKeyTableMap();
			myColumnMap = idColumnMap.GetPrimaryKeyColumnMap();
			if (myTableMap == null)
				throw new MappingException("TableMap '" + idColumnMap.PrimaryKeyTable + "' Not Found!"); // do not localize
			if (myColumnMap == null)
				throw new MappingException("ColumnMap '" + idColumnMap.PrimaryKeyColumn + "' Not Found!"); // do not localize
			SqlTableAlias myTable;

			colColumnMap = propertyMap.GetColumnMap();
			forTableMap = colColumnMap.GetPrimaryKeyTableMap();
			forColumnMap = colColumnMap.GetPrimaryKeyColumnMap();
			if (forTableMap == null)
				throw new MappingException("TableMap '" + idColumnMap.PrimaryKeyTable + "' Not Found!"); // do not localize
			if (forColumnMap == null)
				throw new MappingException("ColumnMap '" + idColumnMap.PrimaryKeyColumn + "' Not Found!"); // do not localize
			SqlTableAlias forTable = select.GetSqlTableAlias(forTableMap);
			SqlColumnAlias forColumn = forTable.GetSqlColumnAlias(forColumnMap);
			SqlColumnAlias colColumn = joinTable.GetSqlColumnAlias(colColumnMap);

			orderByMap = propertyMap.GetOrderByPropertyMap();

			foreach (IPropertyMap iRefPropertyMap in classMap.GetPrimaryPropertyMaps())
			{
				refPropertyMap = iRefPropertyMap;
				if (!((refPropertyMap.IsCollection || (refPropertyMap.ReferenceType != ReferenceType.None && !(refPropertyMap.IsIdentity)))))
				{
					IColumnMap refColumnMap = refPropertyMap.GetColumnMap();
					SqlColumnAlias refColumn = select.GetSqlColumnAlias(refColumnMap);
					colName = refColumnMap.Name;
					if (refPropertyMap.IsIdentity)
						idColumns.Add(colName);

					if (!(refPropertyMap.LazyLoad))
					{
						select.SqlSelectClause.AddSqlAliasSelectListItem(refColumn);

						hashPropertyColumnMap[refPropertyMap.Name] = colName;
						if (refPropertyMap == orderByMap)
							select.SqlOrderByClause.AddSqlOrderByItem(refColumn);
					}
				}
			}
			typeColumnMap = classMap.GetTypeColumnMap();
			if (typeColumnMap != null)
			{
				typeColumns.Add(typeColumnMap.Name);
				SqlColumnAlias typeColumn = select.GetSqlColumnAlias(typeColumnMap);
				select.SqlSelectClause.AddSqlAliasSelectListItem(typeColumn);
			}
			select.SqlFromClause.AddSqlAliasTableSource(table);
			select.SqlFromClause.AddSqlAliasTableSource(joinTable);

			//if (tableMap.SourceMap.Schema.ToLower(CultureInfo.InvariantCulture) == myTableMap.SourceMap.Schema.ToLower(CultureInfo.InvariantCulture) && tableMap.Name.ToLower(CultureInfo.InvariantCulture) == myTableMap.Name.ToLower(CultureInfo.InvariantCulture))
			if (tableMap == myTableMap)
				myTable = select.GetSqlTableAlias(myTableMap, "NPersistSelfRefTable");
			else
				myTable = select.GetSqlTableAlias(myTableMap);

			SqlColumnAlias myColumn = myTable.GetSqlColumnAlias(myColumnMap);

			select.SqlFromClause.AddSqlAliasTableSource(myTable);

			SqlSearchCondition search = select.SqlWhereClause.GetNextSqlSearchCondition();
			search.GetSqlComparePredicate(forColumn, SqlCompareOperatorType.Equals,  colColumn);

			foreach (IColumnMap iAddColumnMap in propertyMap.GetAdditionalColumnMaps())
			{
				addColumnMap = iAddColumnMap;
				addMyColumnMap = addColumnMap.GetPrimaryKeyColumnMap();
				if (addMyColumnMap == null)
					throw new MappingException("ColumnMap '" + addColumnMap.PrimaryKeyColumn + "' Not Found!"); // do not localize

				SqlColumnAlias addColumn = forTable.GetSqlColumnAlias(addColumnMap);
				SqlColumnAlias addMyColumn = joinTable.GetSqlColumnAlias(addMyColumnMap);

				search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(addColumn, SqlCompareOperatorType.Equals,  addMyColumn);
			}
			search = select.SqlWhereClause.GetNextSqlSearchCondition();
			search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals,  myColumn);

			foreach (IColumnMap iAddIdColumnMap in propertyMap.GetAdditionalIdColumnMaps())
			{
				addIdColumnMap = iAddIdColumnMap;
				addMyTableMap = addIdColumnMap.GetPrimaryKeyTableMap();
				addMyColumnMap = addIdColumnMap.GetPrimaryKeyColumnMap();

				if (addMyTableMap == null)
					throw new MappingException("TableMap '" + addIdColumnMap.PrimaryKeyTable + "' Not Found!"); // do not localize
				if (addMyColumnMap == null)
					throw new MappingException("ColumnMap '" + addIdColumnMap.PrimaryKeyColumn + "' Not Found!"); // do not localize

				SqlColumnAlias addIdColumn = joinTable.GetSqlColumnAlias(addIdColumnMap);
				SqlColumnAlias addMyColumn = myTable.GetSqlColumnAlias(addMyColumnMap);

				search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(addIdColumn, SqlCompareOperatorType.Equals,  addMyColumn);
			}

			om = m_SqlEngineManager.Context.ObjectManager;
			foreach (IPropertyMap iMyPropertyMap in propertyMap.ClassMap.GetIdentityPropertyMaps())
			{
				myPropertyMap = iMyPropertyMap;
				columnMap = myPropertyMap.GetColumnMap();
				SqlColumnAlias column = myTable.GetSqlColumnAlias(columnMap);
				SqlParameter param;

				paramName = GetParameterName(myPropertyMap, "Id_");
				if (om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty)
					param = AddSqlParameter(select, parameters, paramName, obj, myPropertyMap, om.GetOriginalPropertyValue(obj, myPropertyMap.Name), columnMap, true);
				else
					param = AddSqlParameter(select, parameters, paramName, obj, myPropertyMap, om.GetPropertyValue(obj, myPropertyMap.Name), columnMap);

				search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals,  param);
			}
			typeColumnMap = propertyMap.ClassMap.GetTypeColumnMap();
			if (typeColumnMap != null)
			{
				SqlColumnAlias typeColumn = myTable.GetSqlColumnAlias(typeColumnMap);
				paramName = GetParameterName(propertyMap.ClassMap, "Type_");
				SqlParameter param = AddSqlParameter(select, parameters, paramName, obj, null, propertyMap.ClassMap.TypeValue, typeColumnMap, true);
				search = select.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(typeColumn, SqlCompareOperatorType.Equals,  param);
			}
			return GenerateSql(select);
		}

		protected virtual string GetRemoveReferencesToObjectInTableStatement(object obj, IClassMap classMap, ITableMap myTableMap, ITableMap tableMap, IPropertyMap propertyMap, IList parameters)
		{
			if (classMap.DomainMap.GetClassMapsForTable(tableMap).Count < 1)
				return GetRemoveReferencesToObjectInLinkTableStatement(obj, classMap, myTableMap, tableMap, propertyMap, parameters);
			else
				return GetRemoveReferencesToObjectInEntityTableStatement(obj, classMap, myTableMap, tableMap, propertyMap, parameters);
		}


		protected virtual string GetRemoveReferencesToObjectInEntityTableStatement(object obj, IClassMap classMap, ITableMap myTableMap, ITableMap tableMap, IPropertyMap propertyMap, IList parameters)
		{
			IColumnMap columnMap;
			string wrappedValue;
			string compareOp = "";
			string paramName = "";

			SqlUpdateStatement update = new SqlUpdateStatement(tableMap.SourceMap);
			SqlTableAlias table = update.GetSqlTableAlias(tableMap);

			update.SqlUpdateClause.SqlTable = table.SqlTable;
 
			columnMap = propertyMap.GetColumnMap();

			if (columnMap.AllowNulls == false)
				return "";

			SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);
			SqlSearchCondition search = update.SqlWhereClause.GetNextSqlSearchCondition();
 			
			//Hack: For some reason it doesn't work to match NULL in parameterized queries...?
			wrappedValue = WrapValue(null, propertyMap, obj, columnMap, ref compareOp, true);
			if (wrappedValue == "NULL" && compareOp == "Is")
			{											
				search.GetSqlIsNullPredicate(column);
			}
			else
			{
				paramName = GetParameterName(propertyMap, "Org_");
				SqlParameter param = AddSqlParameter(update, parameters, paramName, null, propertyMap, obj, columnMap, true);
				search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
			}

			update.AddSqlColumnAndValue(column, new SqlNullValue());

			foreach (IColumnMap iColumnMap in propertyMap.GetAdditionalColumnMaps())
			{
				columnMap = iColumnMap;

				if (columnMap.AllowNulls == false)
					return "";

				column = table.GetSqlColumnAlias(columnMap);

				//Hack: For some reason it doesn't work to match NULL in parameterized queries...?
				wrappedValue = WrapValue(null, propertyMap, obj, columnMap, ref compareOp, true);
				if (wrappedValue == "NULL" && compareOp == "Is")
				{											
					//Thanks to Steven Miller for fixing a bug here
					search.GetSqlIsNullPredicate(column);
				}
				else
				{
					paramName = GetParameterName(propertyMap, columnMap, "Org_");
					SqlParameter param = AddSqlParameter(update, parameters, paramName, null, propertyMap, obj, columnMap, true);
					//Thanks to Steven Miller for fixing a bug here
					search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
				}

				update.AddSqlColumnAndValue(column, new SqlNullValue());
			}
			return GenerateSql(update);
		}

		protected virtual string GetRemoveReferencesToObjectInLinkTableStatement(object obj, IClassMap classMap, ITableMap myTableMap, ITableMap tableMap, IPropertyMap propertyMap, IList parameters)
		{
			IColumnMap columnMap;
			string wrappedValue;
			string compareOp = "";
			string paramName = "";

			SqlDeleteStatement delete = new SqlDeleteStatement(tableMap.SourceMap);
			SqlTableAlias table = delete.GetSqlTableAlias(tableMap);

			delete.SqlFromClause.AddSqlAliasTableSource(table);
 
			columnMap = propertyMap.GetColumnMap();
			SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);

			SqlSearchCondition search = delete.SqlWhereClause.GetNextSqlSearchCondition();
 
			//Hack: For some reason it doesn't work to match NULL in parameterized queries...?
			wrappedValue = WrapValue(null, propertyMap, obj, columnMap, ref compareOp, true);
			if (wrappedValue == "NULL" && compareOp == "Is")
				search.GetSqlIsNullPredicate(column);
			else
			{
				paramName = GetParameterName(propertyMap, "Org_");
				SqlParameter param = AddSqlParameter(delete, parameters, paramName, null, propertyMap, obj, columnMap, true);
				search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
			}

			foreach (IColumnMap iColumnMap in propertyMap.GetAdditionalColumnMaps())
			{
				columnMap = iColumnMap;
				column = table.GetSqlColumnAlias(columnMap);

				//Hack: For some reason it doesn't work to match NULL in parameterized queries...?
				wrappedValue = WrapValue(null, propertyMap, obj, columnMap, ref compareOp, true);
				if (wrappedValue == "NULL" && compareOp == "Is")
				{											
					//Thanks to Steven Miller for fixing a bug here
					search.GetSqlIsNullPredicate(column);
				}
				else
				{
					paramName = GetParameterName(propertyMap, columnMap, "Org_");
					SqlParameter param = AddSqlParameter(delete, parameters, paramName, null, propertyMap, obj, columnMap, true);
					//Thanks to Steven Miller for fixing a bug here
					search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals, param);
				}
			}
			return GenerateSql(delete);
		}

		protected virtual string GetRemoveCollectionPropertyStatement(object obj, IPropertyMap propertyMap, IList parameters)
		{
			IClassMap classMap;
			IColumnMap idColumnMap;
			IPropertyMap idPropertyMap;
			ITableMap tableMap;
			string paramName = "";
			IObjectManager om = m_SqlEngineManager.Context.ObjectManager;
			classMap = propertyMap.ClassMap;
			tableMap = propertyMap.GetTableMap();

			SqlDeleteStatement delete = new SqlDeleteStatement(tableMap.SourceMap) ; 
			SqlTableAlias table = delete.GetSqlTableAlias(tableMap);

			delete.SqlFromClause.AddSqlAliasTableSource(table);

			idColumnMap = propertyMap.GetIdColumnMap();
			SqlColumnAlias idColumn = table.GetSqlColumnAlias(idColumnMap);
			idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
			
			paramName = GetParameterName(idPropertyMap, "Id_");
			SqlParameter param =  AddSqlParameter(delete, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap);

			SqlSearchCondition search = delete.SqlWhereClause.GetNextSqlSearchCondition();
			search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals,  param);

			foreach (IColumnMap iIdColumnMap in propertyMap.GetAdditionalIdColumnMaps())
			{
				idColumnMap = iIdColumnMap;
				idColumn = table.GetSqlColumnAlias(idColumnMap);
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());

				paramName = GetParameterName(propertyMap, idColumnMap, "Id_");
				if (!(classMap.GetTypeColumnMap() == null && classMap.GetTypeColumnMap() == idColumnMap.GetPrimaryKeyColumnMap()))
				{
					param = AddSqlParameter(delete, parameters, paramName, obj, null, classMap.TypeValue, idColumnMap, true);
				}
				else
				{
					param = AddSqlParameter(delete, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap);
				}
				search = delete.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals,  param);
								
			}
			return GenerateSql(delete);
		}

		protected virtual string GetRemoveNonPrimaryStatement(object obj, ArrayList propertyMaps, IList parameters)
		{
			IClassMap classMap = null;
			IPropertyMap firstPropertyMap = null;
			IPropertyMap propertyMap;
			ITableMap tableMap = null;
			string compareOp = "";
			string wrappedValue;
			IObjectManager om;
			IPersistenceManager pm;
			IColumnMap idColumnMap = null;
			IPropertyMap idPropertyMap = null;
			string paramName = "";
			OptimisticConcurrencyBehaviorType propOptBehavior;
			om = m_SqlEngineManager.Context.ObjectManager;
			pm = m_SqlEngineManager.Context.PersistenceManager;
			foreach (IPropertyMap iPropertyMap in propertyMaps)
			{
				propertyMap = iPropertyMap;
				firstPropertyMap = propertyMap;
				classMap = propertyMap.ClassMap;
				tableMap = propertyMap.GetTableMap();
				idColumnMap = propertyMap.GetIdColumnMap();
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				break;
			}

			SqlDeleteStatement delete = new SqlDeleteStatement(tableMap.SourceMap) ; 
			SqlTableAlias table = delete.GetSqlTableAlias(tableMap);
			SqlColumnAlias idColumn = table.GetSqlColumnAlias(idColumnMap);

			delete.SqlFromClause.AddSqlAliasTableSource(table);
			
			paramName = GetParameterName(idPropertyMap, "Id_");
			SqlParameter param = AddSqlParameter(delete, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap);

			SqlSearchCondition search = delete.SqlWhereClause.GetNextSqlSearchCondition();
			search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals,  param);
			
			foreach (IColumnMap iIdColumnMap in firstPropertyMap.GetAdditionalIdColumnMaps())
			{
				idColumnMap = iIdColumnMap;
				idPropertyMap = classMap.GetPropertyMapForColumnMap(idColumnMap.GetPrimaryKeyColumnMap());
				idColumn = table.GetSqlColumnAlias(idColumnMap);

				paramName = GetParameterName(firstPropertyMap, idColumnMap, "Id_");
				if (!(classMap.GetTypeColumnMap() == null && classMap.GetTypeColumnMap() == idColumnMap.GetPrimaryKeyColumnMap()))
				{
					param = AddSqlParameter(delete, parameters, paramName, obj, null, classMap.TypeValue, idColumnMap, true);
				}
				else
				{
					param = AddSqlParameter(delete, parameters, paramName, obj, idPropertyMap, om.GetPropertyValue(obj, idPropertyMap.Name), idColumnMap);
				}
				search = delete.SqlWhereClause.GetNextSqlSearchCondition();
				search.GetSqlComparePredicate(idColumn, SqlCompareOperatorType.Equals,  param);
				
			}
			if (!(pm.GetDeleteOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, classMap) == OptimisticConcurrencyBehaviorType.Disabled))
			{
				foreach (IPropertyMap iPropertyMap in propertyMaps)
				{
					propertyMap = iPropertyMap;
					propOptBehavior = pm.GetDeleteOptimisticConcurrencyBehavior(OptimisticConcurrencyBehaviorType.DefaultBehavior, propertyMap);
					if (propOptBehavior == OptimisticConcurrencyBehaviorType.IncludeWhenLoaded || (propOptBehavior == OptimisticConcurrencyBehaviorType.IncludeWhenDirty && om.GetPropertyStatus(obj, propertyMap.Name) == PropertyStatus.Dirty))
					{
						if (om.HasOriginalValues(obj, propertyMap.Name))
						{
							bool first = true;
							foreach (IColumnMap columnMap in propertyMap.GetAllColumnMaps())
							{
								SqlColumnAlias column = table.GetSqlColumnAlias(columnMap);

								search = delete.SqlWhereClause.GetNextSqlSearchCondition();

								//Hack: For some reason it doesn't work to match NULL in parameterized queries...?
								wrappedValue = WrapValue(obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, ref compareOp, true);
								if (wrappedValue == "NULL" && compareOp == "Is")
									search.GetSqlIsNullPredicate(column); 
								else if (compareOp == "LIKE")
								{
									if (first)
										paramName = GetParameterName(propertyMap, "Org_");
									else
										paramName = GetParameterName(propertyMap, columnMap, "Org_");
									param = AddSqlParameter(delete, parameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
									search.GetSqlLikePredicate(column, param);
								}
								else
								{
									paramName = GetParameterName(propertyMap, "Org_");
									param = AddSqlParameter(delete, parameters, paramName, obj, propertyMap, om.GetOriginalPropertyValue(obj, propertyMap.Name), columnMap, true);
									search.GetSqlComparePredicate(column, SqlCompareOperatorType.Equals,  param);
								}
								first = false;
							}
						}
					}
				}
			}
			return GenerateSql(delete);
		}

		protected virtual string GetParameterName(IPropertyMap propertyMap)
		{
			return GetParameterName(propertyMap, "");			
		}

		protected virtual string GetParameterName(IPropertyMap propertyMap, string prefix)
		{
			string name = prefix;
			name = name + propertyMap.Name;
			name = "@" + name + GetNextParamNr().ToString() ;
			return name;
		}

		protected virtual long GetNextParamNr()
		{
			return this.Context.GetNextParamNr();
		}

		protected virtual string GetParameterName(IClassMap classMap)
		{
			return GetParameterName(classMap, "");			
		}

		protected virtual string GetParameterName(IClassMap classMap, string prefix)
		{
			string name = prefix;
			name = name + classMap.Name;
			name = "@" + name + GetNextParamNr().ToString() ;
            name = name.Replace('.', '_');
			return name;
		}

		protected virtual string GetParameterName(IPropertyMap propertyMap, IColumnMap columnMap)
		{
			return GetParameterName(propertyMap, columnMap, "");			
		}

		protected virtual string GetParameterName(IPropertyMap propertyMap, IColumnMap columnMap, string prefix)
		{
			string name = prefix;
			name = name + propertyMap.Name;
			name = name + "_" + columnMap.Name;
            name = "@" + name + GetNextParamNr().ToString();
			return name;
		}

		protected virtual string GenerateSql(SqlStatement statement)
		{
			ISqlVisitor visitor = GetVisitor();
			statement.Accept(visitor);
			return visitor.Sql;
		}

		protected virtual ISqlVisitor GetVisitor()
		{
			return new SqlVisitorBase();
		}

		#endregion 

		#region Query

		public IList LoadObjects(IQuery query, IList listToFill)
		{
			if (query.Query == null)
				throw new ArgumentNullException("query.Query");

			if (query.PrimaryType == null)
				throw new ArgumentNullException("query.PrimaryType");

			if (query is NPathQuery)
			{
				GetObjectsByNPath(query.Query.ToString(), query.PrimaryType, query.Parameters, query.RefreshBehavior, listToFill);
				return listToFill;
			}
			if (query is SqlQuery)
			{
				GetObjectsBySql(query.Query.ToString(), query.PrimaryType, query.Parameters, query.RefreshBehavior, listToFill);
				return listToFill;
			}
			throw new NPersistException("Query type not supported!");
		}

		public DataTable LoadDataTable(IQuery query)
		{
			if (query.Query == null)
				throw new ArgumentNullException("query.Query");

			if (query.PrimaryType == null)
				throw new ArgumentNullException("query.PrimaryType");

			if (query is NPathQuery)
				return GetDataTableByNPath(query.Query.ToString(), query.PrimaryType, query.Parameters, query.RefreshBehavior);
			if (query is SqlQuery)
				return GetDataTableBySql(query.Query.ToString(), query.PrimaryType, query.Parameters, query.RefreshBehavior);
			throw new NPersistException("Query type not supported!");
		}

		protected virtual DataTable GetDataTableBySql(string sqlQuery, Type type, IList parameters, RefreshBehaviorType refreshBehavior)
		{
			IList idColumns = new ArrayList();
			IList typeColumns = new ArrayList();
			Hashtable propertyColumnMap = new Hashtable();
			IQuery sq = new SqlQuery(sqlQuery);
			IList outParameters = new ArrayList(); 
			sq.ToSql(type, this.Context, ref idColumns, ref typeColumns, ref propertyColumnMap, ref outParameters, parameters);
			return GetDataTableBySql(sqlQuery, type, idColumns, typeColumns, propertyColumnMap, outParameters, refreshBehavior);
		}


		protected virtual DataTable GetDataTableByNPath(string NPathQuery, Type type, IList parameters, RefreshBehaviorType refreshBehavior)
		{
			IList outParameters = new ArrayList();
			IList idColumns = new ArrayList();
			IList typeColumns = new ArrayList();
			Hashtable propertyColumnMap = new Hashtable();
			IClassMap classMap;
			IColumnMap columnMap;
			classMap = this.Context.DomainMap.MustGetClassMap(type);
			columnMap = classMap.GetTypeColumnMap();
			if (columnMap != null)
			{
				typeColumns.Add(columnMap.Name);
			}
			foreach (IPropertyMap propertyMap in classMap.GetIdentityPropertyMaps())
			{
				columnMap = propertyMap.GetColumnMap();
				if (columnMap != null)
				{
					idColumns.Add(columnMap.Name);
				}
			}
			string sql = this.Context.NPathEngine.ToSql(NPathQuery, type, ref propertyColumnMap, ref outParameters, parameters);
			return GetDataTableBySql(sql, type, idColumns, typeColumns, propertyColumnMap, outParameters, refreshBehavior);
		}

		public virtual DataTable GetDataTableBySql(string sqlQuery, Type type, IList idColumns, IList typeColumns, Hashtable propertyColumnMap, IList parameters, RefreshBehaviorType refreshBehavior)
		{
			IDataSource ds = this.Context.DataSourceManager.GetDataSource(type);
			DataTable dt;
			dt = this.Context.SqlExecutor.ExecuteDataTable(sqlQuery, ds, parameters);
			return dt;
		}

		//From Context		
		protected virtual IList GetObjectsBySql(string sqlQuery, Type type, IList parameters, RefreshBehaviorType refreshBehavior, IList listToFill)
		{
			IList idColumns = new ArrayList();
			IList typeColumns = new ArrayList();
			Hashtable propertyColumnMap = new Hashtable();
			IQuery sq = new SqlQuery(sqlQuery);
			IList outParameters = new ArrayList(); 
			//IList inParameters = new ArrayList(); 
			sq.ToSql(type, this.Context, ref idColumns, ref typeColumns, ref propertyColumnMap, ref outParameters, parameters);
			return GetObjectsBySql(sqlQuery, type, idColumns, typeColumns, propertyColumnMap, outParameters, refreshBehavior, listToFill );
		}

		protected virtual IList GetObjectsByNPath(string npathQuery, Type type, IList parameters, RefreshBehaviorType refreshBehavior, IList listToFill)
		{
			IClassMap classMap = this.Context.DomainMap.MustGetClassMap(type);

			//IList result = InternalGetObjectsByNPath(npathQuery, type, parameters, refreshBehavior);
			InternalGetObjectsByNPath(npathQuery, type, parameters, refreshBehavior, listToFill);

			foreach (IClassMap subClassMap in classMap.GetSubClassMaps())
			{
				if (subClassMap.InheritanceType == InheritanceType.ConcreteTableInheritance)
				{
					Type subType = this.Context.AssemblyManager.MustGetTypeFromClassMap(subClassMap);
					InternalGetObjectsByNPath(npathQuery, subType, parameters, refreshBehavior, listToFill); 						

				}					
			}

			return listToFill;
		}

		//From PersistenceManager
		protected virtual void InternalGetObjectsByNPath(string npathQuery, Type type, IList parameters, RefreshBehaviorType refreshBehavior, IList listToFill)
		{
			IList outParameters = new ArrayList();
			IList idColumns = new ArrayList();
			IList typeColumns = new ArrayList();
			Hashtable propertyColumnMap = new Hashtable();
			IClassMap classMap;
			IColumnMap columnMap;
			classMap = this.Context.DomainMap.MustGetClassMap(type);
//			columnMap = classMap.GetTypeColumnMap();
//			if (columnMap != null)
//			{
//				typeColumns.Add(columnMap.Name);
//			}
			foreach (IPropertyMap propertyMap in classMap.GetIdentityPropertyMaps())
			{
				columnMap = propertyMap.GetColumnMap();
				if (columnMap != null)
				{
					idColumns.Add(columnMap.Name);
				}
			}
			string sql = this.Context.NPathEngine.ToSql(npathQuery, type, ref propertyColumnMap, ref outParameters, parameters);
			GetObjectsBySql(sql, type, idColumns, typeColumns, propertyColumnMap, outParameters, refreshBehavior, listToFill);
		}


		public virtual IList GetObjectsBySql(string sqlQuery, Type type, IList idColumns, IList typeColumns, Hashtable propertyColumnMap, IList parameters, RefreshBehaviorType refreshBehavior, IList listToFill)
		{
			IDataSource ds = this.Context.DataSourceManager.GetDataSource(type);
			IDataReader dr;
			//IList colRet = new ArrayList();
			IList colRet = listToFill;
			dr = this.Context.SqlExecutor.ExecuteReader(sqlQuery, ds, parameters);
			if (dr != null)
			{
				try
				{
					FetchObjectsFromDataReader(dr, ref colRet, type, idColumns, typeColumns, propertyColumnMap, refreshBehavior);
				}
				catch (Exception ex)
				{
					dr.Close();
					ds.ReturnConnection();
					throw ex; // do not localize
				}
				dr.Close();
			}
			ds.ReturnConnection();
			return colRet;
		}

		//Maybe one day this method should be modified to take a datatable and be moved outta here, eh ?

		//TODO: Modify so that this handles composite key references!!
		//First step - decide that propertyColumnMap can have values that are columnNames or Lists of columnNames....(done!)
		//when propertyColumnMap are lists, the convention is that:
		//A) if the referenced class has a type value (and the type value is part of the composite key), it should be the first value in the list
		//B) the rest if the items should be the rest of the composite 
		protected virtual void FetchObjectsFromDataReader(IDataReader dr, ref IList colret, Type type, IList idColumns, IList typeColumns, Hashtable propertyColumnMap, RefreshBehaviorType refreshBehavior)
		{
			IObjectManager om = this.Context.ObjectManager;
			IClassMap classMap = this.Context.DomainMap.MustGetClassMap(type);
			IDomainMap dm = this.Context.DomainMap;
			IAssemblyManager am = this.Context.AssemblyManager ;
			IListManager lm = this.Context.ListManager;
			Hashtable added = new Hashtable() ;
			Hashtable addedToList = new Hashtable() ;
			int rowNr = 0;
			object obj;
			object refObj;
			object objColumnIndex;
			IPropertyMap propertyMap;
			string strPropertyName;
			string[] arr;
			int start;
			int CurrLevel;
			int iLevel;
			int MaxLevel = 0;
			object value;
			object orgValue;
			string identity;
			string sep = classMap.IdentitySeparator;
			if (sep == "")
			{
				sep = "|";
			}
			bool doWrite;
			bool doWriteOrg;
			PropertyStatus propStatus;
			string strTypeValue;
			object discriminator; //TODO: ensure disc is set
			IClassMap useClassMap;
			IClassMap refClassMap;
			Type useType;
			IPersistenceManager pm = this.Context.PersistenceManager;
			Hashtable registerLoaded = new Hashtable() ;
			
			RefreshBehaviorType useRefreshBehavior;

			Hashtable foundIdColumns = new Hashtable() ;
			
			//iterate through each column in the datareader,
			//where each row contains id columns for the new object, eventual loaded properties and also possibly columns for values (icluding ids) of referenced objects
			//each column in the row should have a property-path associated ith it in propertyColumnMap
			//(some columns may be referenced by the same property-path)
			while (dr.Read())
			{
				rowNr += 1;
				useType = type;
				useClassMap = classMap;

				//begin by seeing if a typecolumn has been supplied
				//if so, get the type value from the datareader and make sure our classMap is of this type
				foreach (string strColumnName in typeColumns)
				{
					strTypeValue = (string) dr[strColumnName];
					if (!(strTypeValue == classMap.TypeValue))
					{
						useClassMap = classMap.GetBaseClassMap().GetSubClassWithTypeValue(strTypeValue);
						useType = this.Context.AssemblyManager.MustGetTypeFromClassMap(useClassMap);
					}
				}
				identity = "";
				//iterate through the identity properties for our type, get the columnname for each from propertyColumnMap
				//and get the id values from the datareader, then concatenate the id values to an identity string using the id separator of the type
				//also add the id columns to the foundIdColumns hashtable
				//if the id columns are repeated later in the idcolumnlist they can then be ignored
				foreach (IPropertyMap idPropertyMap in useClassMap.GetIdentityPropertyMaps())
				{
					if (propertyColumnMap.ContainsKey(idPropertyMap.Name))
					{
						if (propertyColumnMap[idPropertyMap.Name] is string)
						{
							identity += Convert.ToString(dr[(string) propertyColumnMap[idPropertyMap.Name]]) + sep;
							foundIdColumns[idPropertyMap.Column.ToLower(CultureInfo.InvariantCulture)] = true;							
						}
						else
						{
							IList aliases = (IList) propertyColumnMap[idPropertyMap.Name];
							int index = 0;

							IColumnMap typeColMap = useClassMap.GetTypeColumnMap();
							if (typeColMap != null)
							{
								strTypeValue = Convert.ToString(dr[(string) aliases[0]]);
								if (!(strTypeValue == classMap.TypeValue))
								{
									//When npath adds TypeColumn to where clause, uncomment this
									//useClassMap = classMap.GetSubClassWithTypeValue(strTypeValue);
									useClassMap = classMap.GetBaseClassMap().GetSubClassWithTypeValue(strTypeValue);

									//useType = type.Assembly.GetType(useClassMap.Name);
									useType = this.Context.AssemblyManager.MustGetTypeFromClassMap(useClassMap);
								}
								index = 1;
							}

							for (int iter = index; iter < aliases.Count ; iter++ )
							{
								identity += Convert.ToString(dr[(string) aliases[iter]]) + sep;
								foundIdColumns[idPropertyMap.Column.ToLower(CultureInfo.InvariantCulture)] = true;								
							}
						}
					}
				}
				//iterate through the id columns and concatenate the id values to an identity string using the id separator of the type
				//ignore idcolumns that are aready added in the previous loop
				foreach (string strColumnName in idColumns)
					if (!(foundIdColumns.ContainsKey(strColumnName.ToLower(CultureInfo.InvariantCulture))))
						identity += Convert.ToString(dr[strColumnName]) + sep;						

				//if the identity string ends with a separator, remove the trailing separator
				if (identity.Length > sep.Length)
					identity = identity.Substring(0, identity.Length - sep.Length);

				//ask the context for the object with our identity and type 
				//this is the "root" object represented by the row
				obj = this.Context.GetObjectById(identity, useType, true);

				//add the object to the result list
				object test = added[obj];
				if (test == null)
				{
					colret.Add(obj);
					added[obj] = obj;					
				}

				if (propertyColumnMap != null)
				{
					string typeName = type.ToString();
					//Find the maximum number of levels traversed in any of the propertypaths
					foreach (string iPropertyName in propertyColumnMap.Keys)
					{
						strPropertyName = iPropertyName;
						if (strPropertyName.Length > 0)
						{
							if (strPropertyName.IndexOf(".") > 0)
							{
								arr = strPropertyName.Split('.');
								iLevel = arr.GetUpperBound(0);
								if (String.Compare(arr[0], typeName, true, CultureInfo.InvariantCulture) == 0)
									iLevel -= 1;
								if (iLevel > MaxLevel)
									MaxLevel = iLevel;
							}
						}
					}
					//go through properties level by level, filling properties in each level with values from the datareader
					//first person.FirstName, person.LastName, person.Project and person.House
					//then person.Project.Name, person.Project.Date, person.Project.Company,
					//     person.House.Street, person.House.Color, person.House.City
					//then person.Project.Company.Name, person.Project.Company.Address..
					//     person.House.City.Name, person.House.City.Country....
					//this ensures that when travelling a propertypath all the parts in it will always be loaded
					for (int Level = 0; Level <= MaxLevel; Level++)
					{
						//iterate through all the propertypaths to see which represent properties on the current level
						foreach (string iPropertyName in propertyColumnMap.Keys)
						{
							strPropertyName = iPropertyName;
							if (strPropertyName.Length > 0)
							{
								objColumnIndex = propertyColumnMap[strPropertyName];
								refObj = obj;
								//if it is a propertypath, traverse it
								if (strPropertyName.IndexOf(".") > 0)
								{
									CurrLevel = 0;
									arr = strPropertyName.Split('.');
									start = 0;
									//if the propertypath beginis with the classname, skip one level
									if (arr[0].ToLower(CultureInfo.InvariantCulture) == type.ToString().ToLower(CultureInfo.InvariantCulture))
										start = 1;

									//count the number of levels in the path
									for (int i = start; i <= arr.GetUpperBound(0) - 1; i++)
										CurrLevel += 1;

									//check if the level of the path equals the current level
									if (CurrLevel == Level)
									{
										object prevObj = obj;
										string path = "";
										//if so, traverse the propertypath (knowing that all parts in it have been loaded)
										for (int i = start; i <= arr.GetUpperBound(0) - 1; i++)
										{
											//read the next part in the path
											refObj = om.GetPropertyValue(refObj, arr[i]);
											if (refObj == null)
												break;

											path += arr[i] + ".";

											IList listObject = refObj as IList;
											if (listObject != null)
											{
												string refId = "";
												IClassMap refObjClassMap = dm.MustGetClassMap(prevObj.GetType()).MustGetPropertyMap(arr[i]).GetReferencedClassMap() ;
												Type refType = am.GetTypeFromClassMap(refObjClassMap);
												foreach (IPropertyMap refIdPropertyMap in refObjClassMap.GetIdentityPropertyMaps() )
												{
													string find = path + refIdPropertyMap.Name;
													if (propertyColumnMap.ContainsKey(find))
													{
														if (propertyColumnMap[find] is string)
														{
															refId += Convert.ToString(dr[(string) propertyColumnMap[find]]) + sep;
															//foundIdColumns[refIdPropertyMap.Column.ToLower(CultureInfo.InvariantCulture)] = true;							
														}
														else
														{
															IList aliases = (IList) propertyColumnMap[find];
															int index = 0;

															IColumnMap typeColMap = refObjClassMap.GetTypeColumnMap();
															if (typeColMap != null)
															{
																strTypeValue = Convert.ToString(dr[(string) aliases[0]]);
																if (!(strTypeValue == classMap.TypeValue))
																{
																	//When npath adds TypeColumn to where clause, uncomment this
																	//useClassMap = classMap.GetSubClassWithTypeValue(strTypeValue);
																	refObjClassMap = classMap.GetBaseClassMap().GetSubClassWithTypeValue(strTypeValue);

																	//useType = type.Assembly.GetType(useClassMap.Name);
																	refType = am.MustGetTypeFromClassMap(useClassMap);
																}
																index = 1;
															}

															for (int iter = index; iter < aliases.Count ; iter++ )
															{
																refId += Convert.ToString(dr[(string) aliases[iter]]) + sep;
																//foundIdColumns[refIdPropertyMap.Column.ToLower(CultureInfo.InvariantCulture)] = true;								
															}
														}
													}
												}
												
												//if the identity string ends with a separator, remove the trailing separator
												if (refId.Length > sep.Length)
													refId = refId.Substring(0, refId.Length - sep.Length);

												refObj = this.Context.GetObjectById(refId, refType, true);

												if (registerLoaded[refObj] == null)
													registerLoaded[refObj] = refObj;

												string key = path + rowNr.ToString();
												object testRef = addedToList[key] ;
												if (testRef == null)
												{
													addedToList[key] = refObj;

													bool stackMute = false;
													IInterceptableList mList = listObject as IInterceptableList;
													if (mList != null)
													{
														stackMute = mList.MuteNotify;
														mList.MuteNotify = true;
													}
													listObject.Add(refObj);
													if (mList != null)
													{
														mList.MuteNotify = stackMute ;
													}
													IList orgList = (IList) om.GetOriginalPropertyValue(refObj, arr[i]);
													if (orgList == null)
													{				
														orgList = lm.CreateList(obj, arr[i]) ;
														om.SetOriginalPropertyValue(obj, arr[i], orgList);
													}
													mList = orgList as IInterceptableList;
													if (mList != null)
													{
														stackMute = mList.MuteNotify;
														mList.MuteNotify = true;
													}
													orgList.Add(refObj);
													if (mList != null)
													{
														mList.MuteNotify = stackMute ;
													}

												}
											}
											prevObj = refObj;
										}
									}
									//make sure we have arrived at an object to work with for the property, if not break
									if (refObj == null)
										break;
									else
										strPropertyName = arr[arr.GetUpperBound(0)];										
								}
								else
								{
									CurrLevel = 0;
								}
								//if the proppath is on the current level, put a value in the property from the datareader
								if (CurrLevel == Level)
								{
									if (refObj != null)
									{
										//Check if this property is part of a composite key foreign key (ref prop)
										//If so, add it to the list for this FK
										//Check if the list is complete
										//if so, do()
										discriminator = null;
										
										//get the class map for the object holding the property we're working with
										refClassMap = classMap.DomainMap.MustGetClassMap(refObj.GetType());

										//get the property map for the property we're working with
										propertyMap = refClassMap.GetPropertyMap(strPropertyName);

										if (propertyMap != null && !(CurrLevel == 0 && propertyMap.IsIdentity))
										{
											IList listColumnIndexes = objColumnIndex as IList;
											if (listColumnIndexes != null )
											{
                                                int startIndex = 0;
                                                if (propertyMap.GetInversePropertyMap() != null)
                                                {
                                                    //HACK: roger tried to fix this
                                                    //IClassMap otherClassMap = propertyMap.GetReferencedClassMap();
                                                    IClassMap otherClassMap = propertyMap.GetInversePropertyMap().ClassMap;//propertyMap.ClassMap;
                                                    IColumnMap typeColumnMap = otherClassMap.GetTypeColumnMap();
                                                    
                                                    if (typeColumnMap != null)
                                                    {
                                                        bool foundTypeCol = false;
                                                        foreach (IColumnMap idColumnMap in propertyMap.GetAllColumnMaps())
                                                            if (idColumnMap.GetPrimaryKeyColumnMap() == typeColumnMap)
                                                                foundTypeCol = true;

                                                        //if the referenced class has a type column our property
                                                        //has a column mapping to that type column, we should 
                                                        //assume that the first column in the column list
                                                        //is the type column, set the discriminator to this value 
                                                        //and remove it from the result
                                                        if (foundTypeCol)
                                                        {
                                                            if (Util.IsNumeric(Convert.ToString(listColumnIndexes[0])))
                                                                discriminator = dr[Convert.ToInt32(listColumnIndexes[0])];
                                                            else
                                                                discriminator = dr[Convert.ToString(listColumnIndexes[0])];
                                                            startIndex = 1;
                                                        }
                                                    }
                                                }
												orgValue = new ArrayList();
												for (int i = startIndex; i < listColumnIndexes.Count; i++)
												{
													object itemValue; 
													if (Util.IsNumeric(Convert.ToString(listColumnIndexes[i])))
														itemValue = dr[Convert.ToInt32(listColumnIndexes[i])];
													else
														itemValue = dr[Convert.ToString(listColumnIndexes[i])];												
													((IList)orgValue).Add(itemValue);
												}											
											}
											else
											{
												//get the unmanaged value from the datareader by column index, name or alias
												if (Util.IsNumeric(Convert.ToString(objColumnIndex)))
													orgValue = dr[Convert.ToInt32(objColumnIndex)];
												else
													orgValue = dr[Convert.ToString(objColumnIndex)];
											
											}

											//Manage the column value and set the managed value in a new variable
											value = pm.ManageLoadedValue( refObj, propertyMap, orgValue, discriminator);
											doWrite = false;
											doWriteOrg = false;
											propStatus = om.GetPropertyStatus(refObj, strPropertyName);

											useRefreshBehavior = pm.GetRefreshBehavior(refreshBehavior, refClassMap, propertyMap);

											if (useRefreshBehavior == RefreshBehaviorType.OverwriteNotLoaded || useRefreshBehavior == RefreshBehaviorType.DefaultBehavior)
											{
												//Overwrite both value and original for all unloaded properties
												if (propStatus == PropertyStatus.NotLoaded)
												{
													doWrite = true;
													doWriteOrg = true;
												}
											}
											else if (useRefreshBehavior == RefreshBehaviorType.OverwriteLoaded)
											{
												//Overwrite value and original for all clean or unloaded properties (but not for dirty or deleted properties)
												if (propStatus == PropertyStatus.Clean || propStatus == PropertyStatus.NotLoaded)
												{
													doWriteOrg = true;
													doWrite = true;												
												}
											}
											else if (useRefreshBehavior == RefreshBehaviorType.ThrowConcurrencyException)
											{
												//Overwrite original for all properties unless the old originial value and the fresh value from the
												//database mismatch, in that case raise an exception
												//Overwrite value for all clean or unloaded properties (but not for dirty or deleted properties)
												if (propStatus == PropertyStatus.Clean || propStatus == PropertyStatus.NotLoaded || propStatus == PropertyStatus.Dirty)
												{
													if (!(propStatus == PropertyStatus.NotLoaded))
													{
														object testValue = om.GetOriginalPropertyValue(refObj, strPropertyName);
														object testValue2 = value;
														if (DBNull.Value.Equals(testValue)) { testValue = null; }
														if (DBNull.Value.Equals(testValue2)) { testValue2 = null; }
														if (testValue2 != testValue)
														{
															string cachedValue = "null";
															string freshValue = "null";
															try
															{
																if (testValue != null)
																	cachedValue = testValue.ToString() ;
															} 
															catch { ; }
															try
															{
																if (value != null)
																	freshValue = value.ToString() ;
															} 
															catch { ; }
															if (!cachedValue.Equals(freshValue))
																throw new RefreshException("A refresh concurrency exception occurred when refreshing a cached object of type " + refObj.GetType().ToString() + " with fresh data from the data source. The data source row has been modified since the last time this version of the object was loaded, specifically the value for property " + strPropertyName + ". (this exception occurs because ThrowConcurrencyExceptions refresh behavior was selected). Cashed value: " + cachedValue + ", Fresh value: " + freshValue, cachedValue, freshValue, refObj, strPropertyName); // do not localize
														}
													}
													if (!(propStatus == PropertyStatus.Dirty))
														doWrite = true;
												}
											}
											else if (useRefreshBehavior == RefreshBehaviorType.OverwriteDirty)
											{
												//Overwrite original for all properties
												//Overwrite value for all clean, unloaded or dirty properties (but not for deleted properties)
												doWriteOrg = true;
												if (!(propStatus == PropertyStatus.Deleted))
													doWrite = true;
											}
											else
											{
												throw new NPersistException("Unknown object refresh behavior specified!"); // do not localize
											}
											if (doWrite || doWriteOrg)
											{
												//To keep inverse management correct,
												//We really should pick out a ref to any
												//eventual already referenced object here (in the
												//case of MergeBehaviorType.OverwriteDirty)
												//and perform proper inverse management on that object...

                                                if (doWrite)
                                                {
													om.SetPropertyValue(refObj, strPropertyName, value);
													om.SetNullValueStatus(refObj, strPropertyName, DBNull.Value.Equals(orgValue));
                                                }

												if (doWriteOrg)
												{
													if (propertyMap.ReferenceType == ReferenceType.None)
														om.SetOriginalPropertyValue(refObj, strPropertyName, orgValue);
													else
														om.SetOriginalPropertyValue(refObj, strPropertyName, value);												
												}

												if (propertyMap.ReferenceType != ReferenceType.None )
												{
													if (value != null)
													{
														if (registerLoaded[value] == null)
															registerLoaded[value] = value;

														//Inverse management
														if (doWrite)
															this.Context.InverseManager.NotifyPropertyLoad(refObj, propertyMap, value);
													}
												}
											}											
										}																				
									}
								}
							}
						}
					}
				}
				if (registerLoaded[obj] == null)
					registerLoaded[obj] = obj;
			}
			foreach (object register in registerLoaded.Keys)
			{
				this.Context.IdentityMap.RegisterLoadedObject(register);				
			}
		}


		#endregion


	}
}