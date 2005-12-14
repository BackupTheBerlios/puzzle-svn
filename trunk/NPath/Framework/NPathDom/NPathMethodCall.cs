using System.Collections;

namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathMethodCall : IValue
	{
		#region Public Property PropertyPath

		private NPathIdentifier propertyPath;

		public NPathIdentifier PropertyPath
		{
			get { return this.propertyPath; }
			set { this.propertyPath = value; }
		}

		#endregion

		#region Public Property MethodName

		private string methodName;

		public string MethodName
		{
			get { return this.methodName; }
			set { this.methodName = value; }
		}

		#endregion

		#region Public Property Parameters

		private IList parameters = new ArrayList();

		public IList Parameters
		{
			get { return this.parameters; }
		}

		#endregion
	}
}