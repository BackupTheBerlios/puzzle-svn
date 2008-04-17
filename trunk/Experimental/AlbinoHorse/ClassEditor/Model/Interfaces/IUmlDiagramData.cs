using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AlbinoHorse.Model
{
    public interface IUmlDiagramData
    {
        UmlInstanceType CreateClass();
        UmlInstanceType CreateInterface();
        UmlInstanceType CreateEnum();

        void RemoveClass(UmlInstanceType item);
        void RemoveInterface(UmlInstanceType item);
        void RemoveEnum(UmlInstanceType item);


        IEnumerable<Shape> GetShapes();
        
    }
}
