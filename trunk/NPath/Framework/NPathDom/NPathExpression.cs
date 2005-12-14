namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathExpression : IValue
	{
		#region Property LEFTOPERAND

		private IValue leftOperand;

		public virtual IValue LeftOperand
		{
			get { return leftOperand; }
			set { leftOperand = value; }
		}

		#endregion

		#region Property RIGHTOPERAND

		private IValue rightOperand;

		public virtual IValue RightOperand
		{
			get { return rightOperand; }
			set { rightOperand = value; }
		}

		#endregion

		#region Property OPERATOR

		private string _operator;

		public virtual string Operator
		{
			get { return _operator; }
			set { _operator = value; }
		}

		#endregion

		public virtual int GetOperatorPriority()
		{
			switch (Operator)
			{
				case "or":
					return 0;
				case "and":
					return 1;
				case "=":
				case "!=":
				case ">=":
				case ">":
				case "<":
				case "<=":
					return 2;
				case "mul":
				case "div":
					return 4;
				case "minus":
				case "add":
				case "like":
					return 3;
				default:
					return -1;
			}
		}
	}
}