using System.Collections;

namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathSelectClause
	{
		#region Property SELECTFIELDS

		private IList selectFields;

		public virtual IList SelectFields
		{
			get { return selectFields; }
			set { selectFields = value; }
		}

		#endregion

		private bool distinct;

		public bool Distinct
		{
			get { return this.distinct; }
			set { this.distinct = value; }
		}

		private bool hasTop;

		public bool HasTop
		{
			get { return this.hasTop; }
			set { this.hasTop = value; }
		}

		private long top;

		public long Top
		{
			get { return this.top; }
			set { this.top = value; }
		}

		private bool percent;

		public bool Percent
		{
			get { return this.percent; }
			set { this.percent = value; }
		}

		private bool withTies;

		public bool WithTies
		{
			get { return this.withTies; }
			set { this.withTies = value; }
		}

		public NPathSelectClause()
		{
			selectFields = new ArrayList();
		}
	}
}