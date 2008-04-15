using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlPropertyData : IUmlPropertyData
    {
        public PropertyElement Owner { get; set; }

        public string Name
        {
            get
            {
                return Owner.Name;
            }
            set
            {
                Owner.Name = value;
            }
        }

        public string Type
        {
            get
            {
                return Owner.Type;
            }
            set
            {
                Owner.Type = value;
            }
        }

        public System.Drawing.Image GetImage()
        {
            return Owner.GetIcon ();
        }

        public object DataObject
        {
            get { return Owner; }
        }
    }
}
