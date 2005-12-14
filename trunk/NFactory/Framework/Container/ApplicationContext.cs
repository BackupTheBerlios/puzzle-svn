using System.Collections;
using System.Configuration;
using System.Xml;
using Puzzle.NFactory.Framework.ConfigurationElements;

namespace Puzzle.NFactory.Framework
{
	public class ApplicationContext
	{
		private static volatile Hashtable configurations = new Hashtable();

		public static IContainer Configure()
		{
			if (configurations.ContainsKey("app.config"))
			{
				IContainer container = new Container();
				IContainerConfiguration configuration = (IContainerConfiguration) configurations["app.config"];
				container.Configuration = configuration;
				return container;
			}

			lock (configurations.SyncRoot)
			{
				ConfigurationDeserializer deserializer = new ConfigurationDeserializer();
				
#if NET2
                XmlElement xmlRoot = (XmlElement) ConfigurationManager.GetSection("nfactory");
#else
                XmlElement xmlRoot = (XmlElement) ConfigurationSettings.GetConfig("nfactory");
#endif

                IContainer res = deserializer.Configure(xmlRoot);
				configurations["app.config"] = res.Configuration;
				return res;
			}
		}

		public static IContainer Configure(string configureAs)
		{
			IContainer container = Configure();
			container.ConfigureObject(container, configureAs);
			return container;
		}
	}
}