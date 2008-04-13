using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using System.Runtime.Serialization;
using System.Data;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Columns")]
    [ElementIcon("GenerationStudio.Images.Folder.gif")]
    public class KeysElement : StaticElement
    {
        public override string GetDisplayName()
        {
            return "Keys";
        }
    }
}
