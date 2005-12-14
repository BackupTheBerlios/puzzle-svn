// *
// * Copyright (C) 2005 Mats Helander
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

namespace Puzzle.NPersist.Framework.Mapping
{
	public interface IFixable
	{
		void Fixate();

		void UnFixate();

		bool IsFixed();

		bool IsFixed(string memberName);

		object GetFixedValue(string memberName);

		void SetFixedValue(string memberName, object value);
	}
}