using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public interface IUmlDiagramData 
    {
        T CreateShape<T>() where T:Shape,new();
        void RemoveShape(Shape item);
        IList<Shape> GetShapes();
    }
}
