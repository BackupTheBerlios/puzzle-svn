namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathSelectField
	{
		#region Property EXPRESSION

		private IValue expression;

		public virtual IValue Expression
		{
			get { return expression; }
			set { expression = value; }
		}

		#endregion

		#region Property ALIAS

		private string alias;

		public virtual string Alias
		{
			get { return alias; }
			set { alias = value; }
		}

		#endregion
	}
}