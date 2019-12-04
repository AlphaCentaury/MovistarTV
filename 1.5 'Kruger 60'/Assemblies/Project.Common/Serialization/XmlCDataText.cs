using System;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace IpTviewr.Common.Serialization
{
    [Serializable]
    public class XmlCDataText : IXmlSerializable
    {
        public XmlCDataText()
        {
            // no-op
        } // constructor

        public XmlCDataText(string text)
        {
            Text = text;
        } // constructor

        [XmlIgnore]
        public string Text { get; set; }

        public override string ToString() => Text;

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        } // IXmlSerializable.GetSchema

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            Text = reader.ReadElementContentAsString();
        } // IXmlSerializable.ReadXml

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (Text == null) return;
            if (Text.Contains('\r') || Text.Contains('\n'))
            {
                writer.WriteCData(Text);
            }
            else
            {
                writer.WriteString(Text);
            } // if-else
        } // IXmlSerializable.WriteXml

        public static implicit operator XmlCDataText(string text) => new XmlCDataText(text);
        public static implicit operator string(XmlCDataText xmlCData) => xmlCData.Text;
    } // class XmlCDataText
} // namespace
