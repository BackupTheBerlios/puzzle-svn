namespace Puzzle.NCore.Framework.Text.PatternMatchers
{
	/// <summary>
	/// Pattern matcher that matches case insensitive hex values
	/// </summary>
	public class HexPatternMatcher : PatternMatcherBase
	{
		//perform the match
		public override int Match(string textToMatch, int matchAtIndex)
		{
			int currentIndex = matchAtIndex;
			do
			{
				char currentChar = textToMatch[currentIndex];
				if ((currentChar >= '0' && currentChar <= '9') || (currentChar >= 'a' && currentChar <= 'f') || (currentChar >= 'A' && currentChar <= 'F'))
				{
					//current char is hexchar
				}
				else
				{
					break;
				}
				currentIndex++;
			} while (currentIndex < textToMatch.Length);

			return currentIndex - matchAtIndex;
		}
	}
}