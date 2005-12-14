namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathDecimalValue : IValue
	{
		#region Property VALUE

		private double value;

		public virtual double Value
		{
			get { return value; }
			set { this.value = value; }
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