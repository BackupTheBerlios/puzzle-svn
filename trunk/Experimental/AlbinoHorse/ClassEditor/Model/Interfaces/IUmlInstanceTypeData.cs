using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public interface IUmlInstanceTypeData : IUmlTypeData
    {
        string InheritsTypeName { get; set; }
        bool IsAbstract { get; set; }

        UmlTypeMember CreateProperty();
        void RemoveProperty(UmlTypeMember property);
        int GetPropertyCount();
        IEnumerable<UmlTypeMember> GetProperties();
    }
}
