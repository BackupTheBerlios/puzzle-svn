using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerationStudio.Elements
{
    public class ElementError
    {
        public Element Owner { get; set; }
        public string Message { get; set; }

        public ElementError()
        {
        }

        public ElementError(Element owner, string message)
        {
            Owner = owner;
            Message = message;
        }
    }
}
