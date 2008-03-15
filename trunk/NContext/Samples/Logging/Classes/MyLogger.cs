using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging.Classes
{
    public class MyLogger : ILogger
    {
        public void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}
