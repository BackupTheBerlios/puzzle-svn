using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public class DefaultUmlDiagramData : IUmlDiagramData
    {
        public DefaultUmlDiagramData()
        {
            Shapes = new List<Shape>();
        }

        public List<Shape> Shapes { get; set; }

        public T CreateShape<T>() where T : Shape, new()
        {
            T shape = new T();
            Shapes.Add(shape);
            return shape;
        }

        public void RemoveShape(Shape item)
        {
            Shapes.Remove(item);
        }

        public IList<Shape> GetShapes()
        {
            return Shapes;
        }
    }
}
