using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Puzzle.FastTrack.WebForms.Framework.Controllers
{
    public interface IDomainController : IDisposable 
    {
        Assembly DomainAssembly { get; }

        object SelectedObject { get; set ; }

        object GetObjectByIdentity(object identity, Type type);

        object CreateObject(Type type);

        void SaveObject();

        void SaveObject(object obj);

        void DeleteObject();

        void DeleteObject(object obj);

        object GetPropertyValue(string propertyName);

        object GetPropertyValue(object obj, string propertyName);

        void SetPropertyValue(string propertyName, object value);

        void SetPropertyValue(object obj, string propertyName, object value);

    }
}
