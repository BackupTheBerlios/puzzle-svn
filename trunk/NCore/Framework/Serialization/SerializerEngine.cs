using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Puzzle.NCore.Runtime.Serialization
{
    public class SerializerEngine
    {
        public Hashtable objectLoookup = new Hashtable();
        private IList allObjects = new ArrayList();
        public ObjectBase Root;
        private int objectID = 0;

        private int GetObjectID()
        {
            return objectID++;
        }

        public void Serialize(Stream output, object graph)
        {
            BuildSerilizationGraph(graph);
            XmlTextWriter xml = new XmlTextWriter(output, Encoding.Default);
            xml.Formatting = Formatting.Indented;
            xml.Indentation = 1;
            xml.IndentChar = '\t';
            xml.WriteStartElement("document");
            xml.WriteStartElement("root-object");
            Root.SerializeReference(xml);
            xml.WriteEndElement();
            xml.WriteStartElement("objects");
            foreach (ObjectBase item in allObjects)
            {
                item.Serialize(xml);
            }
            xml.WriteEndElement();
            xml.WriteEndElement();
            xml.Flush();
        }


        private void BuildSerilizationGraph(object graph)
        {
            Root = GetObject(graph);
        }

        public ObjectBase GetObject(object item)
        {
            //return null
            if (item == null)
                return NullObject.Default;

            //dont serialize more than once
            if (objectLoookup.ContainsKey(item))
                return (ObjectBase) objectLoookup[item];

            if (IsValueObject(item))
            {
                return BuildValueObject(item);
            }
            else if (item is IList)
            {
                return BuildIListObject((IList)item);
            }
            else if (item.GetType().IsArray)
            {
                return BuildArrayObject((Array)item);
            }
            else
            {
                return BuildReferenceObject(item);
            }
        }

        private static bool IsValueObject(object item)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(item.GetType());
            return tc.CanConvertFrom(typeof (string)) || item is Type;
        }

        private ArrayObject BuildArrayObject(Array item)
        {
            ArrayObject current = new ArrayObject();
            RegisterObject(current, item);
            current.Build(this, item);
            return current;
        }

        private IListObject BuildIListObject(IList item)
        {
            IListObject current = new IListObject();
            RegisterObject(current, item);
            current.Build(this, item);
            return current;
        }

        private ObjectBase BuildValueObject(object item)
        {
            ValueObject current = new ValueObject();
            RegisterObject(current, item);
            TypeConverter tc = TypeDescriptor.GetConverter(item.GetType());
            current.Value = tc.ConvertToString(item);
            return current;
        }

        private ReferenceObject BuildReferenceObject(object item)
        {
            ReferenceObject current = new ReferenceObject();
            RegisterObject(current, item);

            //let readers of the serialize data know that this should be treated as a list
            if (item is IEnumerable)
                current.IsEnumerable = true;

            Type currentType = item.GetType();
            while (currentType != null)
            {
                FieldInfo[] fields = currentType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                foreach (FieldInfo fieldInfo in fields)
                {
                    if (IsNonSerialized(fieldInfo))
                        continue;

                    Field field = new Field();
                    current.Fields.Add(field);
                    field.Name = fieldInfo.Name;
                    object fieldValue = fieldInfo.GetValue(item);
                    field.Value = GetObject(fieldValue);
                }
                currentType = currentType.BaseType;
            }

            return current;
        }

        private static bool IsNonSerialized(FieldInfo field)
        {
            return field.GetCustomAttributes(typeof (NonSerializedAttribute), true).Length > 0;
        }

        private void RegisterObject(ObjectBase current, object item)
        {
            objectLoookup.Add(item, current);
            current.Type = item.GetType();
            current.ID = GetObjectID();
            allObjects.Add(current);
        }
    }
}