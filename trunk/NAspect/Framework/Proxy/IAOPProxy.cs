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
using System.Collections;

namespace Puzzle.NAspect.Framework
{
	public interface IAopProxy
	{
		IDictionary Data { get; }

		object HandleCall(IAopProxy target, string wrappermethodname, IList parameters, Type returntype);
	}
}