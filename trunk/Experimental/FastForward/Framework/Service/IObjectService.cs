using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Puzzle.FastForward.Framework.Service
{
    public interface IObjectService
    {
        object CreateObject(Type type);

        void DeleteObject(object obj);

        IList GetObjects(Type type, IDictionary<string, object> match);

        IList GetObjects(Type type, string where);

        void Commit();

        void Abort();

        object GetIdentity(object obj);

        void SetProperty(object obj, string propertyName, object value);

        Type GetTypeByName(string className);

        object GetProperty(object obj, string propertyName);

        string GetTypeName(object obj);

        string GetTypeName(Type type);

        bool IsNull(object obj, string propertyName);
    }
}
