﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Puzzle.NContext.Framework
{
    public class NContextConfigurationHandler : IConfigurationSectionHandler
    {
        public NContextConfigurationHandler()
        {
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}
