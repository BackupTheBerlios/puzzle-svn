using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace NObjectStore
{
    [Serializable]
    public class SerializedObject
    {
        private Type type;

        public Type Type
        {
            get { return type; }
            set { type = value; }
        }

        private Hashtable data;

        public Hashtable Data
        {
            get { return data; }
            set { data = value; }
        }

        public static string Serialize(object message)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            f.Serialize(stream, message);
            stream.Flush();
            stream.Position = 0;

            byte[] bytes = stream.ToArray();
            string xml = Convert.ToBase64String(bytes);
            return xml;
        }

        public static SerializedObject DeSerialize(string messageBody)
		{
			byte[] bytes = Convert.FromBase64String (messageBody);
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			stream.Write (bytes,0,bytes.Length);
			stream.Flush ();
			stream.Position = 0;
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			object result = f.Deserialize (stream);
			return (SerializedObject)result;
		}
    }
}
