using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedConfig
{
    public class LogManager
    {
        #region Property Loggers
        private List<ILogger> loggers = new List<ILogger> ();
        public virtual List<ILogger> Loggers
        {
            get
            {
                return this.loggers;
            }
        }
        #endregion

        public void Log(string text)
        {
            foreach (ILogger logger in Loggers)
            {
                logger.Log(text);
            }
        }
    }
}
