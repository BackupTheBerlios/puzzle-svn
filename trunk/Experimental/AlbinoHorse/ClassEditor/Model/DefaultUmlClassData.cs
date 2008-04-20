using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public class DefaultUmlClassData : DefaultUmlInstanceTypeData, IUmlInstanceTypeData
    {
        public string InheritsTypeName { get; set; }
        public bool IsAbstract { get; set; }       
    }
}
