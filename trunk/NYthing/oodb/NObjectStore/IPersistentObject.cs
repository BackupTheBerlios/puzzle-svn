using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace NObjectStore
{
    public interface IPersistentObject
    {
        string Id { get;set;}


        object GetPropertyValue(string property);
        void SetReference(string property, object value);

        bool IsUnloaded(string property);
        void SetUnloaded(string property, bool unloaded);
        Context Context { get;set;}
        bool Mute { get;set;}
        IList GetReferences();
        void AddReference(ObjectReference reference);
        void RemoveReference(ObjectReference reference);

        string Serialize();
    }
}
