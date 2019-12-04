// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class XmlText : IXmlSerializable
    {
        public XmlText()
        {
            // no-op
        } // constructor

        public XmlText(string text)
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

        public static implicit operator XmlText(string text) => new XmlText(text);
        public static implicit operator string(XmlText xmlCData) => xmlCData.Text;
    } // class XmlCDataText
} // namespace
