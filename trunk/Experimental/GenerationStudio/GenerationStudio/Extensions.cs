using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using System.Drawing;
using System.IO;
using System.Reflection;
using GenerationStudio.Gui;

namespace GenerationStudio
{
    public static class Extensions
    {
        public static string GetElementName(this Type elementType)
        {
            ElementNameAttribute attrib =elementType.GetAttribute<ElementNameAttribute>();


            if (attrib == null)
                return "**" + elementType.Name;
            else
                return attrib.Name;
        }

        public static string GetVerbName(this MethodInfo method)
        {
            ElementVerbAttribute attrib = method.GetAttribute<ElementVerbAttribute>();


            if (attrib == null)
                return "**" + method.Name;
            else
                return attrib.Name;
        }

        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            object[] attribs = type.GetCustomAttributes(typeof(T), true);
            if (attribs.Length == 0)
                return null;

            return attribs[0] as T;
        }

        public static T GetAttribute<T>(this MethodInfo method) where T : Attribute
        {
            object[] attribs = method.GetCustomAttributes(typeof(T), true);
            if (attribs.Length == 0)
                return null;

            return attribs[0] as T;
        }

        public static Image GetElementIcon(this Type elementType)
        {
            ElementIconAttribute attrib = elementType.GetAttribute<ElementIconAttribute>();
            if (attrib == null)
                return null;

            Stream stream = elementType.Assembly.GetManifestResourceStream(attrib.ResourceName);

            Image img = Image.FromStream(stream);

            return img;
        }

        public static string GetElementIconName(this Type elementType)
        {
            ElementIconAttribute attrib = elementType.GetAttribute<ElementIconAttribute>();
            if (attrib == null)
                return "";

            return attrib.ResourceName;
        }

        public static List<MethodInfo> GetElementVerbs(this Type elementType)
        {
            var res = from method in elementType.GetMethods()
                      where                       
                      method.IsVerb()
                      orderby method.Name
                      select method;

            return res.ToList();
        }

        public static MethodInfo GetElementDefaultVerb(this Type elementType)
        {
            var verbs = elementType.GetElementVerbs();

            foreach (MethodInfo verb in verbs)
            {
                if (verb.IsDefaultVerb())
                    return verb;
            }
            return null;
        }

        public static bool IsVerb(this MethodInfo method)
        {
            if (method.GetParameters().Length != 1)
                return false;

            if (method.GetParameters()[0].ParameterType != typeof(IHost))
                return false;

            if (method.ReturnType != typeof(void))
                return false;

            ElementVerbAttribute attrib = method.GetAttribute<ElementVerbAttribute>();
            if (attrib == null)
                return false;

            return true;
        }

        public static bool IsDefaultVerb(this MethodInfo method)
        {
            if (method.GetParameters().Length != 1)
                return false;

            if (method.GetParameters()[0].ParameterType != typeof(IHost))
                return false;

            if (method.ReturnType != typeof(void))
                return false;

            ElementVerbAttribute attrib = method.GetAttribute<ElementVerbAttribute>();
            if (attrib == null)
                return false;

            if (attrib.Default)
                return true;

            return false;
        }


    }
}
