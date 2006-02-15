using System.Collections;
using System.Configuration;
using System.Xml;

using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.ConfigurationElements;

namespace Puzzle.NAspect.Extensions
{
	/// <summary>
	/// Custom config files can be used...no need to use web or app.config only
	/// </summary>
	public class NAspectApplicationContext
	{
		private static volatile Hashtable configurations = new Hashtable();

		public static Engine Configure(string strFilename)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = false;
			xmlDocument.Load(strFilename);

			XmlElement xmlRoot = GetElementByTagName(xmlDocument, "naspect");
			return Configure((XmlElement)xmlRoot.FirstChild);
		}

		public static Engine Configure() 
		{
			XmlElement xmlRoot = (XmlElement) ConfigurationSettings.GetConfig("naspect");
			return Configure(xmlRoot);
		}

		private static Engine Configure(XmlElement xmlRoot)
		{
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
				Engine res = deserializer.Configure(xmlRoot);

				configurations["app.config"] = res.Configuration;
				return res;
			}
		}

		private static XmlElement GetElementByTagName(XmlDocument xmlDocument, string strName) 
		{
			XmlNodeList nodes = xmlDocument.GetElementsByTagName(strName);
			
			if(nodes.Count != 0) 
				return (XmlElement) nodes.Item(0);
			else
				return null;
		}
	}
}