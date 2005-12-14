namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathPropertyPathReference
	{
		#region Public Property PropertyPath

		private string propertyPath;

		public string PropertyPath
		{
			get { return this.propertyPath; }
			set { this.propertyPath = value; }
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
	}

	public enum NPathPropertyPathReferenceLocation
	{
		SelectClause,
		WhereClause,
	}
}