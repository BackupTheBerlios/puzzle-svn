// *
// * Copyright (C) 2005 Mats Helander : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

namespace Puzzle.NPersist.Framework.Persistence
{
	public class InverseManagerEntry : IInverseManagerEntry
	{
		private object m_InverseObject;
		private string m_PropertyName;

		public InverseManagerEntry() : base()
		{
		}

		public InverseManagerEntry(object inverse, string propertyName) : base()
		{
			m_InverseObject = inverse;
			m_PropertyName = propertyName;
		}

		public virtual object InverseObject
		{
			get { return m_InverseObject; }
			set { m_InverseObject = value; }
		}

		public virtual string PropertyName
		{
			get { return m_PropertyName; }
			set { m_PropertyName = value; }
		}
	}
}