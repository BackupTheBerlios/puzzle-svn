using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AlbinoHorse.Model
{
    public class DefaultUmlTypeMemberData : IUmlTypeMemberData
    {
        public string Name { get; set; }
        public string SectionName { get; set; }

        public object DataObject
        {
            get
            {
                return null;
            }
        }
        public Image GetImage()
        {
            return AlbinoHorse.ClassDesigner.Properties.Resources.Property;
        }
    }
}
