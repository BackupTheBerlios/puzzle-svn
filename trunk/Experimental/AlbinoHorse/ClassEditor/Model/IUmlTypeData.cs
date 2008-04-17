using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public interface IUmlTypeData
    {
        string TypeName { get; set; }
        string InheritsTypeName { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        bool Expanded { get; set; }
        bool IsAbstract { get; set; }

        UmlProperty CreateProperty();
        void RemoveProperty(UmlProperty property);
        int GetPropertyCount();
        IEnumerable<UmlProperty> GetProperties();
        

        void AddMethod(UmlMethod method);
        void RemoveMethod(UmlMethod method);
        int GetMethodCount();
        IEnumerable<UmlMethod> GetMethods();
        UmlMethod CreateMethod();

    }
}
