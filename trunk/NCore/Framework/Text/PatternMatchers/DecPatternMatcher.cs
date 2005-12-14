namespace Puzzle.NCore.Framework.Text.PatternMatchers
{
	/// <summary>
	/// Pattern matcher that matches culture invariant decimal values
	/// </summary>
	public class DecPatternMatcher : PatternMatcherBase
	{
		//perform the match
		public override int Match(string textToMatch, int matchAtIndex)
		{
			//	matchAtIndex --;
			int currentIndex = matchAtIndex;
			bool comma = false;
			do
			{
				char currentChar = textToMatch[currentIndex];
				if (currentChar >= '0' && currentChar <= '9')
				{
					//current char is hexchar
				}
				else if (currentChar == '.' && comma == false)
				{
					comma = true;
				}
				else
				{
					break;
				}
				currentIndex++;
			} while (currentIndex < textToMatch.Length);

			return currentIndex - matchAtIndex;
		}

//		//patterns that trigger this matcher
//		public override string[] DefaultPrefixes
//		{
//			get
//			{
//				return new string[] {};//"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "."};
//			}
//		}
	}
}