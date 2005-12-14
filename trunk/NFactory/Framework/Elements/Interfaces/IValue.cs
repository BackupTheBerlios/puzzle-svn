using System;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public interface IValue
	{
		object Invoke(IContainer owner, Type requestedType, InstanceMode instanceMode);
	}
}