namespace Puzzle.NCore.Framework.Text.PatternMatchers
{
	/// <summary>
	/// Pattern matcher that matches culture invariant integer values
	/// </summary>
	public class IntPatternMatcher : PatternMatcherBase
	{
		//perform the match
		public override int Match(string textToMatch, int matchAtIndex)
		{
			int currentIndex = matchAtIndex;
			do
			{
				char currentChar = textToMatch[currentIndex];
				if (currentChar >= '0' && currentChar <= '9')
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

//		public override string[] DefaultPrefixes
//		{
//			get
//			{
//				return new string[] {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
//			}
//		}
	}
}