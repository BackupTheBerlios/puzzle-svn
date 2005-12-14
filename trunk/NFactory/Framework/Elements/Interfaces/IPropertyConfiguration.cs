using System;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public interface IPropertyConfiguration
	{
		string Name { get; set; }

		ListAction ListAction { get; set; }

		IValue Value { get; set; }

		Type Type { get; set; }

		InstanceMode InstanceMode { get; set; }

		object GetValue(IContainer owner);
	}
}