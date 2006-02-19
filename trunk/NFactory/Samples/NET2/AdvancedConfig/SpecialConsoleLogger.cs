using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedConfig
{
    public class SpecialConsoleLogger : ILogger
    {
        public void Log(string text)
        {
            Console.WriteLine("special special log: {0}", text);
        }
    }
}
