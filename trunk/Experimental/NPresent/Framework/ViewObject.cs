using System;
using Puzzle.NPresent.Framework.ViewModel;

namespace Puzzle.NPresent.Framework
{
	/// <summary>
	/// Summary description for ViewObject.
	/// </summary>
	public class ViewObject : IViewObject
	{

		public ViewObject(ClassView classView)
		{
			this.classView = classView;
		}

		private ClassView classView;

		public ClassView GetClassView()
		{
			return classView;
		}
	}
}
