using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework;
using System.Collections;
using System.Reflection;

namespace NObjectStore
{
    public class Context
    {
        private IEngine engine;
        private UnitOfWork uow;
        private string dbPath;

        private void RegisterObject(IPersistentObject managed)
        {
            WeakReference reff = new WeakReference (managed);
            uow.Objects[managed.Id] = reff;
        }

        public Context(string dbPath)
        {
            if (!System.IO.Directory.Exists(dbPath))
            {
                System.IO.Directory.CreateDirectory(dbPath);
            }
            this.dbPath = dbPath;

            engine = ApplicationContext.Configure ();
            uow = new UnitOfWork();
        }

        public void Unload(object instance)
        {
            CommitObject(instance);
            DropReferences(instance);
        }

        private void DropReferences(object instance)
        {
            IPersistentObject managed = (IPersistentObject)instance;
            this.uow.Objects.Remove(managed.Id);
            IList references = managed.GetReferences();
            ArrayList tmp = new ArrayList(references);
            foreach (ObjectReference reference in tmp)
            {
                if (!IsLoaded(reference.ObjectId))
                    continue;

                IPersistentObject referencedObject = (IPersistentObject)this.Get(reference.ObjectId);
                referencedObject.SetUnloaded(reference.Property, true);
                Type referencedObjectType = referencedObject.GetType();
                PropertyInfo pi = referencedObjectType.GetProperty(reference.Property);
                bool stackMute = referencedObject.Mute;
                referencedObject.Mute = true;
                pi.SetValue(referencedObject, null, null);
                referencedObject.Mute = stackMute;
            }
        }

        public void CommitObject(object instance)
        {
            IPersistentObject managed = (IPersistentObject)instance;            
            string serializedData = managed.Serialize();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(GetObjectFile(managed), false);
            sw.WriteLine(serializedData);
            sw.Close();
            this.uow.DirtyObjects.Remove(managed.Id);
        }

        private string GetObjectFile(IPersistentObject managed)
        {
            return dbPath + @"\" + managed.Id + ".txt";
        }

        private string GetObjectFile(string id)
        {
            return dbPath + @"\" + id + ".txt";
        }

        public void Commit()
        { 
            ArrayList dirty = new ArrayList(uow.DirtyObjects.Values);
            foreach (IPersistentObject instance in dirty)
            {
                CommitObject(instance);                
            }

            ArrayList deleted = new ArrayList (uow.DeletedObjects.Values);
            foreach (PersistentId id in deleted)
            {
                DeleteObject(id.Id);
            }

            uow.DirtyObjects.Clear();
            uow.DeletedObjects.Clear();
            uow.NewObjects.Clear();
        }

        private void DeleteObject(string id)
        {
            try
            {
                System.IO.File.Delete(GetObjectFile(id));
            }
            catch
            {
            }
        }


        public T CreateNamed<T>(string name,params object[] args)
        {
            string id = name;
            object instance = Create<T>(id,args );
            return (T)instance;
        }

        public T Create<T>(params object[] args)
        {
            string id = Guid.NewGuid().ToString();
            object instance = Create<T>(id, args);
            return (T)instance;
        }

        private object Create<T>(string id,object[] args)
        {
            IPersistentObject instance = (IPersistentObject)engine.CreateProxyWithState<T>(this, args);
            uow.NewObjects.Add(instance);
            ((IPersistentObject)instance).Id = id;
            RegisterObject(instance);            
            return instance;
        }

        

        public T Get<T>(string id)
        {
            return (T)Get(id);
        }

        public void RegisterDirty(IPersistentObject managed)
        {
            uow.DirtyObjects[managed.Id] = managed;
        }

        public object Get(string id)
        {
            if (uow.Objects.Contains(id))
            {
                WeakReference reff = (WeakReference)uow.Objects[id];
                object instance = reff.Target;
                if (reff.IsAlive)
                {
                   return instance; //return object if possible
                }
                else
                {
                    uow.Objects.Remove(id); //clear dead ref
                }
            }
            
            //try to load object 
            string filePath = dbPath + @"\" + id + ".txt";
            if (System.IO.File.Exists(filePath))
            {
                //try to load and deserialize object
                System.IO.StreamReader sr = new System.IO.StreamReader(filePath);
                string serializedData = sr.ReadToEnd();
                sr.Close();

                SerializedObject so = SerializedObject.DeSerialize(serializedData);
                IPersistentObject managed = (IPersistentObject)engine.CreateProxy(so.Type);

                managed.Context = this;
                managed.Id = id;                
                RegisterObject(managed);                
                managed.Initializing = true;
                foreach (DictionaryEntry de in so.Data)
                {
                    string property = (string)de.Key;
                    PropertyInfo pi = so.Type.GetProperty(property);
                    if (de.Value is PersistentId)
                    {
                        managed.SetReference(property, de.Value);
                        managed.SetUnloaded(property, true);
                    }
                    else
                    {
                        pi.SetValue(managed, de.Value, null);
                        managed.SetUnloaded(property, false);

                        if (de.Value is IPersistentList)
                        {
                            IPersistentList list = de.Value as IPersistentList;
                            list.Owner = managed;
                        }
                    }
                }
                managed.Initializing  = false;


                return managed;
            }
            else
            {
                return null; //file didnt exist
            }
            
        }

        public void Delete(object instance)
        {
            IPersistentObject managed = (IPersistentObject)instance;
            uow.DeletedObjects[instance] = instance;
            DropReferences(instance);
            uow.DirtyObjects[instance] = null; //clear object from dirty list
            string filePath = dbPath + @"\" + managed.Id.ToString () +".txt";
            if (System.IO.File.Exists (filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        internal bool IsLoaded(string p)
        {
            return uow.Objects.Contains (p);
        }

        internal static void NotifyUnload(string id)
        {
            Console.WriteLine("Unloading object {0}", id);
        }
    }
}
