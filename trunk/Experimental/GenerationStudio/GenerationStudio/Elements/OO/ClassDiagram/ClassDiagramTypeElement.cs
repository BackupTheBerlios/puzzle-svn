using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;
using System.ComponentModel;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(ClassDiagramElement))]
    [ElementName("Type element")]
    [ElementIcon("GenerationStudio.Images.class.gif")]
    public class ClassDiagramTypeElement : ClassDiagramMemberElement
    {
        [Browsable(false)]
        public TypeElement Type { get; set; }
        [Browsable(false)]
        public int X { get; set; }
        [Browsable(false)]
        public int Y { get; set; }
        [Browsable(false)]
        public int Width { get; set; }
        [Browsable(false)]
        public bool Expanded { get; set; }

        public override string GetDisplayName()
        {
            string typeName = "*missing*";
            if (Type != null)
                typeName = Type.Name;

            return string.Format("Type: {0}", typeName);
        }
    }
}
