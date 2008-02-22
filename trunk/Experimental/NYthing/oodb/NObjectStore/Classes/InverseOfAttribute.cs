using System;
using System.Collections.Generic;
using System.Text;

namespace NObjectStore
{
    [AttributeUsage (AttributeTargets.Property)]
    public class InverseOfAttribute : Attribute
    {
        public InverseOfAttribute(Type inverseType, string inverseProperty)
        {
        }
    }
}
