// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public sealed class TermsAndConditions : IXmlSerializable, IEquatable<TermsAndConditions>
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

        #region Equality members

        public bool Equals(TermsAndConditions other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Format, other.Format, StringComparison.InvariantCulture) &&
                   string.Equals(Type, other.Type, StringComparison.InvariantCulture) &&
                   string.Equals(Text, other.Text, StringComparison.InvariantCulture);
        } // Equals

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || (obj is TermsAndConditions other && Equals(other));
        } // Equals

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Format != null ? StringComparer.InvariantCulture.GetHashCode(Format) : 0);
                hashCode = (hashCode * 397) ^ (Type != null ? StringComparer.InvariantCulture.GetHashCode(Type) : 0);
                hashCode = (hashCode * 397) ^ (Text != null ? StringComparer.InvariantCulture.GetHashCode(Text) : 0);
                return hashCode;
            } // unchecked
        } // GetHashCode

        public static bool operator ==(TermsAndConditions left, TermsAndConditions right)
        {
            return Equals(left, right);
        } // operator ==

        public static bool operator !=(TermsAndConditions left, TermsAndConditions right)
        {
            return !Equals(left, right);
        } // operator !=

        #endregion
    } // class TermsAndConditions
} // namespace
