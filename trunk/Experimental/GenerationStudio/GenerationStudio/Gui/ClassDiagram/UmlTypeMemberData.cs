using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlTypeMemberData : IUmlTypeMemberData
    {
        public TypeMemberElement Owner { get; set; }

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

        public string SectionName
        {
            get
            {
                if (Owner is PropertyElement)
                    return "Properties";

                if (Owner is MethodElement)
                    return "Methods";

                return "";
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
