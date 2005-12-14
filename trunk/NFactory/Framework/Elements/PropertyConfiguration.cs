namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public class PropertyConfiguration : ElementConfiguration, IPropertyConfiguration
	{
		#region Public Property Name

		private string name;

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		#endregion

		#region Public Property ListAction

		private ListAction listAction = ListAction.Replace;

		public ListAction ListAction
		{
			get { return this.listAction; }
			set { this.listAction = value; }
		}

		#endregion
	}

	public enum ListAction
	{
		Replace,
		Add,
	}
}