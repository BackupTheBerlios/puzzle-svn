using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public interface IUmlTypeData
    {
        string TypeName { get; set; }
        string InheritsType { get; set; }

        void AddProperty(UmlProperty property);
        void RemoveProperty(UmlProperty property);
        int GetPropertyCount();
        IEnumerable<UmlProperty> GetProperties();

        void AddMethod(UmlMethod method);
        void RemoveMethod(UmlMethod method);
        int GetMethodCount();
        IEnumerable<UmlMethod> GetMethods();

    }
}
