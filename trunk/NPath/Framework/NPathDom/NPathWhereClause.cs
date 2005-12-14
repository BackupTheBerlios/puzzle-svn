namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathWhereClause
	{
		#region Property EXPRESSION

		private IValue expression;

		public virtual IValue Expression
		{
			get { return expression; }
			set { expression = value; }
		}

		#endregion
	}
}