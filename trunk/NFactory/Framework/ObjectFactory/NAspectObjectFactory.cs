using System;
using Puzzle.NAspect.Framework;

namespace Puzzle.NFactory.Framework
{
	public class NAspectObjectFactory : IObjectFactory
	{
		private IEngine engine;

		public NAspectObjectFactory()
		{
			engine = NAspect.Framework.ApplicationContext.Configure();
		}


		public object CreateInstance(Type type, params object[] args)
		{
			return engine.CreateProxy(type, args);
		}
	}
}