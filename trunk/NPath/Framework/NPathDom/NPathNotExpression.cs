namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathNotExpression : IValue
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