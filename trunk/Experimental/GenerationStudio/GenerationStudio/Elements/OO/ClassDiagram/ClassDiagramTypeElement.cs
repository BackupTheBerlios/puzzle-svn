using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(ClassDiagramElement))]
    [ElementName("Type element")]
    [ElementIcon("GenerationStudio.Images.class.gif")]
    public class ClassDiagramTypeElement : ClassDiagramMemberElement
    {
        public TypeElement Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
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
