using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NObjectStore
{
    public static class JSONSerializer
    {
        public static string Serialize(SerializedObject message)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendFormat("\t\"class\": \"{0}\"", message.Type.AssemblyQualifiedName);
            foreach (var entry in message.Data)
            {
                sb.AppendLine(",");

                string value = Serialize(entry.Value);

                sb.AppendFormat("\t\"{0}\": {1}", entry.Key, value);
            }
            sb.AppendLine();
            sb.Append("}");

            return sb.ToString();
        }

        public static string Serialize(object item)
        {
            string value = "";

            if (item == null)
                value = "{null}";

            else if (item is string)
                value = Serialize((string)item);

            else if (item is PersistentId)
                value = Serialize((PersistentId)item);

            else if (item is IPersistentList)
                value = Serialize((IPersistentList)item);

            else
                value = item.ToString();

            return value;
        }

        public static string Serialize(IPersistentList list)
        {
            IList data = list.GetRawData();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("\t[");
            bool first = true;
            foreach (object o in data)
            {
                string value = Serialize(o);
                sb.AppendFormat("\t\t{0}",value);

                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.AppendLine(",");
                }
            }
            sb.AppendLine();
            sb.Append("\t]");

            return sb.ToString();
        }

        public static string Serialize(PersistentId id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"Id\": \"{0}\"",id.Id);
            sb.Append("}");

            return sb.ToString();
        }

        public static string Serialize(string text)
        {
            text = text.Replace(@"\", @"\\");
            text = text.Replace ("\r","\\r");
            text = text.Replace("\n", "\\n");
            text = text.Replace("\"", "\\\"");

            return "\"" + text + "\"";
        }

        public static SerializedObject DeSerialize(string messageBody)
        {
            byte[] bytes = Convert.FromBase64String(messageBody);
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Position = 0;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            object result = f.Deserialize(stream);
            return (SerializedObject)result;
        }
    }
}
