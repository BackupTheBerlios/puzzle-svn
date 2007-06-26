using System;
using System.Text;

namespace Puzzle.NAspect.Framework
{
	public interface ITypeExtender
	{
        Type Extend(Type baseType);
	}
}
