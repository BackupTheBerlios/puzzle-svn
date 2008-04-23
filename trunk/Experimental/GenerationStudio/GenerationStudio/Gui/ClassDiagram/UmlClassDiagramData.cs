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
        public DiagramElement Owner { get; set; }

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
            foreach (DiagramMemberElement member in res)
                shapes.Add(GetShape(member));

            return shapes;
        }

        private IList<DiagramMemberElement> GetValidMembers()
        {
            var res = Owner.GetChildren<DiagramMemberElement>();
            return res;
        }

        private Dictionary<DiagramMemberElement, Shape> shapeLookup = new Dictionary<DiagramMemberElement, Shape>();

        public Shape GetShape(DiagramMemberElement member)
        {
            Shape shape = null;
            if (shapeLookup.TryGetValue(member, out shape))
            {
                return shape;
            }

            if (member is DiagramTypeElement)
            {
                shape = GetUmlType(member as DiagramTypeElement);
            }
            if (member is DiagramCommentElement)
            {
                shape = GetUmlComment(member as DiagramCommentElement);
            }
            if (member is DiagramRelationElement)
            {
                shape = GetUmlAssociation(member as DiagramRelationElement);
            }

            shapeLookup.Add(member, shape);

            return shape;
        }

        private Shape GetUmlAssociation(DiagramRelationElement associationElement)
        {
            UmlRelation association = new UmlRelation();
            UmlAssociationData data = new UmlAssociationData();
            data.Owner = associationElement;
            data.DiagramData = this;
            association.DataSource = data;

            return association;
        }

        private UmlInstanceType GetUmlType(DiagramTypeElement diagramElement)
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

        private UmlComment GetUmlComment(DiagramCommentElement diagramElement)
        {
            UmlComment comment = new UmlComment();
            UmlCommentData data = new UmlCommentData();
            data.Owner = diagramElement;
            comment.DataSource = data;
            return comment;
        }
    }
}
