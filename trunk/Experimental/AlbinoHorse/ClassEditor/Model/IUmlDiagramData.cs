using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AlbinoHorse.Model
{
    public interface IUmlDiagramData
    {
        UmlType CreateClass();
        UmlType CreateInterface();
        UmlType CreateEnum();

        void RemoveClass(UmlType item);
        void RemoveInterface(UmlType item);
        void RemoveEnum(UmlType item);


        IEnumerable<Shape> GetShapes();
        
    }
}
