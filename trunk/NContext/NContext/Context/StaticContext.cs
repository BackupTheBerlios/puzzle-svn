using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;

namespace Puzzle.NContext.Framework
{
    public partial class Context
    {
        //public static IContext Configure()
        //{            
        //    XmlElement xmlRoot = (XmlElement)ConfigurationManager.GetSection("ncontext");

        //    IContext context = new Context();
        //    foreach (XmlNode node in xmlRoot)
        //    {
        //        if (node.Name == "objectFactory")
        //        {
        //            Type factoryType = Type.GetType(node.Attributes["type"].Value);
        //            context.RegisterTemplate(factoryType);
        //        }
        //    }

            
        //    return context;
        //}
    }
}
