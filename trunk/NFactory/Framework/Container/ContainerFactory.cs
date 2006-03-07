using System;
using Puzzle.NCore.Framework.Logging;
using Puzzle.NFactory.Framework.ConfigurationElements;

namespace Puzzle.NFactory.Framework
{
	public class ContainerFactory
	{
        public static IContainer FromAppConfig()
        {
            return ApplicationContext.Configure();
        }

        public static IContainer FromAppConfig(string subSectionName)
        {
            return ApplicationContext.Configure();
        }

        public static IContainer FromFile(string fileName)
        {
            return null;
        }

        public static IContainer FromFile(string fileName, string subSectionName)
        {
            return null;
        }
	}
}
