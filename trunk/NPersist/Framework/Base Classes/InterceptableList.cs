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

namespace Puzzle.NPersist.Framework.BaseClasses
{
	public class InterceptableList : ArrayList , IInterceptableList
	{
		private IListInterceptor interceptor = new ListInterceptor();

		public InterceptableList() : base()
		{
			interceptor.List = this;
		}

		public InterceptableList(IInterceptable interceptable, string propertyName) : this()
		{
			Interceptable = interceptable;
			PropertyName = propertyName;
		}

		public IListInterceptor Interceptor
		{
			get {return interceptor; }
		}

		public virtual IInterceptable Interceptable
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
		public override int Add(object value)
		{
			interceptor.BeforeCall() ;
			int ret = base.Add(value);
			interceptor.AfterCall() ;
			
			return ret;
		}

		public override void AddRange(ICollection c)
		{
			interceptor.BeforeCall() ;
			base.AddRange(c);
			interceptor.AfterCall() ;
		}

		public override void Clear()
		{
			interceptor.BeforeCall() ;
			base.Clear();
			interceptor.AfterCall() ;
		}

		public override void Insert(int index, object value)
		{
			interceptor.BeforeCall() ;
			base.Insert(index, value);
			interceptor.AfterCall() ;
		}

		public override void InsertRange(int index, ICollection c)
		{
			interceptor.BeforeCall() ;
			base.InsertRange(index, c);
			interceptor.AfterCall() ;
		}

		public override IEnumerator GetEnumerator()
		{
			interceptor.BeforeRead() ;
			return base.GetEnumerator ();
		}

		public override IEnumerator GetEnumerator(int index, int count)
		{
			interceptor.BeforeRead() ;
			return base.GetEnumerator (index, count);
		}

		public override bool Contains(object item)
		{
			interceptor.BeforeRead() ;
			return base.Contains (item);
		}

		public override void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			interceptor.BeforeRead() ;
			base.CopyTo (index, array, arrayIndex, count);
		}

		public override void CopyTo(Array array)
		{
			interceptor.BeforeRead() ;
			base.CopyTo (array);
		}

		public override void CopyTo(Array array, int arrayIndex)
		{
			interceptor.BeforeRead() ;
			base.CopyTo (array, arrayIndex);
		}

		public override ArrayList GetRange(int index, int count)
		{
			interceptor.BeforeRead() ;
			return base.GetRange (index, count);
		}

		public override object[] ToArray()
		{
			interceptor.BeforeRead() ;
			return base.ToArray ();
		}

		public override Array ToArray(Type type)
		{
			interceptor.BeforeRead() ;
			return base.ToArray (type);
		}


		public override object this[int index]
		{
			get { return base[index]; }
			set { base[index] = value; }
		}

		public override void Remove(object obj)
		{
			interceptor.BeforeCall() ;
			base.Remove(obj);
			interceptor.AfterCall() ;
		}

		public override void RemoveAt(int index)
		{
			interceptor.BeforeCall() ;
			base.RemoveAt(index);
			interceptor.AfterCall() ;
		}

		public override void RemoveRange(int index, int count)
		{
			interceptor.BeforeCall() ;
			base.RemoveRange(index, count);
			interceptor.AfterCall() ;
		}

		public override void SetRange(int index, ICollection c)
		{
			interceptor.BeforeCall() ;
			base.SetRange(index, c);
			interceptor.AfterCall() ;
		}

		public override int Count
		{
			get
			{
				int count = 0;
				if (!interceptor.BeforeCount(ref count))
					count = base.Count;
				interceptor.AfterCount(ref count) ;
				return count;
			}
		}

	}
}
