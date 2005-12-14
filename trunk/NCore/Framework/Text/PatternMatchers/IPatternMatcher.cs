namespace Puzzle.NCore.Framework.Text.PatternMatchers
{
	/// <summary>
	/// 
	/// </summary>
	public interface IPatternMatcher
	{
		int Match(string textToMatch, int matchAtIndex);

		string[] DefaultPrefixes { get; }
	}
}