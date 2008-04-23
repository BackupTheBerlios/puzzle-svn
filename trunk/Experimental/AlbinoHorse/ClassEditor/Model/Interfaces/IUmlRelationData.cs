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

    public enum UmlRelationType
    {
        None,
        Association,
        Aggregation,
        Inheritance,        
    }

    public interface IUmlRelationData
    {
        Shape Start { get; }
        int StartPortId { get; }
        UmlPortSide StartPortSide { get; }
        UmlRelationType AssociationType { get; }
        
        Shape End { get; }
        int EndPortId { get; }
        UmlPortSide EndPortSide { get; }

    }
}
