using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace NObjectStore
{
    public class UnitOfWork
    {
        public UnitOfWork()
        {
            deletedObjects = new Dictionary<string, IPersistentObject>();
            objects = new Dictionary<string, WeakReference>();
            newObjects = new List<object>();
            dirtyObjects = new Dictionary<string, IPersistentObject>();
        }

        private IDictionary<string, IPersistentObject> deletedObjects;

        public IDictionary<string, IPersistentObject> DeletedObjects
        {
            get { return deletedObjects; }
        }

        private IDictionary<string, IPersistentObject> dirtyObjects;

        public IDictionary<string, IPersistentObject> DirtyObjects
        {
            get { return dirtyObjects; }
        }

        private IDictionary<string, WeakReference> objects;

        public IDictionary<string, WeakReference> Objects
        {
            get { return objects; }
        }

        private IList<object> newObjects;

        public IList<object> NewObjects
        {
            get { return newObjects; }
        }
    }
}
