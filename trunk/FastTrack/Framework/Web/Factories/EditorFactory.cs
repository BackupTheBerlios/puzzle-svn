using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Web.UI;
using System.Collections;
using Puzzle.FastTrack.Framework.Web.Controls;

namespace Puzzle.FastTrack.Framework.Web.Factories
{
    public class EditorFactory
    {

        public static Control GetPropertyValueEditor(PropertyInfo property)
        {
            Control editor = null;

            if (typeof(IList).IsAssignableFrom(property.PropertyType))
            {
                editor = new ListEditor(property.Name);
            }
            else
            {
                Type type = property.PropertyType;

                if (type.IsEnum)
                    editor = new EnumerationEditor(property.Name);

                else if (type.IsPrimitive)
                {
                    if (type == typeof(bool))
                        editor = new BooleanEditor(property.Name);


                    else if (type == typeof(Int16)
                       || type == typeof(Int32)
                       || type == typeof(Int64)
                       || type == typeof(Byte))
                        editor = new NumberEditor(property.Name);
                }
                else if (type.IsValueType)
                {
                    if (type == typeof(DateTime))
                        editor = new DateTimeEditor(property.Name);

                    else if (type == typeof(Decimal))
                        editor = new NumberEditor(property.Name);
                }
                else if (type.IsClass)
                {
                    if (type == typeof(string))
                        editor = new StringEditor(property.Name);


                    else if (type == typeof(byte[]))
                        ; //editor = new DateTimeEditor(property.Name);
                    
                    else
                    {
                        editor = new ReferenceEditor(property.Name);
                    }
                }
            }

            return editor;
        }
    }
}
