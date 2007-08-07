using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using System.Configuration;

namespace Puzzle.FastForward.Framework.Service
{
    public class ConfigurationService : IConfigurationService
    {
        public ConfigurationService(IEngine engine)
        {
            this.engine = engine;
        }

        private IEngine engine;

        #region IConfigurationService Members

        private string connectionString = null;

        public string ConnectionString
        {
            get 
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    string connStr = ConfigurationManager.AppSettings["FF_ConnectionString"];
                    if (string.IsNullOrEmpty(connStr))
                        throw new Exception("Missing application configuration setting: FF_ConnectionString");
                    connectionString = connStr;
                }
                return connectionString;
            }
            set { connectionString = value; }
        }

        private string schemaFilePath = null;

        public string SchemaFilePath
        {
            get 
            {
                if (string.IsNullOrEmpty(schemaFilePath))
                {
                    string path = ConfigurationManager.AppSettings["FF_SchemaFilePath"];
                    if (string.IsNullOrEmpty(path))
                        throw new Exception("Missing application configuration setting: FF_SchemaFilePath");
                    schemaFilePath = path;
                }
                return schemaFilePath;
            }
            set { schemaFilePath = value; }
        }

        private bool readLogging = false;
        private bool logging = false;

        public bool Logging
        {
            get
            {
                if (!readLogging)
                {
                    string log = ConfigurationManager.AppSettings["FF_Logging"];
                    if (string.IsNullOrEmpty(log))
                        logging = true;
                    else
                        if (log.ToLower() == "false")   
                            logging = false;
                        else
                            logging = true;
                    readLogging = true;
                }
                return logging;
            }
            set 
            { 
                logging = value;
                readLogging = true;
            }
        }

        #endregion
    }
}
