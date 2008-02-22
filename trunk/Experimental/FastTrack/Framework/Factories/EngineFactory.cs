using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Puzzle.SideFX.Framework;

namespace Puzzle.FastTrack.Framework.Factories
{
    public class EngineFactory
    {
        public static IEngine CreateEngine()
        {
            string assemblyName = System.Configuration.ConfigurationManager.AppSettings["EngineAssembly"];
            Assembly engineAssembly = Assembly.Load(assemblyName);

            string engineName = System.Configuration.ConfigurationManager.AppSettings["EngineType"];
            IEngine engine = (IEngine)engineAssembly.CreateInstance(engineName);

            return engine;
        }
    }
}
