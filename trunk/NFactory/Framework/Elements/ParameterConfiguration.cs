namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public class ParameterConfiguration : ElementConfiguration, IParameterConfiguration
	{
		#region Public Property Index

		private int index;

		public int Index
		{
			get { return this.index; }
			set { this.index = value; }
		}

		#endregion		
	}
}