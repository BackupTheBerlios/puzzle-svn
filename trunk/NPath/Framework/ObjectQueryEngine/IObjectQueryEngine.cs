using System.Collections;
using System.Data;
using Puzzle.NPath.Framework.CodeDom;

namespace Puzzle.NPath.Framework
{
	/// <summary>
	/// Summary description for ObjectQueryEngine.
	/// </summary>
	public interface IObjectQueryEngine
	{
		IList GetObjectsByNPath(string npath, IList sourceList);
		DataTable GetDataTableByNPath(string npath, IList sourceList);

		IList GetObjectsByNPath(string npath, IList sourceList, IList parameters);
		DataTable GetDataTableByNPath(string npath, IList sourceList, IList parameters);

		IList GetObjects(NPathSelectQuery query, IList sourceList);
		DataTable GetDataTable(NPathSelectQuery query, IList sourceList);

		object EvalValue(object item, object expression);

		IObjectQueryEngineHelper ObjectQueryEngineHelper { get; set; }
	}
}