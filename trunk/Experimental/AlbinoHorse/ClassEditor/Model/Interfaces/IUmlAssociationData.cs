using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public enum UmlPortSide
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    public enum UmlAssociationType
    {
        None,
        Association,
        Aggregation,
        Inheritance,        
    }

    public interface IUmlAssociationData
    {
        Shape Start { get; }
        int StartPortId { get; }
        UmlPortSide StartPortSide { get; }
        UmlAssociationType AssociationType { get; }
        
        Shape End { get; }
        int EndPortId { get; }
        UmlPortSide EndPortSide { get; }

    }
}
