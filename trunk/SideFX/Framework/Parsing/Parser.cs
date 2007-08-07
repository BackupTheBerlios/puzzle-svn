using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.SideFX.Framework.Parsing
{
    public class Parser
    {
        public static IList<Command> Parse(string text)
        {
            Parser parser = new Parser(text);
            return parser.Parse();
        }

        private Parser(string text)
        {
            this.text = text;
        }

        private ParserState parserState = ParserState.ParameterName;
        private bool inString = false;
        private bool escape = false;
        private string text = "";

        private IList<Command> commands = new List<Command>();
        private Command command = new Command();
        private Parameter parameter = new Parameter();
        private IList<IParent> parentStack = new List<IParent>();

        private IList<Command> Parse()
        {
            parentStack.Add(command);

            foreach (char c in text.ToCharArray())
            {
                switch (parserState)
                {
                    case ParserState.BeginParameterName:
                        ParseBeginParameterName(c);
                        break;
                    case ParserState.ParameterName:
                        ParseParameterName(c);
                        break;
                    case ParserState.EndParameterName:
                        ParseEndParameterName(c);
                        break;
                    case ParserState.BeginParameterValue:
                        ParseBeginParameterValue(c);
                        break;
                    case ParserState.ParameterValue:
                        ParseParameterValue(c);
                        break;
                }
            }

            switch (parserState)
            {
                case ParserState.ParameterName:
                case ParserState.EndParameterName:
                    AddParameter(parameter);
                    break;
                case ParserState.ParameterValue:
                    AddParameter(parameter);
                    break;
            }

            commands.Add(command);
            return commands;
        }

        private void ParseBeginParameterName(char c)
        {
            switch (c)
            {
                case ' ':
                    break; // do nothing

                case '=':
                case '(':
                    throw new Exception("Name must not be empty!");

                case ')':
                    throw new Exception("Unmatching block closing!");

                default:
                    parameter.Name += c;
                    parserState = ParserState.ParameterName;
                    break;
            }
        }


        private void ParseParameterName(char c)
        {
            switch (c)
            {
                case ' ':
                    parserState = ParserState.EndParameterName;
                    break;

                case ',':
                    BeginNewParameter();
                    break;

                case '=':
                    parserState = ParserState.BeginParameterValue;
                    break;

                case '(':
                    BeginBlock();
                    break;

                default:
                    parameter.Name += c;
                    break;
            }

        }

        private void ParseEndParameterName(char c)
        {
            switch (c)
            {
                case ' ':
                    break; // do nothing

                case '=':
                    parserState = ParserState.BeginParameterValue;
                    break;

                case '(':
                    BeginBlock();
                    break;

                default:
                    BeginNewParameter();
                    parameter.Name += c;
                    parserState = ParserState.ParameterName;
                    break;
            }
        }

        private void ParseBeginParameterValue(char c)
        {
            switch (c)
            {
                case ' ':
                    break; // do nothing

                case '=':
                    throw new Exception("Name must not be empty!");

                case '(':
                    BeginBlock();
                    break;

                case ')':
                    throw new Exception("Unmatching block closing!");

                case '"':
                    inString = true;
                    parameter.Value = "";
                    parserState = ParserState.ParameterValue;
                    break;

                default:
                    parameter.Value = "";
                    parameter.Value = ((string)parameter.Value) + c;
                    parserState = ParserState.ParameterValue;
                    break;
            }
        }

        private void ParseParameterValue(char c)
        {
            if (inString)
            {
                if (escape)
                {
                    parameter.Value = ((string)parameter.Value) + c;
                    escape = false;
                }
                else
                {
                    switch (c)
                    {
                        case '\\':
                            escape = true;
                            break;
                        case '"':
                            inString = false;
                            BeginNewParameter();
                            break;
                        default :
                            parameter.Value = ((string)parameter.Value) + c;
                            break;
                    }
                }                
            }
            else
            {

                switch (c)
                {
                    case ' ':
                    case ',':
                        BeginNewParameter();
                        break;

                    case '(':
                        throw new Exception("Wrong...");

                    case ')':
                        EndBlock();
                        break;

                    default:
                        parameter.Value = ((string)parameter.Value) + c;
                        break;
                }
            }
        }

        private void EndBlock()
        {
            AddParameter(parameter);
            parameter = new Parameter();
            PullParent();
            parserState = ParserState.BeginParameterName;
        }

        private void BeginNewParameter()
        {
            AddParameter(parameter);
            parameter = new Parameter();
            parserState = ParserState.BeginParameterName;
        }

        private void BeginBlock()
        {
            parameter.Value = new List<Parameter>();
            AddParameter(parameter);
            PushParent(parameter);
            parameter = new Parameter();

            parserState = ParserState.BeginParameterName;
        }

        private void AddParameter(Parameter parameter)
        {
            IParent parent = GetCurrentParent();
            parent.AddParameter(parameter);
        }

        private void PushParent(IParent newParent)
        {
            parentStack.Add(newParent);
        }

        private void PullParent()
        {
            parentStack.RemoveAt(parentStack.Count - 1);
        }

        private IParent GetCurrentParent()
        {
            return parentStack[parentStack.Count - 1];
        }
    }
}
