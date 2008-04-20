using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public interface IUmlClassData : IUmlInstanceTypeData
    {
        string InheritsTypeName { get; set; }
        bool IsAbstract { get; set; }
    }
}
