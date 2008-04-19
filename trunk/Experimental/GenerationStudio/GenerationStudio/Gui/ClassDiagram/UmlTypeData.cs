using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlTypeData : IUmlInstanceTypeData
    {

        public ClassDiagramTypeElement Owner { get; set; }

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

        public bool IsAbstract
        {
            get
            {
                return Owner.Type.IsAbstract;
            }
            set
            {
                Owner.Type.IsAbstract = value;
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

        public string InheritsTypeName
        {
            get
            {
                return Owner.Type.Inherits;
            }
            set
            {
                Owner.Type.Inherits = value;
            }
        }

        

        public void RemoveProperty(UmlTypeMember property)
        {
            PropertyElement pe = (PropertyElement)property.DataSource.DataObject;
            pe.Parent.RemoveChild(pe);
            propertyLookup.Remove(pe);
        }

        public int GetPropertyCount()
        {
            var res = GetValidProperties();

            return res.ToList().Count();
        }

        private IOrderedEnumerable<Element> GetValidProperties()
        {
            var res = from e in Owner.Type.AllChildren

                      where !e.Excluded &&
                            e is PropertyElement &&
                            e.GetDisplayName() != ""
                      orderby e.GetDisplayName()
                      select e;
            return res;
        }

        private Dictionary<PropertyElement, UmlTypeMember> propertyLookup = new Dictionary<PropertyElement, UmlTypeMember>();

        public IEnumerable<UmlTypeMember> GetProperties()
        {
            var res = GetValidProperties();

            foreach (PropertyElement pe in res)
                yield return GetProperty(pe);
        }

        private UmlTypeMember GetProperty(PropertyElement pe)
        {
            UmlTypeMember property = null;
            if (propertyLookup.TryGetValue (pe,out property))
            {
                return property;
            }

            property = new UmlTypeMember();
            UmlPropertyData data = new UmlPropertyData();
            data.Owner = pe;
            property.DataSource = data;

            propertyLookup.Add(pe, property);

            return property;
        }

        public UmlTypeMember CreateProperty()
        {
            UmlTypeMember property = new UmlTypeMember();
            UmlPropertyData data = new UmlPropertyData();
            PropertyElement pe = new PropertyElement();
            pe.Type = "string";
            pe.Name = "";
            data.Owner = pe;
            property.DataSource = data;

            propertyLookup.Add(pe, property);
            Owner.Type.AddChild(pe);

            return property;
        }        
    }
}
