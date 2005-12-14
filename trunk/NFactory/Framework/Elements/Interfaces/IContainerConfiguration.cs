namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public interface IContainerConfiguration
	{
		void AddObjectConfiguration(IObjectConfiguration config);
		void AddListConfiguration(IListConfiguration config);
		IListConfiguration GetListConfiguration(string name);
		IObjectConfiguration GetObjectConfiguration(string name);
		void AddFactoryConfiguration(IFactoryConfiguration config);
		IFactoryConfiguration GetFactoryConfiguration(string name);

		string Name { get; set; }
	}
}