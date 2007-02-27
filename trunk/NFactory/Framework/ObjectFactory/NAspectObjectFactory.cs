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


		public object CreateInstance(IEngine specificEngine,Type type, params object[] args)
		{
            if (specificEngine != null)
            {
                return specificEngine.CreateProxy(type, args);
            }
            else
            {
                return engine.CreateProxy(type, args);
            }
		}
	}
}