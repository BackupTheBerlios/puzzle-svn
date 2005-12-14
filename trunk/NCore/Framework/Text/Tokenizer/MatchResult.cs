namespace Puzzle.NCore.Framework.Text
{
	public struct MatchResult
	{
		public bool Found;
		public object Tag;
		public int Index;
		public int Length;
		public string Text;

		public static MatchResult NoMatch
		{
			get
			{
				MatchResult result = new MatchResult();
				result.Found = false;
				return result;
			}
		}

		public override string ToString()
		{
			if (this.Found == false)
				return "no match"; // do not localize
			else if (Tag != null)
				return Tag.ToString() + "  " + this.Index.ToString() + "  " + this.Length.ToString();
			else
				return "MatchResult";
		}

		public string GetText()
		{
			if (Text != null)
				return Text.Substring(Index, Length);
			else
				return "";
		}
	}
}