using System;

namespace Puzzle.NFactory.Framework
{
	public interface IObjectFactory
	{
		object CreateInstance(Type type, params object[] ctorParams);
	}
}