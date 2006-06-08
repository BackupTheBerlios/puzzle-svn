using System;
using System.Collections.Generic;
using System.Text;
using System.Query;
using System.Collections;
using Puzzle.NPersist.Framework.Querying;

namespace Puzzle.NPersist.Linq
{
    public class LinqList<T> :  ILinqList<T>
    {
       #region Property IsLoaded
        private bool isLoaded;
        public virtual bool IsLoaded
        {
            get
            {
                return this.isLoaded;
            }
            set
            {
                this.isLoaded = value;
            }
        }
        #endregion

        #region Property IsDirty
        private bool isDirty;
        public virtual bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                this.isDirty = value;
            }
        }
        #endregion

       #region Property Query
       private LinqQuery<T> query;
       public virtual LinqQuery<T> Query
       {
           get
           {
               return this.query;
           }
           set
           {
               this.query = value;
           }
       }
       #endregion


        protected virtual void EnsureLoaded()
        {
            if (IsDirty)
            {
                if (!IsLoaded)
                {
                    NPathQuery npquery = new NPathQuery(query.ToNPath(),typeof(T));
                    innerList.Clear ();
                    query.Context.GetObjectsByNPath (npquery,innerList);
                    IsLoaded = true;
                }
                else
                {
                    NPathQuery npquery = new NPathQuery(query.ToNPath(),typeof(T));
                    List<T> oldList = innerList;
                    IList tmp = query.Context.FilterObjects (innerList,npquery);
                    List<T> newList = new List<T>();
                    foreach (T item in tmp)
                    {
                        newList.Add (item);
                    }
                    this.innerList = newList;
                }
                IsDirty = false;
            }
        }


       private List<T> innerList = new List<T>();

       public int IndexOf(T item)
       {
           EnsureLoaded();
           return innerList.IndexOf (item);
       }

       public void Insert(int index, T item)
       {
           EnsureLoaded();
           innerList.Insert (index,item);
       }

       public void RemoveAt(int index)
       {
           EnsureLoaded();
           innerList.RemoveAt(index);
       }

       public T this[int index]
       {
           get
           {
               EnsureLoaded();
               return innerList[index];
           }
           set
           {
               EnsureLoaded();
               innerList[index] = value;
           }
       }

       public void Add(T item)
       {
           EnsureLoaded();
           innerList.Add (item);
       }

       public void Clear()
       {
           EnsureLoaded();
           innerList.Clear ();
       }

       public bool Contains(T item)
       {
           EnsureLoaded();
           return innerList.Contains (item);
       }

       public void CopyTo(T[] array, int arrayIndex)
       {
           EnsureLoaded();
           innerList.CopyTo (array,arrayIndex);
       }

       public int Count
       {
           get 
           { 
               EnsureLoaded();
               return innerList.Count; 
           }
       }

       public bool IsReadOnly
       {
           get 
           { 
               EnsureLoaded();
               return false; 
           }
       }

       public bool Remove(T item)
       {
           EnsureLoaded();
           return innerList.Remove (item);
       }

       IEnumerator<T> IEnumerable<T>.GetEnumerator()
       {
           EnsureLoaded();
           return innerList.GetEnumerator ();
       }

       IEnumerator IEnumerable.GetEnumerator()
       {
           EnsureLoaded();
           return innerList.GetEnumerator ();
       }

       public ILinqList<T> Clone()
       {
           LinqList<T> clone = new LinqList<T>();
           
           clone.innerList.AddRange (this.innerList);
           clone.IsDirty = this.IsDirty;
           clone.IsLoaded = this.IsLoaded;
           clone.Query = this.Query.Clone();

           return clone;
       }
   }
}
