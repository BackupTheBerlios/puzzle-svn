using System;
using System.Collections;
using System.Reflection;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public interface IFactoryConfiguration : IValue
	{
		ArrayList ParameterConfigurations { get; }

		string Name { get; set; }

		string MethodName { get; set; }

		MethodInfo Method { get; set; }

		Type Type { get; set; }

		IObjectConfiguration Object { get; set; }

	}
}