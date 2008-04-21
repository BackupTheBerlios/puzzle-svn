using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlbinoHorse.Model;
using GenerationStudio.Elements;
using System.Drawing;

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

                if (Owner is EnumValueElement)
                    return "Values";

                return "";
            }
        }

        private static Dictionary<string, Image> imageLookup = new Dictionary<string, Image>();
        public Image GetImage()
        {
            string iconKey = Owner.GetIconKey ();

            Image res = null;
            if (!imageLookup.TryGetValue(iconKey, out res))
            {
                res = Owner.GetIcon();
                imageLookup.Add(iconKey, res);
            }

            return res;
        }

        public object DataObject
        {
            get { return Owner; }
        }
    }
}
