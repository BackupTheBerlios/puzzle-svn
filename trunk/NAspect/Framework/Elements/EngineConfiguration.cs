using System.Collections;

namespace Puzzle.NAspect.Framework.ConfigurationElements
{
	public class EngineConfiguration
	{
		#region Public Property Aspects

		private IList aspects;

		public IList Aspects
		{
			get { return this.aspects; }
			set { this.aspects = value; }
		}

		#endregion

		#region Public Property Name

		private string name;

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		#endregion

		public EngineConfiguration()
		{
			aspects = new ArrayList();
		}
	}
}