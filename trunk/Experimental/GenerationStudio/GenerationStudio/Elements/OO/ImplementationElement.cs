﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(InstanceTypeElement))]
    [ElementName("Interface Implementation")]
    [ElementIcon("GenerationStudio.Images.implementation.bmp")]
    public class ImplementationElement : Element
    {
        public InterfaceElement Implements { get; set; }

        public override IList<ElementError> GetErrors()
        {
            List<ElementError> errors = new List<ElementError>();
            if (Implements == null)
                errors.Add(new ElementError(this, string.Format("Class {0} is missing an interface", Parent.GetDisplayName())));

            return errors;
        }

        public override string GetDisplayName()
        {
            string interfaceName = "*missing*";
            if (Implements != null)
                interfaceName = Implements.Name;

            return string.Format("Implements: {0}", interfaceName);
        }
    }
}
