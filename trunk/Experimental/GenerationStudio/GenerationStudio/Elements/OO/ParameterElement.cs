using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(MethodElement))]
    [ElementName("Parameter")]
    [ElementIcon("GenerationStudio.Images.field.bmp")]
    public class ParameterElement : NamedElement
    {
        public string Type { get; set; }
    }
}
