using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace NObjectStore
{
    public class SerializedObject
    {
        public Type Type { get; set; }

        public PropertyDictionary Data { get; set; }
    }

    
}
