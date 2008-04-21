﻿using System;
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
    [ElementName("Comment")]
    [ElementIcon("GenerationStudio.Images.comment.gif")]
    public class ClassDiagramCommentElement : ClassDiagramMemberElement
    {        
        [Browsable(false)]
        public int X { get; set; }
        [Browsable(false)]
        public int Y { get; set; }
        [Browsable(false)]
        public int Width { get; set; }
        [Browsable(false)]
        public int Height { get; set; }
        public string Text { get; set; }

        public override string GetDisplayName()
        {
            return "Comment";
        }
    }
}
