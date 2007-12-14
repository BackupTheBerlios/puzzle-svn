using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace NObjectStore
{
    [Serializable]
    public class InterceptableList<T> : IList<T> , IPersistentList
    {

        private ArrayList list = new ArrayList();
        private object ConvertToId(object item)
        {
            if (item is IPersistentObject)
            {
                string id = ((IPersistentObject)item).Id;
                PersistentId persId = new PersistentId();
                persId.Id = id;
                return persId;
            }
            else
            {
                return item;
            }
        }

        private object ConvertFromId(object item)
        {

            if (item is PersistentId)
            {
                PersistentId id = item as PersistentId;
                object res = owner.Context.Get(id.Id);
                return res;
            }
            else
            {
                return item;
            }
        }

        public int IndexOf(T item)
        {
            object value = ConvertToId(item);

            return list.IndexOf(value);         
        }

        

        public void Insert(int index, T item)
        {
            object value = ConvertToId(item);
            list.Insert(index, value);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                object tmp = ConvertFromId(list[index]);
                return (T)tmp;
            }
            set
            {
                object tmp = ConvertToId(value);
                list[index]=tmp;
            }
        }

        public void Add(T item)
        {
            object tmp = ConvertToId(item);
            list.Add(tmp);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            object tmp = ConvertToId(item);
            return list.Contains(tmp);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return list.IsReadOnly;
            }
        }

        public bool Remove(T item)
        {
            object tmp = ConvertToId(item);
            int count = list.Count;
            list.Remove(item);
            
            bool wasRemoved = list.Count != count;
            return wasRemoved;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #region IPersistentList Members

        [NonSerialized]
        private IPersistentObject owner;        
        public IPersistentObject Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        #endregion
    }
}
