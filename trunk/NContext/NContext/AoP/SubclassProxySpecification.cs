﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mojo
{
    public class SubclassProxySpecification
    {
        public SubclassProxySpecification()
        {
        }
        
        //The type that should be inherited
        public Type BaseType { get; set; }
        //list of mixins that should be applied
        public IList<Mixin> Mixins { get; set; }
        //list of type extenders taht should be applied
        public IList<TypeExtender> TypeExtenders { get; set; }

    }
}
