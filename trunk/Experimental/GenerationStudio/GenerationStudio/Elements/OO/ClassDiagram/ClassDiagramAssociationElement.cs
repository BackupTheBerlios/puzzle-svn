using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;
using System.ComponentModel;

namespace GenerationStudio.Elements
{
    public enum ClassDiagramPortSide
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    public enum ClassDiagramAssociationType
    {
        None,
        Association,
        Aggregation,
        Inheritance,
    }

    [Serializable]
    [ElementParent(typeof(ClassDiagramElement))]
    [ElementName("Association")]
    [ElementIcon("GenerationStudio.Images.association.gif")]
    public class ClassDiagramAssociationElement : ClassDiagramMemberElement
    {
        public ClassDiagramMemberElement Start { get; set; }
        public int StartPortId { get; set; }
        public ClassDiagramPortSide StartPortSide { get; set; }
        public ClassDiagramAssociationType AssociationType { get; set; }

        public ClassDiagramMemberElement End { get; set; }
        public int EndPortId { get; set; }
        public ClassDiagramPortSide EndPortSide { get; set; }        

        public override string GetDisplayName()
        {
            return string.Format("{0}: {1} - {2}",AssociationType, Start.GetDisplayName(), End.GetDisplayName());
        }
    }
}
