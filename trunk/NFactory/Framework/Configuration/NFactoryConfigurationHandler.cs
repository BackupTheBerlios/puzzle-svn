using System.Configuration;
using System.Xml;

namespace Puzzle.NFactory.Framework.Configuration
{
	public class NFactoryConfigurationHandler : IConfigurationSectionHandler
	{
		public NFactoryConfigurationHandler()
		{
		}

		public object Create(object parent, object configContext, XmlNode section)
		{
			return section.SelectSingleNode("configuration");
		}

	}
}