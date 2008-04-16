using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlTypeData : IUmlTypeData
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

        public string InheritsType
        {
            get
            {
                return null;
                //if (Owner.Type.Inherits != null)
                //    return Owner.Type.Inherits.Name;
                //else
                //    return null;
            }
            set
            {
               // Owner.Inherits = value;
            }
        }

        

        public void AddProperty(UmlProperty property)
        {
        
        }

        public void RemoveProperty(UmlProperty property)
        {
            PropertyElement pe = (PropertyElement)property.DataSource.DataObject;
            pe.Parent.RemoveChild(pe);
        }

        public int GetPropertyCount()
        {
            return Owner.Type.GetChildren<PropertyElement>().Count;
        }

        private Dictionary<PropertyElement, UmlProperty> propertyLookup = new Dictionary<PropertyElement, UmlProperty>();

        public IEnumerable<UmlProperty> GetProperties()
        {
            var res = from e in Owner.Type.AllChildren

                      where !e.Excluded &&
                            e is PropertyElement
                      orderby e.GetDisplayName()
                      select e;

            foreach (PropertyElement pe in res)
                yield return GetProperty(pe);
        }

        private UmlProperty GetProperty(PropertyElement pe)
        {
            UmlProperty property = null;
            if (propertyLookup.TryGetValue (pe,out property))
            {
                return property;
            }

            property = new UmlProperty();
            UmlPropertyData data = new UmlPropertyData();
            data.Owner = pe;
            property.DataSource = data;

            propertyLookup.Add(pe, property);

            return property;
        }

        public UmlProperty CreateProperty()
        {
            UmlProperty property = new UmlProperty();
            property.DataSource = new DefaultUmlPropertyData();
            return property;
        }

        public void AddMethod(UmlMethod method)
        {
            throw new NotImplementedException();
        }

        public void RemoveMethod(UmlMethod method)
        {
            throw new NotImplementedException();
        }

        public int GetMethodCount()
        {
            return 0;
        }

        public IEnumerable<UmlMethod> GetMethods()
        {
            return Enumerable.Empty<UmlMethod>();
        }

        public UmlMethod CreateMethod()
        {
            throw new NotImplementedException();
        }
    }
}
