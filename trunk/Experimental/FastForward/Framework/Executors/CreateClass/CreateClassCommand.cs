using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.FastForward.Framework.Service;
using System.Data;
using Puzzle.SideFX.Framework.Parsing;

namespace Puzzle.FastForward.Framework.Executors.CreateClass
{
    public class CreateClassCommand
    {
        public static CreateClassCommand Evaluate(Command command)
        {
            if (command.Parameters.Count < 3)
                return null;

            if (command.Parameters[0].Name.ToLower() != "create")
                return null;
            if (command.Parameters[0].Value != null)
                return null;

            if (command.Parameters[1].Name.ToLower() != "class")
                return null;
            if (command.Parameters[1].Value != null)
                return null;

            string name = command.Parameters[2].Name;
                if (string.IsNullOrEmpty(name))
                    return null;  // register exception...

            CreateClassCommand createClassCommand = new CreateClassCommand(name);

            IList<Parameter> parameters = command.Parameters[2].Value as IList<Parameter>;
            if (parameters != null)
            {
                foreach (Parameter parameter in parameters)
                {
                    switch (parameter.Name.ToLower())
                    {
                        case "base":
                            createClassCommand.BaseClass = parameter.Value.ToString();
                            break;
                        case "table":
                        case "tablename":
                            createClassCommand.TableName = parameter.Value.ToString();
                            break;
                    }
                }
            }

            return createClassCommand;
        }

        public CreateClassCommand(string name)
        {
            this.name = name;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string tableName;

        public string TableName
        {
            get 
            {
                if (string.IsNullOrEmpty(tableName))
                    return this.name;
                return tableName; 
            }
            set { tableName = value; }
        }

        private string baseClass;

        public string BaseClass
        {
            get { return baseClass; }
            set { baseClass = value; }
        }
    }
}
