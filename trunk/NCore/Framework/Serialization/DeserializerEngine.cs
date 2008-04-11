using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Puzzle.NCore.Runtime.Serialization
{
	public class DeserializerEngine
	{
        public object Deserialize(Stream input)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(input);

            foreach (XmlNode node in doc["document"]["objects"])
            {
                if (node.Name == "object")
                {
                }
                else if (node.Name == "list")
                {
                }
                else if (node.Name == "array")
                {
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            return null;
        }
	}
}
