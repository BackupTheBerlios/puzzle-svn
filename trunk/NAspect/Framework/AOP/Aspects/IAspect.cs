using System;
using System.Collections;

namespace Puzzle.NAspect.Framework.Aop
{
	public interface IAspect
	{
		string Name { get; set; }
		bool IsMatch(Type type);
		IList Mixins { get; }
		IList Pointcuts { get; }
	}
}