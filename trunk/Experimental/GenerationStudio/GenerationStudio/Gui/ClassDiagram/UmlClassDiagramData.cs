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

        public Shape GetShape(ClassDiagramMemberElement member)
        {
            Shape shape = null;
            if (shapeLookup.TryGetValue(member, out shape))
            {
                return shape;
            }

            if (member is ClassDiagramTypeElement)
            {
                shape = GetUmlType(member as ClassDiagramTypeElement);
            }
            if (member is ClassDiagramCommentElement)
            {
                shape = GetUmlComment(member as ClassDiagramCommentElement);
            }
            if (member is ClassDiagramAssociationElement)
            {
                shape = GetUmlAssociation(member as ClassDiagramAssociationElement);
            }

            shapeLookup.Add(member, shape);

            return shape;
        }

        private Shape GetUmlAssociation(ClassDiagramAssociationElement associationElement)
        {
            UmlAssociation association = new UmlAssociation();
            UmlAssociationData data = new UmlAssociationData();
            data.Owner = associationElement;
            data.DiagramData = this;
            association.DataSource = data;

            return association;
        }

        private UmlInstanceType GetUmlType(ClassDiagramTypeElement diagramElement)
        {
            UmlInstanceType t = null;
            if (diagramElement.Type is InterfaceElement)
            {
                t = new UmlInterface();
                UmlInterfaceData data = new UmlInterfaceData();
                data.Owner = diagramElement;
                t.DataSource = data;
            }

            if (diagramElement.Type is ClassElement)
            {
                t = new UmlClass();
                UmlClassData data = new UmlClassData();
                data.Owner = diagramElement;
                t.DataSource = data;
            }

            if (diagramElement.Type is EnumElement)
            {
                t = new UmlEnum();
                UmlEnumData data = new UmlEnumData();
                data.Owner = diagramElement;
                t.DataSource = data;
            }

            return t;
        }

        private UmlComment GetUmlComment(ClassDiagramCommentElement diagramElement)
        {
            UmlComment comment = new UmlComment();
            UmlCommentData data = new UmlCommentData();
            data.Owner = diagramElement;
            comment.DataSource = data;
            return comment;
        }
    }
}
