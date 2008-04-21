using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using System.ComponentModel;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Root")]
    public class RootElement : NamedElement
    {
        public string FilePath { get; set; }

        //root nodes are always valid
        public override bool IsValid
        {
            get
            {
                return true;
            }
        }

        public RootElement()
        {
            Name = "MyProject";
        }
    }
}
