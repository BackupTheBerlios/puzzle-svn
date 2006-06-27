using System.Collections;
using System.IO;
using System.Xml;

namespace Puzzle.NCore.Runtime.Serialization
{
    public class ReferenceObject : ObjectBase
    {
        public bool IsEnumerable = false;
        public readonly IList Properties = new ArrayList();

        public override string ToString()
        {
            return string.Format("{0}#{1}", Type.Name, ID);
        }

        public override void Serialize(XmlTextWriter xml)
        {
            
            xml.WriteStartElement ("object");
            xml.WriteAttributeString ("id",ID.ToString());
            xml.WriteAttributeString ("type",Type.FullName);

            foreach (Field property in Properties)
            {
                property.Serialize(xml);
            }


            xml.WriteEndElement ();
        }

        public override void SerializeReference(XmlTextWriter xml)
        {
            xml.WriteAttributeString ("id-ref",ID.ToString());                        
        }
    }
}