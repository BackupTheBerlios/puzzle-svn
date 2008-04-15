using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Property")]
    [ElementIcon("GenerationStudio.Images.property.gif")]
    public class PropertyElement : TypeMemberElement
    {
        private string type;
        public string Type {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnNotifyChange();
            }
        }

        public override IList<ElementError> GetErrors()
        {
            List<ElementError> errors = new List<ElementError>();
            if (string.IsNullOrEmpty(Type))
                errors.Add(new ElementError(this, string.Format("Property {0}.{1} is missing Type", Parent.GetDisplayName(), GetDisplayName())));

            return errors;
        }
    }
}
