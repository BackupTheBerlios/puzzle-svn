using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.SideFX.Framework.Parsing;
using Puzzle.FastForward.Framework.Service;

namespace Puzzle.FastForward.Framework.Executors.UpdateObjects
{
    public class UpdateObjectsCommand
    {
        public static UpdateObjectsCommand Evaluate(IEngine engine, Command command)
        {
            if (command.Parameters.Count < 3)
                return null;

            if (command.Parameters[0].Name.ToLower() != "update")
                return null;
            if (command.Parameters[0].Value != null)
                return null;

            string name = command.Parameters[1].Name;
            if (string.IsNullOrEmpty(name))
                return null;

            ISchemaService schemaService = engine.GetService<ISchemaService>();
            if (!schemaService.HasClass(name))
                return null;

            if (command.Parameters[2].Name.ToLower() != "match")
                return null;
            IList<Parameter> match = command.Parameters[2].Value as IList<Parameter>;

            UpdateObjectsCommand updateObjectsCommand = new UpdateObjectsCommand(name);

            IList<Parameter> parameters = command.Parameters[1].Value as IList<Parameter>;
            if (parameters != null)
            {
                foreach (Parameter parameter in parameters)
                {
                    if (schemaService.HasProperty(name, parameter.Name))
                    {
                        updateObjectsCommand.Values[parameter.Name] = parameter.Value;
                    }
                }
            }

            if (match != null)
            {
                foreach (Parameter parameter in match)
                {
                    if (schemaService.HasProperty(name, parameter.Name))
                    {
                        updateObjectsCommand.Match[parameter.Name] = parameter.Value;
                    }
                }
            }

            return updateObjectsCommand;
        }

        public UpdateObjectsCommand(string className)
        {
            this.className = className;
        }

        private string className;

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private IDictionary<string, object> values = new Dictionary<string, object>();

        public IDictionary<string, object> Values
        {
            get { return values; }
            set { values = value; }
        }

        private IDictionary<string, object> match = new Dictionary<string, object>();

        public IDictionary<string, object> Match
        {
            get { return match; }
            set { match = value; }
        }	

    }
}
