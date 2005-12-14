using System;

namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathDateTimeValue : IValue
	{
		#region Property VALUE

		private DateTime value;

		public virtual DateTime Value
		{
			get { return value; }
			set { this.value = value; }
		}

		#endregion
	}
}