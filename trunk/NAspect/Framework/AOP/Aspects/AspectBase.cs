using System;
using System.Collections;

namespace Puzzle.NAspect.Framework.Aop
{
	public abstract class AspectBase : IAspect
	{
		private string name;
		private IList mixins = new ArrayList();
		private IList pointcuts = new ArrayList();

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public abstract bool IsMatch(Type type);


		public IList Mixins
		{
			get { return mixins; }
			set { mixins = value; }
		}

		public IList Pointcuts
		{
			get { return pointcuts; }
			set { pointcuts = value; }
		}

	}
}