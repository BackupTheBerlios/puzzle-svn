using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Puzzle.FastForward.Framework.Service
{
    public interface ISchemaService
    {
        void Commit();

        void Abort();

        /// <summary>
        /// Creates a new class mapping to a table with the same name.
        /// </summary>
        /// <remarks>The class will not inherit from any base class and it will not be able to become a base class for another class.</remarks>
        /// <param name="name">The name of the class and the table.</param>
        /// <returns></returns>
        void CreateClass(string name);

        void CreateTable(string name);

        void MapClassToTable(string className, string tableName);

        void CreateProperty(string className, string propertyName, string propertyType);

        void SetPropertyMetaData(string className, string propertyName, PropertyMetaData propertyMetaData, object value);

        void CreateColumn(string tableName, string columnName, DbType dbType);

        void SetColumnMetaData(string tableName, string columnName, ColumnMetaData columnMetaData, object value);

        void MapPropertyToColumn(string className, string propertyName, string tableName, string columnName);

        string GetTableForClass(string className);

        bool HasClass(string className);

        bool HasProperty(string className, string propertyName);

        Type GetPropertyType(string className, string propertyName);

        IList<string> GetPropertyNames(Type type);

        string GetIdentityPropertyName(string className);

        void CreateListProperty(string className, string propertyName, string propertyType);
    }
}
