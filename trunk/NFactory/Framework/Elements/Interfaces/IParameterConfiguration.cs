using System;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public interface IParameterConfiguration
	{
		int Index { get; set; }

		IValue Value { get; set; }

		Type Type { get; set; }

		InstanceMode InstanceMode { get; set; }

		object GetValue(IContainer owner);
	}
}