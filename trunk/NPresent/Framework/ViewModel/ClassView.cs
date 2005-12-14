using System.Collections;
// *
// * Copyright (C) 2005 Mats Helander
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

namespace Puzzle.NPresent.Framework.ViewModel
{
	/// <summary>
	/// Summary description for ClassView.
	/// </summary>
	public class ClassView : INamedView
	{
		public ClassView()
		{
		}

		#region Property  Name
		
		private string name = "";
		
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}
		
		#endregion

		#region Property  PropertyViews
		
		private IList propertyViews = new ArrayList() ;
		
		public IList PropertyViews
		{
			get { return this.propertyViews; }
			set { this.propertyViews = value; }
		}
		
		#endregion

		#region Method  GetPropertyView

		public PropertyView GetPropertyView(string name)
		{
			foreach (PropertyView propertyView in this.propertyViews)
			{
				if (propertyView.Name.Equals(name))
				{
					return propertyView;
				}
			}
			return null;
		}

		#endregion

		#region Property  TreeNodeView
		
		private TreeNodeView treeNodeView = null;//new TreeNodeView(this) ;
		
		public TreeNodeView TreeNodeView
		{
			get { return this.treeNodeView; }
			set { this.treeNodeView = value; }
		}
		
		#endregion

	}
}
