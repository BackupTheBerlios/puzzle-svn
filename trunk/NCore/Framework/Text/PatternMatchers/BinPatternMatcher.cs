namespace Puzzle.NCore.Framework.Text.PatternMatchers
{
	/// <summary>
	/// Pattern matcher that matches binary tokens
	/// </summary>
	public class BinPatternMatcher : PatternMatcherBase
	{
		//perform the match
		public override int Match(string textToMatch, int matchAtIndex)
		{
			int currentIndex = matchAtIndex;
			do
			{
				char currentChar = textToMatch[currentIndex];
				if (currentChar == '0' || currentChar == '1')
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

		//patterns that trigger this matcher
		public override string[] DefaultPrefixes
		{
			get { return new string[] {"0", "1"}; }
		}
	}
}