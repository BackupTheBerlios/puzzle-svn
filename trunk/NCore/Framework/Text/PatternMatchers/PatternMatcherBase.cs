namespace Puzzle.NCore.Framework.Text.PatternMatchers
{
	/// <summary>
	/// Base implementation for pattern matchers
	/// </summary>
	public class PatternMatcherBase : IPatternMatcher
	{
		public virtual int Match(string textToMatch, int matchAtIndex)
		{
			return 0;
		}

		public virtual string[] DefaultPrefixes
		{
			get { return null; }
		}
	}
}