using System;
using Puzzle.NAspect.Framework;

namespace Puzzle.NFactory.Framework
{
	public interface IObjectFactory
	{
		object CreateInstance(IEngine specificEngine,Type type, params object[] ctorParams);
	}
}