namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathIdentifier : IValue
	{
		#region Property PATH

		private string path;

		public virtual string Path
		{
			get { return path; }
			set
			{
				value = value.Replace("@", ""); //ignore escape chars
				path = value;
				IsWildcard = path.EndsWith("*") || path.EndsWith("¤");
			}
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

		#region Public Property ReferenceLocation

		private NPathPropertyPathReferenceLocation referenceLocation;

		public NPathPropertyPathReferenceLocation ReferenceLocation
		{
			get { return this.referenceLocation; }
			set { this.referenceLocation = value; }
		}

		#endregion

		public bool IsWildcard = false;

	}
}