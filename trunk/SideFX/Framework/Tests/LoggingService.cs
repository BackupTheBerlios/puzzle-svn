using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.SideFX.Framework.Tests
{
    public class LoggingService : ILoggingService
    {
        #region ILoggingService Members

        public void WriteToLog(string text)
        {
            output += text + Environment.NewLine;

            Console.WriteLine(text);
        }

        #endregion

        private string output;

        public string Output
        {
            get { return output; }
            set { output = value; }
        }
	
    }
}
