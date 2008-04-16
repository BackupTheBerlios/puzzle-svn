using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public class DefaultUmlTypeData : IUmlTypeData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public bool Expanded { get; set; }
        public string TypeName { get; set; }
        public string InheritsType { get; set; }
        public bool IsAbstract { get; set; }

        private List<UmlProperty> properties = new List<UmlProperty>(); 
        public void AddProperty(UmlProperty property)
        {
            properties.Add(property);
        }

        public void RemoveProperty(UmlProperty property)
        {
            properties.Remove(property);
        }

        public int GetPropertyCount()
        {
            return properties.Count;
        }

        public IEnumerable<UmlProperty> GetProperties()
        {
            foreach (var property in properties)
                yield return property;
        }

        public UmlProperty CreateProperty()
        {
            UmlProperty property = new UmlProperty();
            property.DataSource = new DefaultUmlPropertyData();
            return property;
        }

        private List<UmlMethod> methods = new List<UmlMethod>();
        public void AddMethod(UmlMethod method)
        {
            methods.Add(method);
        }

        public void RemoveMethod(UmlMethod method)
        {
            methods.Remove(method);
        }

        public int GetMethodCount()
        {
            return methods.Count;
        }

        public IEnumerable<UmlMethod> GetMethods()
        {
            foreach (var method in methods)
                yield return method;
        }

        public UmlMethod CreateMethod()
        {
            return new UmlMethod();
        }
    }
}
