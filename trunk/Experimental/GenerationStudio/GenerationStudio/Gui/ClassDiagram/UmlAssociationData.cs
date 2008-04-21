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
    }
}
