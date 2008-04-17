using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public interface IUmlInstanceTypeData : IUmlTypeData
    {
        string InheritsTypeName { get; set; }
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
