using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AlbinoHorse.Model
{
    public interface IUmlPropertyData
    {
        string Name { get; set; }
        string Type { get; set; }
        Image GetImage();
        object DataObject { get; }
    }
}
