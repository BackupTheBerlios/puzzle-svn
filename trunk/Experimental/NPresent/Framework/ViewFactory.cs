using System;
using System.Collections;
using System.IO;
using Puzzle.NPresent.Framework.ViewModel;
using Puzzle.NPresent.Framework.Wrapping;

namespace Puzzle.NPresent.Framework
{
	/// <summary>
	/// Summary description for ViewFactory.
	/// </summary>
	public class ViewFactory : IViewFactory
	{
		public ViewFactory()
		{
		}

		public ViewFactory(string viewPath)
		{
			LoadView(viewPath);
		}

		private IWrapper wrapper = new Wrapper();

		public void LoadView(string viewPath)
		{
			ViewSerializer serializer = new ViewSerializer() ;
			IList view = serializer.Load(viewPath);
			foreach (INamedView namedView in view)
			{
				INamedView test = (INamedView) namedViews[namedView.Name];
				if (test != null)
					throw new Exception("A view with the name " + namedView.Name + " is already loaded!");
				namedViews[namedView.Name] = namedView;
			}			
		}

		#region Property  NamedViews
		
		private Hashtable namedViews = new Hashtable() ;
		
		public Hashtable NamedViews
		{
			get { return this.namedViews; }
			set { this.namedViews = value; }
		}
		
		#endregion

		public object CreateView(object obj, string viewName)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (viewName == null)
				throw new ArgumentNullException("viewName");
			if (viewName.Length < 1)
				throw new ArgumentException("viewName must not be empty");

			ClassView classView = GetClassView(viewName);
			return CreateView(obj, classView);
		}

		public object CreateView(object obj, ClassView classView)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (classView == null)
				throw new ArgumentNullException("classView");

			return wrapper.WrapObject(obj, classView);
		}

		public IList CreateViewList(IList list, string viewName)
		{
			if (list == null)
				throw new ArgumentNullException("list");
			if (viewName == null)
				throw new ArgumentNullException("viewName");
			if (viewName.Length < 1)
				throw new ArgumentException("viewName must not be empty");

			ClassView classView = GetClassView(viewName);
			return CreateViewList(list, classView);
		}

		public IList CreateViewList(IList list, ClassView classView)
		{
			if (list == null)
				throw new ArgumentNullException("list");
			if (classView == null)
				throw new ArgumentNullException("classView");

			IList wrappedList = (IList) Activator.CreateInstance(list.GetType());

			foreach (object obj in list)
			{
				wrappedList.Add(wrapper.WrapObject(obj, classView));
			}

			return wrappedList;
		}

		
		public ClassView GetClassView(string viewName)
		{
			if (viewName == null)
				throw new ArgumentNullException("viewName");
			if (viewName.Length < 1)
				throw new ArgumentException("viewName must not be empty");

			INamedView namedView = GetNamedView(viewName);

			ClassView classView = namedView as ClassView;
			
			if (classView == null)
				throw new Exception("View with name " + viewName + " is not a class view!");
			
			return classView;
		}

		private INamedView GetNamedView(string viewName)
		{
			if (viewName == null)
				throw new ArgumentNullException("viewName");

			INamedView namedView = (ClassView) this.namedViews[viewName];
	
			if (namedView == null)
				throw new Exception("View with name " + viewName + " not loaded!");

			return namedView;
		}

	}
}
