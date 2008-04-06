using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(ProjectElement))]
    [ElementName("Namespace")]
    [ElementIcon("GenerationStudio.Images.namespace.gif")]
    public class NamespaceElement : NamedElement
    {
    }
}
