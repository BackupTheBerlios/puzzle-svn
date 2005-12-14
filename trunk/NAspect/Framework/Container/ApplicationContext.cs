using System.Collections;
using System.Configuration;
using System.Xml;
using Puzzle.NAspect.Framework.ConfigurationElements;

namespace Puzzle.NAspect.Framework
{
	public class ApplicationContext
	{
		private static volatile Hashtable configurations = new Hashtable();

		public static Engine Configure()
		{
#if NET2
			XmlElement o = (XmlElement) ConfigurationManager.GetSection("naspect");
#else
            XmlElement o = (XmlElement) ConfigurationSettings.GetConfig("naspect");
#endif

            if (configurations.ContainsKey("app.config"))
			{
				Engine engine = new Engine("app.config");
				EngineConfiguration configuration = (EngineConfiguration) configurations["app.config"];
				engine.Configuration = configuration;
				return engine;
			}

			lock (configurations.SyncRoot)
			{
				ConfigurationDeserializer deserializer = new ConfigurationDeserializer();
#if NET2
				XmlElement xmlRoot = (XmlElement) ConfigurationManager.GetSection("naspect");
#else
				XmlElement xmlRoot = (XmlElement) ConfigurationSettings.GetConfig("naspect");
#endif

				Engine res = deserializer.Configure(xmlRoot);

				configurations["app.config"] = res.Configuration;
				return res;
			}
		}
	}
}