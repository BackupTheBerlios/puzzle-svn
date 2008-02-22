using System;

namespace Puzzle.NPresent.Framework.ViewModel
{
	/// <summary>
	/// Summary description for TreeNodeView.
	/// </summary>
	public class TreeNodeView
	{
		public TreeNodeView(ClassView classView)
		{
			this.classView = classView;
		}

		#region Property  ClassView
		
		private ClassView classView;
		
		public ClassView ClassView
		{
			get { return this.classView; }
			set { this.classView = value; }
		}
		
		#endregion

		#region Property  Text
		
		private string text = "";
		
		public string Text
		{
			get { return this.text; }
			set { this.text = value; }
		}
		
		#endregion

		#region Property  ImageIndex
		
		private int imageIndex = 0;
		
		public int ImageIndex
		{
			get { return this.imageIndex; }
			set { this.imageIndex = value; }
		}
		
		#endregion

		#region Property  SelectedImageIndex
		
		private int selectedImageIndex = 0;
		
		public int SelectedImageIndex
		{
			get { return this.selectedImageIndex; }
			set { this.selectedImageIndex = value; }
		}
		
		#endregion
	
	}
}
