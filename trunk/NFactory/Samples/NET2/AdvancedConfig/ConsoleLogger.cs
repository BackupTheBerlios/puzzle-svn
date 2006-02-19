using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedConfig
{
    public class ConsoleLogger : ILogger 
    {
        public void Log(string text)
        {
            Console.WriteLine("log: {0}",text);
        }
    }
}
