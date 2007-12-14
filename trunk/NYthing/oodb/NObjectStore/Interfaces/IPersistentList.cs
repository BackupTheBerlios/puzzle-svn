using System;
using System.Collections.Generic;
using System.Text;

namespace NObjectStore
{
    public interface IPersistentList
    {
        IPersistentObject Owner { get;set;}

    }
}
