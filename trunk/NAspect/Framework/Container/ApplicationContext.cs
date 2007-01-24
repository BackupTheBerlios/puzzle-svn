// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Collections;
using System.Configuration;
using System.Xml;
using Puzzle.NAspect.Framework.ConfigurationElements;
using System;

namespace Puzzle.NAspect.Framework
{
    /// <summary>
    /// Factory class that creates and configures an IEngine from app.config
    /// </summary>
    public class ApplicationContext
    {
        private static volatile Hashtable configurations = new Hashtable();

        /// <summary>
        /// Deserializes app.config and configures an IEngine.
        /// </summary>
        /// <returns>a default configured IEngine</returns>
        public static IEngine Configure()
        {
#if NET2
            XmlElement o = (XmlElement) ConfigurationManager.GetSection("naspect");
#else
            XmlElement o = (XmlElement) ConfigurationSettings.GetConfig("naspect");
#endif
            IEngine engine = FindCachedConfiguration("app.config");
            if (engine != null)
                return engine;

            //if (configurations.ContainsKey("app.config"))
            //{
            //    Engine engine = new Engine("app.config");
            //    EngineConfiguration configuration = (EngineConfiguration) configurations["app.config"];
            //    engine.Configuration = configuration;
            //    return engine;
            //}

            lock (configurations.SyncRoot)
            {
                ConfigurationDeserializer deserializer = new ConfigurationDeserializer();
#if NET2
                XmlElement xmlRoot = (XmlElement) ConfigurationManager.GetSection("naspect");
#else
				XmlElement xmlRoot = (XmlElement) ConfigurationSettings.GetConfig("naspect");
#endif

                IEngine res = deserializer.Configure(xmlRoot);

                configurations["app.config"] = res.Configuration;
                return res;
            }
        }

        public static IEngine Configure(string fileName)
        {
            return Configure(fileName, false);
        }

        public static IEngine Configure(string fileName, bool useTypePlaceHolders)
        {

            IEngine engine = FindCachedConfiguration(fileName);
            if (engine != null)
                return engine;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("naspect");
            if (xmlNode == null)
            {
                xmlNode = xmlDoc.SelectSingleNode("configuration");
                if (xmlNode != null)
                {
                    xmlNode = xmlNode.SelectSingleNode("naspect");
                }
            }

            if (xmlNode != null)
            {
                xmlNode = xmlNode.SelectSingleNode("configuration");
            }

            if (xmlNode != null)
            {
                lock (configurations.SyncRoot)
                {
                    ConfigurationDeserializer deserializer = new ConfigurationDeserializer();

                    IEngine res = deserializer.Configure(xmlNode, fileName, useTypePlaceHolders);

                    configurations[fileName] = res.Configuration;
                    return res;
                }
            }

            throw new Exception(String.Format("Could not read xml conig file {0}", fileName));
        }

        private static IEngine FindCachedConfiguration(string name)
        {
            if (configurations.ContainsKey(name))
            {
                Engine engine = new Engine(name);
                EngineConfiguration configuration = (EngineConfiguration)configurations[name];
                engine.Configuration = configuration;
                return engine;
            }
            return null;
        }
    }
}