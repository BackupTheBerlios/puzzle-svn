// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Collections;
using Puzzle.NPersist.Framework.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace Puzzle.NPersist.Framework.BaseClasses
{
	class InterceptableGenericsList<T> : IList<T> , IInterceptableList , IBindingList , ICancelAddNew
	{
        private List<T> list = new List<T>();

        public virtual int IndexOf(T item)
        {
            return list.IndexOf (item);
        }

        public virtual void Insert(int index, T item)
        {
            ((IList)this).Insert (index,item);            
        }

        public virtual void RemoveAt(int index)
        {
            interceptor.BeforeCall() ;
			this.list.RemoveAt (index);
			interceptor.AfterCall() ;
            OnListChanged(ListChangedType.ItemDeleted,index);
        }

        public virtual T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                ((IList)this)[index]=value;
            }
        }

        public virtual void Add(T item)
        {
            ((IList)this).Add (item);
        }

        public virtual void Clear()
        {
            ((IList)this).Clear ();
        }

        public virtual bool Contains(T item)
        {
            return list.Contains (item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo (array,arrayIndex);
        }

        public virtual int Count
        {
            get { return list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(T item)
        {
         
            int oldCount = this.Count;
            ((IList)this).Remove(item);
            return oldCount != this.Count;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return list.GetEnumerator ();
        }

        public IEnumerator GetEnumerator()
        {
             return list.GetEnumerator ();
        }

        #region IList Members

        int IList.Add(object value)
        {
            interceptor.BeforeCall() ;
			list.Add ((T)value);
			interceptor.AfterCall() ;
            int index = this.Count-1;
			this.OnListChanged (ListChangedType.ItemAdded ,index);
            return index;
        }

        bool IList.Contains(object value)
        {
            if (value == null)
                return false;

            if (typeof(T).IsAssignableFrom (value.GetType()))
            {
                //TODO: make it return if datatype is mismatch
                return list.Contains ((T)value);           
            }
            else
            {
                return false;
            }
        }

        int IList.IndexOf(object value)
        {
            return list.IndexOf ((T)value);
        }

        void IList.Insert(int index, object value)
        {
            interceptor.BeforeCall() ;
			list.Insert (index,(T)value);
			interceptor.AfterCall() ;
            this.OnListChanged (ListChangedType.ItemAdded ,index);
        }

        bool IList.IsFixedSize
        {
            get
            {
            return false;
            }
        }

        void IList.Remove(object value)
        {
            int index = list.IndexOf ((T)value);   

            //the item does not exist in the list
            if (index == -1)
                return;

            interceptor.BeforeCall() ;
			list.RemoveAt (index);
			interceptor.AfterCall() ;
            this.OnListChanged (ListChangedType.ItemDeleted ,index);
        }

        object IList.this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index]=(T)value;
                this.OnListChanged (ListChangedType.ItemChanged ,index);
            }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            ((IList)list).CopyTo (array,index);
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return list; }
        }

        #endregion

        public InterceptableGenericsList ()
        {
            this.interceptor.List = this;
        }

        public InterceptableGenericsList(IInterceptable interceptable, string propertyName) : this()
		{
			Interceptable = interceptable;
			PropertyName = propertyName;
		}

        #region IInterceptableListState Members

        private IListInterceptor interceptor = new ListInterceptor();
        public IListInterceptor Interceptor
        {
            get {return interceptor; }
        }

        public IInterceptable Interceptable
        {
            get { return interceptor.Interceptable; }
			set { interceptor.Interceptable = value; }
        }

        public string PropertyName
        {
            get { return interceptor.PropertyName; }
			set { interceptor.PropertyName = value; }
        }

        public bool MuteNotify
        {
            get { return interceptor.MuteNotify; }
			set { interceptor.MuteNotify = value; }
        }

        #endregion

        public IContext Context
        {
            get
            {
                return this.interceptor.Interceptable.GetInterceptor ().Context;
            }
        }

        #region IBindingList Members

        private void OnListChanged(ListChangedType type, int index)
        {
              if (this.ListChanged != null)
              {
                    this.ListChanged(this, new ListChangedEventArgs(type, index));
              }
        }

        public void AddIndex(PropertyDescriptor property)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public object AddNew()
        {
           T entity = this.Context.CreateObject<T>();
            this.Add (entity);
            return entity;
        }

        public bool AllowEdit
        {
            get 
            { 
                return true; 
            }
        }

        public bool AllowNew
        {
            get 
            { 
                return true; 
            }
        }

        public bool AllowRemove
        {
            get 
            { 
                return true; 
            }
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Find(PropertyDescriptor property, object key)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool IsSorted
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public event ListChangedEventHandler ListChanged;

        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RemoveSort()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public ListSortDirection SortDirection
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public PropertyDescriptor SortProperty
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool SupportsChangeNotification
        {
              get
              {
                    return false;
              }
        }

        public bool SupportsSearching
        {
              get
              {
                    return false;
              }
        }

        public bool SupportsSorting
        {
            get
            {
                return false;
            }

        }

        #endregion

        #region ICancelAddNew Members

        public void CancelNew(int itemIndex)
        {
            T entity = this[itemIndex];
            this.RemoveAt (itemIndex);

            this.Context.DeleteObject (entity);
        }

        public void EndNew(int itemIndex)
        {
            
        }

        #endregion
    }
}
