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
    [ElementName("Association")]
    [ElementIcon("GenerationStudio.Images.association.gif")]
    public class ClassDiagramAssociationElement : ClassDiagramMemberElement
    {
        public ClassDiagramMemberElement Start { get; set; }
        public ClassDiagramMemberElement End { get; set; }

        public override string GetDisplayName()
        {
            return string.Format("Association: {0} - {1}", Start.GetDisplayName(), End.GetDisplayName());
        }
    }
}
