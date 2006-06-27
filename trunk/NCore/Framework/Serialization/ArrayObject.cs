using System.Collections;
using System.Xml;
using System;

namespace Puzzle.NCore.Runtime.Serialization
{
	public class ArrayObject : ObjectBase
	{
		public IList Items = new ArrayList();


		public override string ToString()
		{
			return string.Format("Count = {0} : {1}", Items.Count, Type.Name);
		}

		public override void Serialize(XmlTextWriter xml)
		{
			xml.WriteStartElement("array");
			xml.WriteAttributeString("id", ID.ToString());
			xml.WriteAttributeString("type", Type.FullName);

			foreach (ObjectBase element in Items)
			{
				xml.WriteStartElement("element");
				element.SerializeReference(xml);
				xml.WriteEndElement();
			}

			xml.WriteEndElement();
		}

		public override void SerializeReference(XmlTextWriter xml)
		{
			xml.WriteAttributeString("id-ref", ID.ToString());
		}


        public override object GetValue()
        {            
            Type elementType = Type.GetElementType();
            object arr = Array.CreateInstance (elementType,Items.Count);
            for (int i = 0;i<Items.Count;i++)
            {
                ObjectBase element = (ObjectBase)Items[i];               
                ((Array)arr).SetValue(element.GetValue(),i);
            }

            return arr;
        }
    }
}