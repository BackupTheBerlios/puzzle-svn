using System.Collections;

namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathInExpression : IValue
	{
		public IValue TestExpression;
		public IList Values = new ArrayList();
	}
}