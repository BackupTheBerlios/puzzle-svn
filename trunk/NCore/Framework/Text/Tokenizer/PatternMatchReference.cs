namespace Puzzle.NCore.Framework.Text.PatternMatchers
{
	public class PatternMatchReference
	{
		public PatternMatchReference NextSibling = null;
		public IPatternMatcher Matcher = null;
		public object Tag = null;


		public PatternMatchReference(IPatternMatcher matcher)
		{
			this.Matcher = matcher;
		}
	}
}