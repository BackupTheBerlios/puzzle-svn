using System;
using System.ComponentModel;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public interface IValueConfiguration : IValue
	{
		object Value { get; set; }

		TypeConverter TypeConverter { get; set; }
		
	}
}