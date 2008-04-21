using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public interface IUmlAssociationData
    {
        Shape Start { get; }
        Shape End { get; }
    }
}
