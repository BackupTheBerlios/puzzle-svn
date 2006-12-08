using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Puzzle.NAspect.Framework
{
	public class FixedMixinAttribute : Attribute 
	{
        public FixedMixinAttribute(Type type) 
        {
            this.types.Add(type);
        }

        public FixedMixinAttribute(IList types)
        {
            this.types = types;
        }

        public FixedMixinAttribute(params Type[] types)
        {
            foreach (Type type in types)
            this.types.Add(type);
        }

        private IList types = new ArrayList();
        public virtual IList Types
        {
            get { return types; }
        }

	
	}
}
