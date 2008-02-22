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
        public static Control GetPropertyValueEditor(FastTrackPage page, object obj, PropertyInfo property)
        {
            return GetPropertyValueEditor(page, obj.GetType(), property);
        }

        public static Control GetPropertyValueEditor(FastTrackPage page, Type type, PropertyInfo property)
        {
            Control editor = null;

            if (page.IsListProperty(type, property.Name))
            {
                editor = new ListEditor(property.Name);
            }
            else
            {
                Type propertyType = property.PropertyType;

                if (propertyType.IsEnum)
                    editor = new EnumerationEditor(property.Name);

                else if (propertyType.IsPrimitive)
                {
                    if (propertyType == typeof(bool))
                        editor = new BooleanEditor(property.Name);


                    else if (propertyType == typeof(Int16)
                       || propertyType == typeof(Int32)
                       || propertyType == typeof(Int64)
                       || propertyType == typeof(Byte))
                        editor = new NumberEditor(property.Name);
                }
                else if (propertyType.IsValueType)
                {
                    if (propertyType == typeof(DateTime))
                        editor = new DateTimeEditor(property.Name);

                    else if (propertyType == typeof(Decimal))
                        editor = new NumberEditor(property.Name);
                }
                else if (propertyType.IsClass)
                {
                    if (propertyType == typeof(string))
                        editor = new StringEditor(property.Name);


                    else if (propertyType == typeof(byte[]))
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
