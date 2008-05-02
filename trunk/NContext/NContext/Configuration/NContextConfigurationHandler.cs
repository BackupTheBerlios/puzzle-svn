using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Mojo
{
    public class MojoConfigurationHandler : IConfigurationSectionHandler
    {
        public MojoConfigurationHandler()
        {
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}
