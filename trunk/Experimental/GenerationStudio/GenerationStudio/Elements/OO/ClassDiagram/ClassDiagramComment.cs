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
    [ElementName("Comment")]
    [ElementIcon("GenerationStudio.Images.class.gif")]
    public class ClassDiagramCommentElement : ClassDiagramMemberElement
    {        
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Text { get; set; }

        public override string GetDisplayName()
        {
            return "Comment";
        }
    }
}
