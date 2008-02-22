using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.FastForward.Framework.Service
{
    public interface IConfigurationService
    {
        string ConnectionString { get; set; }

        string SchemaFilePath { get; set; }

        bool Logging { get; set; }
    }
}
