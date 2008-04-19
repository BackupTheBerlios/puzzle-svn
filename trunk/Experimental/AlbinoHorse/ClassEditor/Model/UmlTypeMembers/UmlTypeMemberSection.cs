using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AlbinoHorse.Model
{
    public class UmlTypeMemberSection
    {
        public readonly object CaptionIdentifier = new object();
        public readonly object AddNewIdentifier = new object();

        public string Caption { get; set; }
        public bool Expanded { get; set; }

    }
}
