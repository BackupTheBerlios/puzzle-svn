namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathStringValue : IValue
	{
		#region Property VALUE

		private string value;

		public virtual string Value
		{
			get { return value; }
			set { this.value = value; }
		}

		#endregion
	}
}