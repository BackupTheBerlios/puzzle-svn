using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace NObjectStore
{
    public interface IPersistentList
    {
        IPersistentObject Owner { get;set;}
        IList GetRawData();
    }
}
