namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathParenthesisGroup : IValue
	{
		#region Property EXPRESSION

		private IValue expression;

		public virtual IValue Expression
		{
			get { return expression; }
			set { expression = value; }
		}

		#endregion

		#region Property ISNEGATIVE

		private bool isNegative;

		public virtual bool IsNegative
		{
			get { return isNegative; }
			set { isNegative = value; }
		}

		#endregion
	}
}