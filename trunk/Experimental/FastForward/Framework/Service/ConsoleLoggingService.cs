using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.FastForward.Framework.Service;
using Puzzle.SideFX.Framework;

namespace Puzzle.FastForward.Framework.Service
{
    public class ConsoleLoggingService : ILoggingService
    {
        #region ILoggingService Members

        public ConsoleLoggingService(IEngine engine)
        {
            this.engine = engine;
        }

        private IEngine engine;

        public void LogInfo(object sender, string text)
        {
            bool logging = true;

            IConfigurationService configurationService = engine.GetService<IConfigurationService>();
            if (configurationService != null)
                logging = configurationService.Logging;

            if (logging)
                Console.WriteLine(text);
        }

        #endregion
    }
}
