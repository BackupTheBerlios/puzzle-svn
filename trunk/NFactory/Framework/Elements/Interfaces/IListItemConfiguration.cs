using System;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public interface IListItemConfiguration
	{
		IValue Value { get; set; }

		Type Type { get; set; }

		InstanceMode InstanceMode { get; set; }

		object GetValue(IContainer owner);
	}
}