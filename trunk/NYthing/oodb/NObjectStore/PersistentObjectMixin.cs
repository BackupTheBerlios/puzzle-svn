using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using System.Collections;

namespace NObjectStore
{
    public class PersistentObjectMixin : IPersistentObject , IProxyAware
    {
        public PersistentObjectMixin()
        {
        }

        private object target;
        public void SetProxy(Puzzle.NAspect.Framework.IAopProxy target)
        {
            this.target = target;
        }

        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public object GetPropertyValue(string property)
        {
            object res = properties[property];
            if (res is PersistentId)
            {
                PersistentId id = res as PersistentId;
                res = Context.Get(id.Id);
            }

            return res;
        }

        private Hashtable properties = new Hashtable();
        public void SetReference(string property, object value)
        {
            if (Mute)
                return;

            if (properties[property] is PersistentId)
            {
                PersistentId id = (PersistentId)properties[property];
                if (context.IsLoaded(id.Id))
                {
                    object oldRef = context.Get(id.Id);                    
                    ObjectReference reference = new ObjectReference();
                    reference.Property = property;
                    reference.ObjectId = ((IPersistentObject)target).Id;
                    ((IPersistentObject)oldRef).RemoveReference(reference);
                }
            }
            

            if (value is IPersistentObject)
            {
                ObjectReference reference = new ObjectReference();
                reference.Property = property;
                reference.ObjectId = ((IPersistentObject)target).Id;
                ((IPersistentObject)value).AddReference(reference);

                PersistentId id = new PersistentId();
                id.Id = ((IPersistentObject)value).Id;
                value = id;
            }

            properties[property] = value;
        }

        private Context context;
        public Context Context
        {
            get
            {
                return context;
            }
            set
            {
                context = value;
            }
        }

        private Hashtable unloadedProperties = new Hashtable();
        public bool IsUnloaded(string property)
        {
            object res = unloadedProperties[property];
            if (res != null && (bool)res == true)
                return true;

            return false;
        }

        public void SetUnloaded(string property, bool unloaded)
        {
            unloadedProperties[property] = unloaded;
        }

        private bool mute;

        public bool Mute
        {
            get { return mute; }
            set { mute = value; }
        }

        private IList references = new ArrayList();
        public IList GetReferences()
        {
            return references;
        }

        public void AddReference(ObjectReference reference)
        {
            references.Add(reference);
        }

        public void RemoveReference(ObjectReference reference)
        {
            foreach (ObjectReference other in references)
            {
                if (other.ObjectId == reference.ObjectId && other.Property == reference.Property)
                {
                    references.Remove(other);
                    return;
                }
            }
        }

        public string Serialize()
        {
            Hashtable ht = new Hashtable();
            foreach (DictionaryEntry de in properties)
            {
                if ((string)de.Key == "Mute")
                    continue;

                if ((string)de.Key == "Context")
                    continue;

                if ((string)de.Key == "Id")
                    continue;

                ht.Add(de.Key, de.Value);
            }

            SerializedObject so = new SerializedObject();
            so.Data = ht;
            so.Type = target.GetType().BaseType;

            return SerializedObject.Serialize (so) ;
        }

       
    }

    [Serializable]
    public class PersistentId
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }

    public class ObjectReference
    {
        private string property;

        public string Property
        {
            get { return property; }
            set { property = value; }
        }

        private string objectId;

        public string ObjectId
        {
            get { return objectId; }
            set { objectId = value; }
        }
    }
}
