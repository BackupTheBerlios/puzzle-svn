using System.Reflection;

namespace Puzzle.NAspect.Framework
{
	/// <summary>
	/// Summary description for Tools.
	/// </summary>
	public class AopTools
	{
		#region GetMethodSignature

		public static string GetMethodSignature(MethodBase method)
		{
			return method.ToString();
		}

		#endregion
	}
}