using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GenerationStudio.AppCore;
using GenerationStudio.Attributes;
using System.Xml.Serialization;
using System.Collections;
using System.Runtime.Serialization;
using GenerationStudio.Gui;
using System.Drawing;
using System.IO;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementIcon("GenerationStudio.Images.class.gif")]
    public abstract class StaticElement : Element
    {
        public override bool AllowDelete()
        {
            return false;
        }
    }
}
