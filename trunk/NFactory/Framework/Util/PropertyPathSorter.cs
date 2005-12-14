using System.Collections;
using Puzzle.NFactory.Framework.ConfigurationElements;

namespace Puzzle.NFactory.Framework.Util
{
	public class PropertyPathSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			string xx = ((IPropertyConfiguration) x).Name;
			string yy = ((IPropertyConfiguration) y).Name;

			int xcount = xx.Split('.').Length;
			int ycount = yy.Split('.').Length;
			if (xcount != ycount)
				return Comparer.DefaultInvariant.Compare(xcount, ycount);

			return Comparer.DefaultInvariant.Compare(xx, yy);
		}
	}
}