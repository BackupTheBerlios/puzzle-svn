namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathPropertyFilter : IValue
	{
		#region Property PATH

		private string path;

		public virtual string Path
		{
			get { return path; }
			set { path = value; }
		}

		#endregion

		#region Property FILTER

		private NPathBracketGroup filter;

		public virtual NPathBracketGroup Filter
		{
			get { return filter; }
			set { filter = value; }
		}

		#endregion
	}
}