﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(EnumElement))]
    [ElementName("Enumeration Value")]
    [ElementIcon("GenerationStudio.Images.enumvalue.gif")]
    public class EnumValueElement : TypeMemberElement
    {
        public int Value { get; set; }
    }
}
