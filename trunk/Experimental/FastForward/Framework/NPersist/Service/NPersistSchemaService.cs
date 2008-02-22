using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework.Mapping;
using System.Data;
using Puzzle.NPersist.Framework.Mapping.Serialization;
using Puzzle.SideFX.Framework;
using Puzzle.NPersist.Framework.Persistence;
using Puzzle.NPersist.Framework;

namespace Puzzle.FastForward.Framework.Service
{
    public class NPersistSchemaService : ISchemaService
    {
        public NPersistSchemaService(IEngine engine)
        {
            this.engine = engine;
        }

        private IEngine engine;

        private IDomainMap domainMap = null;

        public IDomainMap GetDomainMap()
        {
            if (domainMap == null)
            {
                ILoggingService loggingService = engine.GetService<ILoggingService>();
                if (loggingService != null)
                    loggingService.LogInfo(this, "Loading NPersist mapping file.");

                IConfigurationService configurationService = engine.GetService<IConfigurationService>();

                IMapSerializer serializer = new DefaultMapSerializer();
                this.domainMap = DomainMap.Load(configurationService.SchemaFilePath, serializer, false, false);
            }
            return this.domainMap;
        }

        public void Commit()
        {
            if (domainMap != null)
            {
                ILoggingService loggingService = engine.GetService<ILoggingService>();
                if (loggingService != null)
                    loggingService.LogInfo(this, "Saving NPersist mapping file.");

                string path = @"C:\Test\test.xml";
                IMapSerializer serialiser = new DefaultMapSerializer();
                domainMap.Save(path, serialiser);
                domainMap = null;
            }
        }

        public void Abort()
        {
            if (domainMap != null)
            {
                ILoggingService loggingService = engine.GetService<ILoggingService>();
                if (loggingService != null)
                    loggingService.LogInfo(this, "Discarding changes to NPersist mapping file.");

                domainMap = null;
            }
        }

        public void CreateClass(string name)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Creating class {0} in NPersist mapping file.",
                    name));

            //Add the class to the npersist xml file
            IClassMap classMap = new ClassMap(name);
            classMap.DomainMap = GetDomainMap();
        }

        public void CreateProperty(string className, string propertyName, string propertyType)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Creating {0} property {1} in class {2} in NPersist mapping file.",
                    propertyType, propertyName, className));

            IClassMap classMap = GetDomainMap().MustGetClassMap(className);

            //Add the property to the npersist xml file
            IPropertyMap propertyMap = new PropertyMap(propertyName);
            propertyMap.ClassMap = classMap;

            propertyMap.DataType = propertyType;
        }

        public void CreateListProperty(string className, string propertyName, string propertyType)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Creating list property {0}, item type {1} in class {2} in NPersist mapping file.",
                    propertyName, propertyType, className));

            IClassMap classMap = GetDomainMap().MustGetClassMap(className);

            //Add the property to the npersist xml file
            IPropertyMap propertyMap = new PropertyMap(propertyName);
            propertyMap.ClassMap = classMap;
            propertyMap.IsCollection = true;

            propertyMap.ItemType = propertyType;
        }


        public void CreateTable(string name)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Creating table {0} in NPersist mapping file.",
                    name));

            //Add the table to the npersist xml file
            ITableMap tableMap = new TableMap(name);
            tableMap.SourceMap = GetDomainMap().GetSourceMap();
        }

        public void CreateColumn(string tableName, string columnName, DbType dbType)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Creating {0} column {1} in table {2} in NPersist mapping file.",
                    dbType.ToString(), columnName, tableName));

            ITableMap tableMap = GetDomainMap().GetSourceMap().MustGetTableMap(tableName);

            //Add the column to the npersist xml file
            IColumnMap columnMap = new ColumnMap(columnName);
            columnMap.TableMap = tableMap;
            columnMap.DataType = dbType;            
        }

        public void MapClassToTable(string className, string tableName)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Mapping class {0} to table {1} in NPersist mapping file.",
                    className, tableName));

            IClassMap classMap = GetDomainMap().MustGetClassMap(className);

            //Map the class to the table in the npersist xml file
            classMap.Table = tableName;
        }

        public void MapPropertyToColumn(string className, string propertyName, string tableName, string columnName)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Mapping property {0}.{1} to column {2}.{3} in NPersist mapping file.",
                    className, propertyName, tableName, columnName));

            IClassMap classMap = GetDomainMap().MustGetClassMap(className);
            IPropertyMap propertyMap = classMap.MustGetPropertyMap(propertyName);
            propertyMap.Column = columnName;
        }

        public void SetPropertyMetaData(string className, string propertyName, PropertyMetaData propertyMetaData, object value)
        {
            
            IClassMap classMap = GetDomainMap().MustGetClassMap(className);
            IPropertyMap propertyMap = classMap.MustGetPropertyMap(propertyName);
            switch (propertyMetaData)
            {
                case PropertyMetaData.Identity:
                    SetIdentity(propertyMap, (bool) value);
                    break;
                case PropertyMetaData.Nullable:
                    SetNullable(propertyMap, (bool)value);
                    break;
                case PropertyMetaData.SourceAssigned:
                    SetSourceAssigned(propertyMap, (bool)value);
                    break;
                default:
                    throw new Exception("Unknown property meta data type " + propertyMetaData.ToString());
            }
        }

        private void SetIdentity(IPropertyMap propertyMap, bool value)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Setting rule for property {0}.{1}: identity = {2}.",
                    propertyMap.ClassMap.Name, propertyMap.Name, value.ToString()));

            propertyMap.IsIdentity = value;
        }

        private void SetNullable(IPropertyMap propertyMap, bool value)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Setting rule for property {0}.{1}: nullable = {2}.",
                    propertyMap.ClassMap.Name, propertyMap.Name, value.ToString()));

            propertyMap.IsNullable = value;
        }

        private void SetSourceAssigned(IPropertyMap propertyMap, bool value)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Setting rule for property {0}.{1}: assigned by source = {2}.",
                    propertyMap.ClassMap.Name, propertyMap.Name, value.ToString()));

            propertyMap.IsAssignedBySource = value;
        }

        public void SetColumnMetaData(string tableName, string columnName, ColumnMetaData columnMetaData, object value)
        {
            ITableMap tableMap = GetDomainMap().GetSourceMap().MustGetTableMap(tableName);
            IColumnMap columnMap = tableMap.MustGetColumnMap(columnName);
            switch (columnMetaData)
            {
                case ColumnMetaData.PrimaryKey:
                    SetPrimaryKey(columnMap, (bool)value);
                    break;
                case ColumnMetaData.Nullable:
                    SetAllowNulls(columnMap, (bool)value);
                    break;
                case ColumnMetaData.AutoIncreaser:
                    SetAutoIncreaser(columnMap, (bool)value);
                    break;
                default:
                    throw new Exception("Unknown column meta data type" + columnMetaData.ToString());
            }
        }

        private void SetPrimaryKey(IColumnMap columnMap, bool value)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Setting rule for column {0}.{1}: primary key = {2}.",
                    columnMap.TableMap.Name, columnMap.Name, value.ToString()));

            columnMap.IsPrimaryKey = value;
        }

        private void SetAllowNulls(IColumnMap columnMap, bool value)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Setting rule for column {0}.{1}: nullable = {2}.",
                    columnMap.TableMap.Name, columnMap.Name, value.ToString()));

            columnMap.AllowNulls = value;
        }

        private void SetAutoIncreaser(IColumnMap columnMap, bool value)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Setting rule for column {0}.{1}: auto increaser = {2}.",
                    columnMap.TableMap.Name, columnMap.Name, value.ToString()));

            columnMap.IsAutoIncrease = value;
        }

        public string GetTableForClass(string className)
        {
            IClassMap classMap = GetDomainMap().MustGetClassMap(className);
            return classMap.Table;
        }

        public bool HasClass(string className)
        {
            IClassMap classMap = GetDomainMap().GetClassMap(className);
            if (classMap != null)
                return true;
            return false;
        }

        public bool HasProperty(string className, string propertyName)
        {
            IClassMap classMap = GetDomainMap().GetClassMap(className);
            if (classMap != null)
            {
                IPropertyMap propertyMap = classMap.GetPropertyMap(propertyName);
                if (propertyMap != null)
                    return true;
            }
            return false;
        }

        public Type GetPropertyType(string className, string propertyName)
        {
            IClassMap classMap = GetDomainMap().GetClassMap(className);
            if (classMap != null)
            {
                IPropertyMap propertyMap = classMap.GetPropertyMap(propertyName);
                if (propertyMap != null)
                    return Type.GetType(propertyMap.DataType);
            }
            return null;
        }

        public IList<string> GetPropertyNames(Type type)
        {
            IList<string> propertyNames = new List<string>();
            IClassMap classMap = GetDomainMap().MustGetClassMap(type);
            foreach (IPropertyMap propertyMap in classMap.GetAllPropertyMaps())
                propertyNames.Add(propertyMap.Name);
            return propertyNames;
        }

        public string GetIdentityPropertyName(string className)
        {
            IClassMap classMap = GetDomainMap().MustGetClassMap(className);
            foreach (IPropertyMap propertyMap in classMap.GetIdentityPropertyMaps())
                return propertyMap.Name;
            return "";
        }

    }
}
