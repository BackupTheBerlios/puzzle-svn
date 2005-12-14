using System.Collections;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public class ContainerConfiguration : IContainerConfiguration
	{
		private Hashtable objectConfigurations = new Hashtable();
		private Hashtable listConfigurations = new Hashtable();
		private Hashtable factoryConfigurations = new Hashtable();

		public void AddObjectConfiguration(IObjectConfiguration config)
		{
			this.objectConfigurations[config.Name] = config;
		}

		public void AddListConfiguration(IListConfiguration config)
		{
			this.listConfigurations[config.Name] = config;
		}

		public IListConfiguration GetListConfiguration(string name)
		{
			return listConfigurations[name] as IListConfiguration;
		}

		public IObjectConfiguration GetObjectConfiguration(string name)
		{
			return objectConfigurations[name] as IObjectConfiguration;
		}

		public void AddFactoryConfiguration(IFactoryConfiguration config)
		{
			this.factoryConfigurations[config.Name] = config;
		}

		public IFactoryConfiguration GetFactoryConfiguration(string name)
		{
			return factoryConfigurations[name] as IFactoryConfiguration;
		}

		#region Public Property Name

		private string name;

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		#endregion
	}
}