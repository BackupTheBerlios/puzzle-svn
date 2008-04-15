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

        public ClassElement Owner { get; set; }

        public string TypeName
        {
            get
            {
                return Owner.Name;
            }
            set
            {
                Owner.Name = value;
            }
        }

        public string InheritsType
        {
            get
            {
                if (Owner.Inherits != null)
                    return Owner.Inherits.Name;
                else
                    return null;
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
            return Owner.GetChildren<PropertyElement>().Count;
        }

        private Dictionary<PropertyElement, UmlProperty> propertyLookup = new Dictionary<PropertyElement, UmlProperty>();

        public IEnumerable<UmlProperty> GetProperties()
        {
            foreach (PropertyElement pe in Owner.GetChildren<PropertyElement>())
            {
                UmlProperty property = GetProperty(pe);
                yield return property;
            }
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
