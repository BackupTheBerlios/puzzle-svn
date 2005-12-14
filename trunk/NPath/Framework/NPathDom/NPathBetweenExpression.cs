namespace Puzzle.NPath.Framework.CodeDom
{
	/// <summary>
	/// Summary description for Between.
	/// </summary>
	public class NPathBetweenExpression : IValue
	{
		public IValue TestExpression;
		public IValue FromExpression;
		public IValue EndExpression;
	}
}