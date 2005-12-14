using System;
using System.Collections;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public interface IListConfiguration : IValue
	{
		string Name { get; set; }

		ArrayList ListItemConfigurations { get; }

	}
}