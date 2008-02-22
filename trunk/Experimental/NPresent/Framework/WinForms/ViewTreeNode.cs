using System;
using System.Windows.Forms;
using Puzzle.NPresent.Framework.ViewModel;
using Puzzle.NPresent.Framework.Wrapping;

namespace Puzzle.NPresent.Framework.WinForms
{
	/// <summary>
	/// Summary description for ViewTreeNode.
	/// </summary>
	public class ViewTreeNode : TreeNode, IViewObject
	{
		public ViewTreeNode(object obj, ClassView classView)
		{
			this.obj = obj;
			this.classView = classView;
			SetupFromView();
		}

		public ViewTreeNode(IViewObject obj)
		{
			this.obj = obj;
			this.classView = obj.GetClassView();
			SetupFromView();
		}

		private object obj;
		private ClassView classView = null;

		public object GetObject()
		{
			return obj;			
		}

		public ClassView GetClassView()
		{
			return this.classView;			
		}

		private void SetupFromView()
		{
			if (this.classView == null)
				return;


		}

	}
}
