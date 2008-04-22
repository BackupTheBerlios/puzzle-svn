using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlAssociationData : IUmlAssociationData
    {
        public UmlClassDiagramData DiagramData { get; set; }
        public ClassDiagramAssociationElement Owner { get; set; }

        public Shape Start
        {
            get 
            { 
                return DiagramData.GetShape (Owner.Start);
            }
        }

        public Shape End
        {
            get 
            { 
                return DiagramData.GetShape (Owner.End);
            }
        }

        public int StartPortId
        {
            get { return Owner.StartPortId; }
        }

        public UmlPortSide StartPortSide
        {
            get { return (UmlPortSide)(int)Owner.StartPortSide; }
        }

        public UmlAssociationType AssociationType
        {
            get { return (UmlAssociationType)(int)Owner.AssociationType; }
        }

        public int EndPortId
        {
            get { return Owner.EndPortId; }
        }

        public UmlPortSide EndPortSide
        {
            get { return (UmlPortSide)(int)Owner.EndPortSide; }
        }



    }
}
