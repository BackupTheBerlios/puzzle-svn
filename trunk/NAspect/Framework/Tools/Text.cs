// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Text.RegularExpressions;

namespace Puzzle.NAspect.Framework.Tools
{
	public class Text
	{
		#region IsMatch

		public static bool IsMatch(string text, string pattern)
		{
			string regexpattern = "";
			foreach (char c in pattern)
			{
				if (c == '*')
					regexpattern += @"\w*";
				else if (c == '?')
					regexpattern += @"\w";
				else
					regexpattern += Regex.Escape(c.ToString());
			}
			return Regex.IsMatch(text, regexpattern);
		}

		#endregion
	}
}