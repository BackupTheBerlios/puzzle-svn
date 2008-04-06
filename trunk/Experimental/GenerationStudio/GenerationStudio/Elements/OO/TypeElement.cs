﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(NamespaceElement))]
    [ElementIcon("GenerationStudio.Images.table.bmp")]
    public abstract class TypeElement : NamedElement
    {
        public override bool GetDefaultExpanded()
        {
            return false;
        }
    }
}
