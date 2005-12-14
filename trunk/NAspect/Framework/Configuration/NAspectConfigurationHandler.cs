using System.Configuration;
using System.Xml;

namespace Puzzle.NAspect.Framework.Configuration
{
	public class NAspectConfigurationHandler : IConfigurationSectionHandler
	{
		public NAspectConfigurationHandler()
		{
		}

		public object Create(object parent, object configContext, XmlNode section)
		{
			return section.SelectSingleNode("configuration");
		}

	}
}