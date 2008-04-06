using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Class")]
    [ElementIcon("GenerationStudio.Images.class.gif")]
    public class ClassElement : InstanceTypeElement
    {
        public ClassElement Inherits { get; set; }
        public bool IsAbstract { get; set; }
    }
}
