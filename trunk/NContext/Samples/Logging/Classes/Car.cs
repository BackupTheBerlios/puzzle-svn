using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging.Classes
{
    public class Car : ILoggable
    {
        public ILogger Logger {get;set;}
        public int NumberOfWheels { get; set; }
        public string Name { get; set; }

        public void Drive()
        {
            Logger.Log(string.Format ("Brum brum I'm driving with my {0} with {1} wheels",Name,NumberOfWheels));
        }
    }
}
