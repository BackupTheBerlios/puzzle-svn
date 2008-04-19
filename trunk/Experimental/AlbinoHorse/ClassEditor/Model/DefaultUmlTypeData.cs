using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public class DefaultUmlInstanceTypeData : IUmlInstanceTypeData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public bool Expanded { get; set; }
        public string TypeName { get; set; }
        public string InheritsTypeName { get; set; }
        public bool IsAbstract { get; set; }

        private List<UmlTypeMember> properties = new List<UmlTypeMember>(); 

        public void RemoveProperty(UmlTypeMember property)
        {
            properties.Remove(property);
        }

        public int GetPropertyCount()
        {
            return properties.Count;
        }

        public IEnumerable<UmlTypeMember> GetProperties()
        {
            foreach (var property in properties)
                yield return property;
        }

        public UmlTypeMember CreateProperty()
        {
            UmlTypeMember property = new UmlTypeMember();
            property.DataSource = new DefaultUmlTypeMemberData();
            properties.Add(property);
            return property;
        }       
    }
}
