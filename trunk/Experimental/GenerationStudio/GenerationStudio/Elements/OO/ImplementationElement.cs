using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(InstanceTypeElement))]
    [ElementName("Interface Implementation")]
    [ElementIcon("GenerationStudio.Images.implementation.bmp")]
    public class ImplementationElement : Element
    {
        public InterfaceElement Implements { get; set; }
    }
}
