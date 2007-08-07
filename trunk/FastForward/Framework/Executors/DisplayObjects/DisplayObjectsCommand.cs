using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.SideFX.Framework.Parsing;
using Puzzle.FastForward.Framework.Service;

namespace Puzzle.FastForward.Framework.Executors.DisplayObjects
{
    public class DisplayObjectsCommand
    {
        public static DisplayObjectsCommand Evaluate(IEngine engine, Command command)
        {
            if (command.Parameters.Count < 2)
                return null;

            bool list = false;

            switch (command.Parameters[0].Name.ToLower())
            {
                case "display":
                case "show":
                    break;
                case "list":
                    list = true;
                    break;
                default:
                    return null;
            }
            if (command.Parameters[0].Value != null)
                return null;

            string name = command.Parameters[1].Name;
            if (string.IsNullOrEmpty(name))
                return null;

            ISchemaService schemaService = engine.GetService<ISchemaService>();
            if (!schemaService.HasClass(name))
                return null;

            DisplayObjectsCommand displayObjectsCommand = new DisplayObjectsCommand(name, list);

            EvaluateMatch(command.Parameters[1].Value, name, schemaService, displayObjectsCommand);

            if (command.Parameters.Count > 2)
            {
                switch (command.Parameters[2].Name.ToLower())
                {
                    case "match":
                    case "where":
                        EvaluateMatch(command.Parameters[2].Value, name, schemaService, displayObjectsCommand);
                        break;
                    case "query":
                        if (command.Parameters[2].Value != null)
                            displayObjectsCommand.Where = command.Parameters[2].Value.ToString();
                        break;
                }
            }
            return displayObjectsCommand;
        }

        private static void EvaluateMatch(object possibleMatch, string name, ISchemaService schemaService, DisplayObjectsCommand displayObjectsCommand)
        {
            IList<Parameter> match = possibleMatch as IList<Parameter>;
            if (match != null)
            {
                foreach (Parameter parameter in match)
                {
                    if (schemaService.HasProperty(name, parameter.Name))
                    {
                        displayObjectsCommand.Match[parameter.Name] = parameter.Value;
                    }
                }
            }
        }

        public DisplayObjectsCommand(string className, bool list)
        {
            this.className = className;
            this.list = list;
        }

        private string className;

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private IDictionary<string, object> match = new Dictionary<string, object>();

        public IDictionary<string, object> Match
        {
            get { return match; }
            set { match = value; }
        }

        private string where = "";

        public string Where
        {
            get { return where; }
            set { where = value; }
        }
	
        private bool list = false;

        public bool List
        {
            get { return list; }
            set { list = value; }
        }	

    }
}
