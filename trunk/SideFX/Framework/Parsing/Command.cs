using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.SideFX.Framework.Parsing
{
    public class Command : IParent
    {

        private IList<Parameter> parameters = new List<Parameter>();

        public IList<Parameter> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        #region IParent Members

        public void AddParameter(Parameter parameter)
        {
            parameters.Add(parameter); 
        }

        public IList<Parameter> GetParameters()
        {
            return parameters;
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Parameter parameter in parameters)
                sb.Append(parameter.ToString());
            return sb.ToString();
        }
    }
}
