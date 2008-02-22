using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.FastForward.Framework.Service;
using Puzzle.SideFX.Framework.Parsing;
using Puzzle.SideFX.Framework;

namespace Puzzle.FastForward.Framework.Executors.CreateObject
{
    public class CreateObjectCommand
    {
        public static CreateObjectCommand Evaluate(IEngine engine, Command command)
        {
            if (command.Parameters.Count < 2)
                return null;

            if (command.Parameters[0].Name.ToLower() != "create")
                return null;
            if (command.Parameters[0].Value != null)
                return null;

            string name = command.Parameters[1].Name;
            if (string.IsNullOrEmpty(name))
                return null;

            ISchemaService schemaService = engine.GetService<ISchemaService>();
            if (!schemaService.HasClass(name))
                return null;

            CreateObjectCommand createObjectCommand = new CreateObjectCommand(name);

            IList<Parameter> parameters = command.Parameters[1].Value as IList<Parameter>;
            if (parameters != null)
            {
                foreach (Parameter parameter in parameters)
                {
                    if (schemaService.HasProperty(name, parameter.Name))
                    {
                        createObjectCommand.Values[parameter.Name] = parameter.Value;
                    }
                }
            }

            return createObjectCommand;
        }

        public CreateObjectCommand(string className)
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
    }
}
