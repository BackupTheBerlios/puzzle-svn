using System.Collections;

namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathOrderByClause
	{
		#region Property SORTPROPERTIES

		private IList sortProperties;

		public virtual IList SortProperties
		{
			get { return sortProperties; }
			set { sortProperties = value; }
		}

		#endregion

		public NPathOrderByClause()
		{
			sortProperties = new ArrayList();
		}
	}

	public class SortProperty
	{
		public IValue Expression;
		public SortDirection Direction = SortDirection.Asc;
	}

	public enum SortDirection
	{
		Asc,
		Desc,
	}
}