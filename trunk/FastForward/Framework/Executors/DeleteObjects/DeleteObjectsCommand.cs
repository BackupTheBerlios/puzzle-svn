using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.SideFX.Framework.Parsing;
using Puzzle.FastForward.Framework.Service;

namespace Puzzle.FastForward.Framework.Executors.DeleteObjects
{
    public class DeleteObjectsCommand
    {
        public static DeleteObjectsCommand Evaluate(IEngine engine, Command command)
        {
            if (command.Parameters.Count < 2)
                return null;

            if (command.Parameters[0].Name.ToLower() != "delete")
                return null;
            if (command.Parameters[0].Value != null)
                return null;

            string name = command.Parameters[1].Name;
            if (string.IsNullOrEmpty(name))
                return null;

            ISchemaService schemaService = engine.GetService<ISchemaService>();
            if (!schemaService.HasClass(name))
                return null;

            DeleteObjectsCommand deleteObjectsCommand = new DeleteObjectsCommand(name);

            EvaluateMatch(command.Parameters[1].Value, name, schemaService, deleteObjectsCommand);

            if (command.Parameters.Count > 2)
            {
                switch (command.Parameters[2].Name.ToLower())
                {
                    case "match":
                    case "where":
                        EvaluateMatch(command.Parameters[2].Value, name, schemaService, deleteObjectsCommand);
                        break;
                    case "query":
                        if (command.Parameters[2].Value != null)
                            deleteObjectsCommand.Where = command.Parameters[2].Value.ToString();
                        break;
                }
            }
            return deleteObjectsCommand;
        }

        private static void EvaluateMatch(object possibleMatch, string name, ISchemaService schemaService, DeleteObjectsCommand deleteObjectsCommand)
        {
            IList<Parameter> match = possibleMatch as IList<Parameter>;
            if (match != null)
            {
                foreach (Parameter parameter in match)
                {
                    if (schemaService.HasProperty(name, parameter.Name))
                    {
                        deleteObjectsCommand.Match[parameter.Name] = parameter.Value;
                    }
                }
            }
        }

        public DeleteObjectsCommand(string className)
        {
            this.className = className;
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

    }
}
