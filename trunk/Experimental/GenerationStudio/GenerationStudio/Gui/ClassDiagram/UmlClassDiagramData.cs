using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlClassDiagramData : IUmlDiagramData
    {
        public ClassDiagramElement Owner { get; set; }

        public T CreateShape<T>() where T : Shape, new()
        {
            throw new NotImplementedException();
        }

        public void RemoveShape(Shape item)
        {
            throw new NotImplementedException();
        }


        public IList<Shape> GetShapes()
        {
            var res = GetValidMembers();

            List<Shape> shapes = new List<Shape>();
            foreach (ClassDiagramMemberElement member in res)
                shapes.Add(GetShape(member));

            return shapes;
        }

        private IList<ClassDiagramMemberElement> GetValidMembers()
        {
            var res = Owner.GetChildren<ClassDiagramMemberElement>();
            return res;
        }

        private Dictionary<ClassDiagramMemberElement, Shape> shapeLookup = new Dictionary<ClassDiagramMemberElement, Shape>();

        private Shape GetShape(ClassDiagramMemberElement member)
        {
            Shape shape = null;
            if (shapeLookup.TryGetValue(member, out shape))
            {
                return shape;
            }

            

            

            shapeLookup.Add(member, shape);

            return shape;
        }
    }
}
