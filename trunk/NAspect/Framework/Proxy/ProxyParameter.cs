using System;

namespace Puzzle.NAspect.Framework
{
	public enum ParameterType
	{
		ByVal,
		Ref,
		Out
	}

	public class InterceptedParameter
	{
		public readonly string Name;
		public readonly int Index;
		public readonly Type Type;
		public readonly ParameterType ParameterType;
		public object Value;

		#region InterceptedParameter

		public InterceptedParameter(string name, int index, Type type, object value, ParameterType parametertype)
		{
			Name = name;
			Index = index;
			Type = type;
			Value = value;
			ParameterType = parametertype;
		}

		#endregion
	}
}