namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathParameter : IValue
	{
		#region Property VALUE

		private object value;

		public virtual object Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		#endregion
	}
}