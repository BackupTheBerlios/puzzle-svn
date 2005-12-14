// *
// * Copyright (C) 2005 Roger Johansson
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Collections;

namespace Puzzle.NCore.Framework.Collections
{
    public abstract class CollectionBase : System.Collections.CollectionBase, IList
	{
		protected CollectionBase()
		{
		}

		#region Events

		public event CollectionEventHandler ItemAdded;

		protected virtual void OnItemAdded(int index, object item)
		{
			if (this.ItemAdded != null)
			{
				CollectionEventArgs e = new CollectionEventArgs(item, index);

				this.ItemAdded(this, e);
			}
		}

		public event CollectionEventHandler ItemRemoved;

		protected virtual void OnItemRemoved(int index, object item)
		{
			if (this.ItemRemoved != null)
			{
				CollectionEventArgs e = new CollectionEventArgs(item, index);

				this.ItemRemoved(this, e);
			}
		}

		public event EventHandler ItemsCleared;

		protected virtual void OnItemsCleared()
		{
			if (this.ItemsCleared != null)
				this.ItemsCleared(this, EventArgs.Empty);
		}

		#endregion

		#region Overrides

		protected override void OnClearComplete()
		{
			base.OnClearComplete();
			this.OnItemsCleared();
		}

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			this.OnItemRemoved(index, value);
		}

		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			this.OnItemAdded(index, value);
		}

		#endregion
	}
}