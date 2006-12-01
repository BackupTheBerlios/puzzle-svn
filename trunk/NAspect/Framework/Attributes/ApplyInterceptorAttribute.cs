using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Framework
{
	public class ApplyInterceptorAttribute : Attribute 
	{
        public ApplyInterceptorAttribute(Type type) 
        {
            this.type = type;
        }

        private Type type;
        public virtual Type Type
        {
            get { return type; }
            set { type = value; }
        }

	}
}
