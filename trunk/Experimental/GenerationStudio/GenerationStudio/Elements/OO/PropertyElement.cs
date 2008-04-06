using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Property")]
    [ElementIcon("GenerationStudio.Images.property.gif")]
    public class PropertyElement : TypeMemberElement
    {
        public string Type { get; set; }
    }
}
