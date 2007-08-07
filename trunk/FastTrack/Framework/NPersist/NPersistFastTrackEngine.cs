using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.FastForward.Framework.NPersist;
using Puzzle.FastForward.Framework.Service;

namespace Puzzle.FastTrack.Framework.NPersist
{
    public class NPersistFastTrackEngine : NPersistFastForwardEngine
    {
        public NPersistFastTrackEngine()
            : base()
        {
            string mapPath = System.Configuration.ConfigurationManager.AppSettings["MapPath"];
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

            IConfigurationService configurationService = this.GetService<IConfigurationService>();
            configurationService.ConnectionString = connectionString;
            configurationService.SchemaFilePath = mapPath;
        }
    }
}
