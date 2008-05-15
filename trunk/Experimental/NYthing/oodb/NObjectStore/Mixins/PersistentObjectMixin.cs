using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using System.Collections;
using Puzzle.NAspect.Framework;

namespace NObjectStore
{
    public class PersistentObjectMixin : IPersistentObject , IProxyAware
    {

        private IPersistentObject target;
        public string Id { get; set; }
        public Context Context { get; set; }

        public PersistentObjectMixin()
        {
        }

        ~PersistentObjectMixin()
        {
            if (Context != null)
            {
                Context.NotifyUnload(Id);
            }
        }
        
        public void SetProxy(IAopProxy target)
        {
            this.target = (IPersistentObject)target;
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

        private PropertyDictionary properties = new PropertyDictionary();
        public void SetReference(string property, object value)
        {
            if (Mute)
                return;

            //drop old references
            object currentValue = null;
            properties.TryGetValue(property, out currentValue);

            if (currentValue is PersistentId)
            {
                PersistentId id = (PersistentId)currentValue;
                if (Context.IsLoaded(id.Id))
                {
                    IPersistentObject oldRef = Context.Get(id.Id);                    
                    ObjectReference reference = new ObjectReference();
                    reference.Property = property;
                    reference.ObjectId = target.Id;
                    ((IPersistentObject)oldRef).RemoveReference(reference);
                }
            }
            
            //create new reference
            if (value is IPersistentObject)
            {                
                ObjectReference reference = new ObjectReference();
                reference.Property = property;
                reference.ObjectId = target.Id;
                ((IPersistentObject)value).AddReference(reference);

                PersistentId id = new PersistentId();
                id.Id = ((IPersistentObject)value).Id;
                value = id;
            }

            properties[property] = value;
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

        private bool initializing = true;
        public bool Initializing
        {
            get
            {
                return initializing;
            }
            set
            {
                initializing = value;
            }
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
            var persistentProperties = new PropertyDictionary();
            foreach (var entry in properties)
            {
                if ((string)entry.Key == "Initializing")
                    continue;

                if ((string)entry.Key == "Mute")
                    continue;

                if ((string)entry.Key == "Context")
                    continue;

                if ((string)entry.Key == "Id")
                    continue;

                persistentProperties.Add(entry.Key, entry.Value);
            }

            SerializedObject so = new SerializedObject();
            so.Data = persistentProperties;
            so.Type = target.GetType().BaseType;

            return JSONSerializer.Serialize (so) ;
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
