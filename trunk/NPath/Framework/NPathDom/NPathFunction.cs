namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathFunction : IValue
	{
		#region Property DISTINCT

		private bool distinct;

		public virtual bool Distinct
		{
			get { return distinct; }
			set { distinct = value; }
		}

		#endregion

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