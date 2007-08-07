using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using Puzzle.FastTrack.Framework.Filtering;

namespace Puzzle.FastTrack.Framework.Controllers
{
    public interface IDomainController : IDisposable 
    {
        Assembly DomainAssembly { get; set; }

        void RegisterTypeController(string typeName, ITypeController typeController);

        void RegisterTypeController(Type type, ITypeController typeController);

        ITypeController GetTypeController(Type type);

        bool HasTypeController(Type type);

        Type SelectedType { get; set; }

        object SelectedObject { get; set ; }

        IList SelectedObjects { get; set; }

        string SelectedPropertyName { get; set; }

        object GetObjectByIdentity(object identity, Type type);

        string GetObjectIdentity(object obj);

        IList GetObjectsOfType(Type type);

        IList GetObjectsOfType(Type type, Filter filter);

        object CreateObject(Type type);

        object ExecuteCreateObject(Type type);

        void SaveObject();

        void SaveObject(object obj);

        void ExecuteSaveObject(object obj);

        void DeleteObject();

        void DeleteObject(object obj);

        void ExecuteDeleteObject(object obj);

        bool IsListProperty(string propertyName);

        bool IsListProperty(object obj, string propertyName);

        bool IsListProperty(Type type, string propertyName);

        bool IsReadOnlyProperty(string propertyName);

        bool IsReadOnlyProperty(object obj, string propertyName);

        bool IsReadOnlyProperty(Type type, string propertyName);

        Type GetListPropertyItemType(string propertyName);

        Type GetListPropertyItemType(object obj, string propertyName);

        bool IsNullableProperty(string propertyName);

        bool IsNullableProperty(object obj, string propertyName);

        bool IsNullableProperty(Type type, string propertyName);

        bool GetPropertyNullStatus(string propertyName);

        bool GetPropertyNullStatus(object obj, string propertyName);

        void SetPropertyNullStatus(string propertyName, bool isNull);

        void SetPropertyNullStatus(object obj, string propertyName, bool isNull);

        object GetPropertyValue(string propertyName);

        object GetPropertyValue(object obj, string propertyName);

        void SetPropertyValue(string propertyName, object value);

        void SetPropertyValue(object obj, string propertyName, object value);

        string GetObjectName();

        string GetObjectName(object obj);

        //Useful for getting unproxied type if domain objects are proxied
        Type GetTypeFromType();

        Type GetTypeFromType(Type type);

        string GetTypeNameFromType();

        string GetTypeNameFromType(Type type);

        Type GetTypeFromTypeName(string typeName);

        IList GetTypeNames();
    }
}
