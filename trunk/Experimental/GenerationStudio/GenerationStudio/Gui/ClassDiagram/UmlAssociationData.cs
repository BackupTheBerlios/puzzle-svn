﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlAssociationData : IUmlRelationData
    {
        public UmlClassDiagramData DiagramData { get; set; }
        public DiagramRelationElement Owner { get; set; }

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

        public UmlRelationType AssociationType
        {
            get { return (UmlRelationType)(int)Owner.AssociationType; }
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
