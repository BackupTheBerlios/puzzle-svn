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
	/// Summary description for PropertyView.
	/// </summary>
	public class PropertyView
	{
		public PropertyView(ClassView classView)
		{
			this.classView = classView;
			classView.PropertyViews.Add(this);
		}

		#region Property  ClassView
		
		private ClassView classView;
		
		public ClassView ClassView
		{
			get { return this.classView; }
			set { this.classView = value; }
		}
		
		#endregion

		#region Property  Name
		
		private string name = "";
		
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}
		
		#endregion

		#region Property  DisplayName
		
		private string displayName = "";
		
		public string DisplayName
		{
			get { return this.displayName; }
			set { this.displayName = value; }
		}
		
		#endregion

		#region Property  Path
		
		private string path = "";
		
		public string Path
		{
			get
			{
				if (this.path.Length > 0)
					return this.path;
				return this.name;
			}
			set { this.path = value; }
		}
		
		#endregion

		#region Property  Description
		
		private string description = "";
		
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}
		
		#endregion
	
		#region Property  Category
		
		private string category = "";
		
		public string Category
		{
			get { return this.category; }
			set { this.category = value; }
		}
		
		#endregion
		
		#region Property  IsReadOnly
		
		private bool isReadOnly = false;
		
		public bool IsReadOnly
		{
			get { return this.isReadOnly; }
			set { this.isReadOnly = value; }
		}
		
		#endregion

		#region Property  DefaultValue
		
		private string defaultValue = "";
		
		public string DefaultValue
		{
			get { return this.defaultValue; }
			set { this.defaultValue = value; }
		}
		
		#endregion
	}
}
