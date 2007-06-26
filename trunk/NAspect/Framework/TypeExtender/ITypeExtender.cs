using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Framework
{
	public interface ITypeExtender
	{
        Type Extend(Type baseType);
	}
}
