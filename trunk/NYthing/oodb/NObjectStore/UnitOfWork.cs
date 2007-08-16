using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace NObjectStore
{
    class UnitOfWork
    {
        public UnitOfWork()
        {
            deletedObjects = new Hashtable();
            objects = new Hashtable();
            newObjects = new ArrayList();
            dirtyObjects = new Hashtable();
        }

        private Hashtable deletedObjects;

        public Hashtable DeletedObjects
        {
            get { return deletedObjects; }
        }

        private Hashtable dirtyObjects;

        public Hashtable DirtyObjects
        {
            get { return dirtyObjects; }
        }

        private Hashtable objects;

        public Hashtable Objects
        {
            get { return objects; }
        }

        private ArrayList newObjects;

        public ArrayList NewObjects
        {
            get { return newObjects; }
        }
    }
}
