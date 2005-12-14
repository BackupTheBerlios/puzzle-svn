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