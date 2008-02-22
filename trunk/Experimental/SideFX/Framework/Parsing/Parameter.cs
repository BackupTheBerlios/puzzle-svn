using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.SideFX.Framework.Parsing
{
    public class Parameter : IParent
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private object value;

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        #region IParent Members

        public void AddParameter(Parameter parameter)
        {
            IList<Parameter> parameters = value as IList<Parameter>;
            if (parameters == null)
                throw new Exception("Baaad bad thing...");

            parameters.Add(parameter);
        }

        public IList<Parameter> GetParameters()
        {
            IList<Parameter> parameters = value as IList<Parameter>;
            if (parameters == null)
                throw new Exception("Baaad bad thing...");

            return parameters;
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Name);
            sb.Append(" ");
            if (this.Value != null)
            {
                sb.Append("= ");

                if (typeof(string).IsAssignableFrom(this.Value.GetType()))
                {
                    sb.Append(this.Value.ToString());
                    sb.Append(", ");
                }
                else
                {
                    sb.Append("(");
                    foreach (Parameter parameter in GetParameters())
                        sb.Append(parameter.ToString());
                    sb.Append(") ");
                }
            }
            return sb.ToString();
            ;
        }
    }
}
