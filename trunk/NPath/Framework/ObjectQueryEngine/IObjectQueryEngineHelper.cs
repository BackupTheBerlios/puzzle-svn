using Puzzle.NPath.Framework.CodeDom;

namespace Puzzle.NPath.Framework
{
	public interface IObjectQueryEngineHelper
	{
		void ExpandWildcards(NPathSelectQuery query);
		bool GetNullValueStatus(object target, string property);
		object EvalParameter(object item, NPathParameter parameter);
	}
}