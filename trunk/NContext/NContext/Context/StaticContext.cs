using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;

namespace Mojo
{
    public static class Context
    {
        public static IContext<T> Configure<T>() where T : ITemplate 
        {
            XmlNode res = (XmlNode)ConfigurationManager.GetSection("mojo");
            foreach (XmlNode child in res)
            {
                if (child.Name == "template")
                {
                    Type contractType = Type.GetType (child.Attributes["contract"].Value);
                    Type implementationType = Type.GetType (child.Attributes["implementation"].Value);

                    if (contractType == typeof(T))
                    {
                        return new Context<T>(implementationType);
                    }
                }
            }

            //return default
            Context<T> context = new Context<T> ();
            return context;
        }

        public static IContext<T> Configure<T,I>() where T : ITemplate
        {
            Context<T> context = new Context<T>(typeof(I));
            return context;
        }
    }
}
