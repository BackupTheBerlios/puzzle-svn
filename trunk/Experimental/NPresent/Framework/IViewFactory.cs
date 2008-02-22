using System;
using System.Collections;
using Puzzle.NPresent.Framework.ViewModel;

namespace Puzzle.NPresent.Framework
{
	/// <summary>
	/// Summary description for IViewFactory.
	/// </summary>
	public interface IViewFactory
	{
		object CreateView(object obj, string viewName);

		object CreateView(object obj, ClassView classView);

		IList CreateViewList(IList list, string viewName);

		IList CreateViewList(IList list, ClassView classView);

		void LoadView(string viewPath);

		ClassView GetClassView(string viewName);
	}
}
