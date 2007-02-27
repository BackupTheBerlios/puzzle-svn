using System;
using System.Collections;
using System.Reflection;
using Puzzle.NAspect.Framework;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public interface IObjectConfiguration : IValue
	{
		ArrayList PropertyConfigurations { get; }

		ArrayList CtorParameterConfigurations { get; }

		string Name { get; set; }

		Type Type { get; set; }

		ConstructorInfo Constructor { get; set; }

		InstanceMode InstanceMode { get; set; }

		IValue InstanceValue { get; set; }

        IEngine AopEngine { get;set;}
		
		object CreateObject(IContainer owner);
	}
}