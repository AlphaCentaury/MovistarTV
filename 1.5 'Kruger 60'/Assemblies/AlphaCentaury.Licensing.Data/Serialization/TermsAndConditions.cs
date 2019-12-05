// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class TermsAndConditions: IXmlSerializable
    {
        //[XmlAttribute("format")]
        public string Format { get; set; }

        //[XmlAttribute("type")]
        public string Type { get; set; }

        //[XmlText]
        public string Text { get; set; }

        #region IXmlSerializable

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        } // IXmlSerializable.GetSchema

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.MoveToAttribute("format"))
            {
                Format = reader.Value;
                reader.MoveToElement();
            } // if

            if (reader.MoveToAttribute("type"))
            {
                Type = reader.Value;
                reader.MoveToElement();
            } // if

            Text = reader.ReadElementContentAsString();
        } // IXmlSerializable.ReadXml

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (Format != null) writer.WriteAttributeString("format", Format);
            if (Type != null) writer.WriteAttributeString("type", Type);

            if (Text == null) return;
            if (Text.Contains("\r") || Text.Contains("\n"))
            {
                writer.WriteCData(Text);
            }
            else
            {
                writer.WriteString(Text);
            } // if-else
        } // IXmlSerializable.WriteXml

        #endregion
    } // class TermsAndConditions
} // namespace
