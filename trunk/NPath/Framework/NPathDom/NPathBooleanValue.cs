namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathBooleanValue : IValue
	{
		#region Property VALUE

		private bool value = false;

		public virtual bool Value
		{
			get { return value; }
			set { this.value = value; }
		}

		#endregion
	}
}