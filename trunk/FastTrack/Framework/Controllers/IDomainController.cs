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
        Assembly DomainAssembly { get; }

        Type SelectedType { get; set; }

        object SelectedObject { get; set ; }

        IList SelectedObjects { get; set; }

        string SelectedPropertyName { get; set; }

        object GetObjectByIdentity(object identity, Type type);

        string GetObjectIdentity(object obj);

        IList GetObjectsOfType(Type type);

        IList GetObjectsOfType(Type type, Filter filter);

        object CreateObject(Type type);

        void SaveObject();

        void SaveObject(object obj);

        void DeleteObject();

        void DeleteObject(object obj);

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
    }
}
