using System;
using System.Collections;

namespace Puzzle.NAspect.Framework
{
	public interface IAopProxy
	{
		IDictionary Data { get; }

		object HandleCall(IAopProxy target, string wrappermethodname, IList parameters, Type returntype);
	}
}