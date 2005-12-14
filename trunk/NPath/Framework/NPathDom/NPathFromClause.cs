using System.Collections;

namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathFromClause
	{
		#region Property CLASSES

		private IList classes;

		public virtual IList Classes
		{
			get { return classes; }
			set { classes = value; }
		}

		#endregion

		public NPathFromClause()
		{
			classes = new ArrayList();
		}
	}
}