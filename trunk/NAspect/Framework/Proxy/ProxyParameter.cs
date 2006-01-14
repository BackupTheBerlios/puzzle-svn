// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

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