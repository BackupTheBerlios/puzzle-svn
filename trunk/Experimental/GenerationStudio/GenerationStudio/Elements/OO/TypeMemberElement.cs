using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(InstanceTypeElement))]
    public abstract class TypeMemberElement : NamedElement
    {
    }
}
