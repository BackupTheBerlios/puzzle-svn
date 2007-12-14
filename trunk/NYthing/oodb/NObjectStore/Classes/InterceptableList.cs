using System;
using System.Collections.Generic;
using System.Text;

namespace NObjectStore
{
    public class InterceptableList<T> : IList<T>
    {
        public int IndexOf(T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Insert(int index, T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RemoveAt(int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public T this[int index]
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public void Add(T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Clear()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool Contains(T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Count
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool IsReadOnly
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool Remove(T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
