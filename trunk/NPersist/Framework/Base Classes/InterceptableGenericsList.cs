// *
// * Copyright (C) 2005 Roger Alsing : http://www.puzzleframework.com
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
    public class InterceptableGenericsList<T> : InterceptableList, IList<T>
	{

        #region IList<T> Members

        public virtual int IndexOf(T item)
        {
            return base.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            base.Insert(index, item);
        }

        public new virtual T this[int index]
        {
            get
            {
                return (T)base[index];
            }
            set
            {
                base[index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members

        public virtual void Add(T item)
        {
            base.Add(item);
        }

        public virtual bool Contains(T item)
        {
            return base.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(T item)
        {
            int oldCount = this.Count;
            base.Remove(item);

            return (oldCount != this.Count);
        }

        #endregion

        #region IEnumerable<T> Members

        public new virtual IEnumerator<T> GetEnumerator()
        {
            foreach (T item in (IEnumerable)this)
            {
                yield return item;
            }
        }

        #endregion
    }
}
