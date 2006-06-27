using System;
using System.IO;
using System.Xml;

namespace Puzzle.NCore.Runtime.Serialization
{
    public class ValueObject : ObjectBase
    {
        public string Value;

        public override string ToString()
        {
            return string.Format("'{0}' : {1}", Value, Type.Name);
        }

        public override void Serialize(XmlTextWriter xml)
        {
            
            //xml.WriteStartElement ("value");
            //xml.WriteAttributeString ("id",ID.ToString());
            //xml.WriteAttributeString ("value",Value);
            //xml.WriteAttributeString ("type",Type.FullName);
            //xml.WriteEndElement ();
        }

        public override void SerializeReference(XmlTextWriter xml)
        {
            xml.WriteAttributeString ("value",Value);
            xml.WriteAttributeString ("type",Type.FullName);
        }

        public override object GetValue()
        {
            object res = Convert.ChangeType(Value,Type);
            return res;
        }
    }
}