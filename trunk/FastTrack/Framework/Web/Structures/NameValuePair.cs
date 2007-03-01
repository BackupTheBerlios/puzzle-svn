using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.FastTrack.Framework.Web.Structures
{
    public class NameValuePair
    {
        public NameValuePair(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        private string name;
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        private object value;
        public virtual object Value
        {
            get { return value; }
            set { this.value = value; }
        }
	
	
    }
}
