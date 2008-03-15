using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging.Classes
{
    public interface ILoggable
    {
        ILogger Logger { get; set; }
    }
}
