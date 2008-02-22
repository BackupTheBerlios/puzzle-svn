using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework.Parsing;
using Puzzle.FastForward.Framework.Service;
using Puzzle.SideFX.Framework;

namespace Puzzle.FastForward.Framework.Executors.CreateProperty
{
    public class CreatePropertyCommand
    {
        public static CreatePropertyCommand Evaluate(IEngine engine, Command command)
        {
            if (command.Parameters.Count < 3)
                return null;

            if (command.Parameters[0].Name.ToLower() != "create")
                return null;
            if (command.Parameters[0].Value != null)
                return null;

            Multiplicity multiplicity = Multiplicity.None;

            switch (command.Parameters[1].Name.ToLower())
            {
                case "property":
                case "relationship":
                    break;
                case "one-one":
                case "oneone":
                case "one-to-one":
                case "onetoone":
                    multiplicity = Multiplicity.OneToOne;
                    break;
                case "one-many":
                case "onemany":
                case "one-to-many":
                case "onetomany":
                case "reference":
                    multiplicity = Multiplicity.OneToMany;
                    break;
                case "many-one":
                case "manyone":
                case "many-to-one":
                case "manytoone":
                case "list":
                    multiplicity = Multiplicity.ManyToOne;
                    break;
                case "many-many":
                case "manymany":
                case "many-to-many":
                case "manytomany":
                    multiplicity = Multiplicity.ManyToMany;
                    break;
                default:
                    return null;
            }
            if (command.Parameters[1].Value != null)
                return null;

            string name = command.Parameters[2].Name;
            if (string.IsNullOrEmpty(name))
                return null;  // register exception...

            string[] parts = name.Split(".".ToCharArray());
            if (parts.Length != 2)
                return null;  // register exception...

            string className = parts[0];
            string propertyName = parts[1];

            if (string.IsNullOrEmpty(className))
                return null;  // register exception...

            if (string.IsNullOrEmpty(propertyName))
                return null;  // register exception...

            ISchemaService schemaService = engine.GetService<ISchemaService>();
            IObjectService objectService = engine.GetService<IObjectService>();

            CreatePropertyCommand createPropertyCommand = new CreatePropertyCommand(className, propertyName);
            createPropertyCommand.Multiplicity = multiplicity;
            createPropertyCommand.Type = typeof(string);
            createPropertyCommand.StringLength = 255;

            bool list = false;

            IList<Parameter> parameters = command.Parameters[2].Value as IList<Parameter>;
            if (parameters != null)
            {
                foreach (Parameter parameter in parameters)
                {
                    if (parameter.Value != null)
                    {
                        switch (parameter.Name.ToLower())
                        {
                            case "type":
                                if (schemaService.HasClass(parameter.Value.ToString()))
                                    createPropertyCommand.Type = objectService.GetTypeByName(parameter.Value.ToString());
                                else
                                    createPropertyCommand.Type = Type.GetType(parameter.Value.ToString());
                                break;
                            case "list":
                                list = true;
                                break;
                            case "multiplicity":
                            case "ref":
                            case "reference":
                            case "referencetype":
                                if (createPropertyCommand.Multiplicity == Multiplicity.None)
                                {
                                    string refType = parameter.Value.ToString();
                                    switch (refType.ToLower())
                                    {
                                        case "one-one":
                                        case "oneone":
                                        case "one-to-one":
                                        case "onetoone":
                                            createPropertyCommand.Multiplicity = Multiplicity.OneToOne;
                                            break;
                                        case "one-many":
                                        case "onemany":
                                        case "one-to-many":
                                        case "onetomany":
                                            createPropertyCommand.Multiplicity = Multiplicity.OneToMany;
                                            break;
                                        case "many-one":
                                        case "manyone":
                                        case "many-to-one":
                                        case "manytoone":
                                            createPropertyCommand.Multiplicity = Multiplicity.ManyToOne;
                                            break;
                                        case "many-many":
                                        case "manymany":
                                        case "many-to-many":
                                        case "manytomany":
                                            createPropertyCommand.Multiplicity = Multiplicity.ManyToMany;
                                            break;
                                        default:
                                            createPropertyCommand.Multiplicity = Multiplicity.None;
                                            break;
                                    }
                                }
                                list = true;
                                break;
                            case "length":
                                createPropertyCommand.StringLength = Convert.ToInt32(parameter.Value.ToString());
                                break;
                            case "column":
                            case "columnname":
                                createPropertyCommand.ColumnName = parameter.Value.ToString();
                                break;
                            case "null":
                            case "nullable":
                                createPropertyCommand.Nullable = Convert.ToBoolean(parameter.Value.ToString());
                                break;
                            case "inverse":
                                createPropertyCommand.Inverse = parameter.Value.ToString();
                                break;
                        }
                    }
                }
            }

            if (createPropertyCommand.Multiplicity == Multiplicity.None)
            {
                if (schemaService.HasClass(objectService.GetTypeName(createPropertyCommand.Type)))
                {
                    if (list)
                        createPropertyCommand.Multiplicity = Multiplicity.ManyToOne;
                    else
                        createPropertyCommand.Multiplicity = Multiplicity.OneToMany;
                }
            }

            if (createPropertyCommand.Multiplicity != Multiplicity.None)
            {
                if (string.IsNullOrEmpty(createPropertyCommand.Inverse))
                    createPropertyCommand.Inverse = "InverseOf" + name;
            }

            return createPropertyCommand;
        }

        public CreatePropertyCommand(string className, string name)
        {
            this.type = typeof(string);
            this.className = className;
            this.name = name;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string className;

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private Type type = null;

        public Type Type
        {
            get { return type; }
            set { type = value; }
        }

        private int stringLength = 255;

        public int StringLength
        {
            get { return stringLength; }
            set { stringLength = value; }
        }

        private string columnName;

        public string ColumnName
        {
            get 
            {
                if (string.IsNullOrEmpty(this.columnName))
                    return this.name;
                return columnName; 
            }
            set { columnName = value; }
        }

        private bool nullable = true;

        public bool Nullable
        {
            get { return nullable; }
            set { nullable = value; }
        }

        private Multiplicity multiplicity = Multiplicity.None;

        public Multiplicity Multiplicity
        {
            get { return multiplicity; }
            set { multiplicity = value; }
        }

        private string inverse = "";

        public string Inverse
        {
            get { return inverse; }
            set { inverse = value; }
        }
    }
}
