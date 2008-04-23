using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlInterfaceData : IUmlInterfaceData
    {

        public DiagramTypeElement Owner { get; set; }

        public bool Expanded
        {
            get
            {
                return Owner.Expanded;
            }
            set
            {
                Owner.Expanded = value;
            }
        }

        public int X
        {
            get
            {
                return Owner.X;
            }
            set
            {
                Owner.X = value;
            }
        }

        public int Y
        {
            get
            {
                return Owner.Y;
            }
            set
            {
                Owner.Y = value;
            }
        }

        public int Width
        {
            get
            {
                return Owner.Width;
            }
            set
            {
                Owner.Width = value;
            }
        }

        public string TypeName
        {
            get
            {
                return Owner.Type.Name;
            }
            set
            {
                Owner.Type.Name = value;
            }
        }

        public void RemoveTypeMember(UmlTypeMember property)
        {
            TypeMemberElement pe = (TypeMemberElement)property.DataSource.DataObject;
            pe.Parent.RemoveChild(pe);
            typeMemberLookup.Remove(pe);
        }

        private IOrderedEnumerable<Element> GetValidProperties()
        {
            var res = from e in Owner.Type.AllChildren

                      where !e.Excluded &&
                            (e is PropertyElement || e is MethodElement)
                        orderby e.GetDisplayName()
                      select e;
            return res;
        }

        private Dictionary<TypeMemberElement, UmlTypeMember> typeMemberLookup = new Dictionary<TypeMemberElement, UmlTypeMember>();

        public IList<UmlTypeMember> GetTypeMembers()
        {
            var res = GetValidProperties();

            List<UmlTypeMember> members = new List<UmlTypeMember>();
            foreach (TypeMemberElement pe in res)
                members.Add(GetTypeMember(pe));

            return members;
        }

        private UmlTypeMember GetTypeMember(TypeMemberElement pe)
        {
            UmlTypeMember typeMember = null;
            if (typeMemberLookup.TryGetValue(pe, out typeMember))
            {
                return typeMember;
            }

            typeMember = new UmlTypeMember();
            UmlTypeMemberData data = new UmlTypeMemberData();
            data.Owner = pe;
            typeMember.DataSource = data;

            typeMemberLookup.Add(pe, typeMember);

            return typeMember;
        }

        public UmlTypeMember CreateTypeMember(string sectionName)
        {
            UmlTypeMember property = new UmlTypeMember();
            UmlTypeMemberData data = new UmlTypeMemberData();
            if (sectionName == "Properties")
            {
                PropertyElement pe = new PropertyElement();
                pe.Type = "string";
                pe.Name = "";
                data.Owner = pe;
                property.DataSource = data;

                typeMemberLookup.Add(pe, property);
                Owner.Type.AddChild(pe);
            }

            if (sectionName == "Methods")
            {
                MethodElement pe = new MethodElement();
                pe.Name = "";
                data.Owner = pe;
                property.DataSource = data;

                typeMemberLookup.Add(pe, property);
                Owner.Type.AddChild(pe);
            }

            return property;
        }
    }
}
