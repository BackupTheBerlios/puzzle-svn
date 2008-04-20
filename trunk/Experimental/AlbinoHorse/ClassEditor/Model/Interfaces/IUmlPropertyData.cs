using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AlbinoHorse.Model
{
    public interface IUmlTypeMemberData
    {
        string Name { get; set; }
        string SectionName { get; }
        Image GetImage();
        object DataObject { get; }
    }
}
