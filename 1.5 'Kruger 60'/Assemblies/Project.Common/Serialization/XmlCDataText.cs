// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace IpTviewr.Common.Serialization
{
    [Serializable]
    public class XmlCDataText : IXmlSerializable, IEquatable<XmlCDataText>, IEquatable<string>, IComparable<XmlCDataText>, IComparable<string>, IComparable
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

        #region Equality members

        public bool Equals(XmlCDataText other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Text, other.Text, StringComparison.CurrentCulture);
        } // Equals

        public bool Equals(string other)
        {
            if (other is null) return false;
            return string.Equals(Text, other, StringComparison.CurrentCulture);
        } // Equals

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || (obj is XmlCDataText other && Equals(other)) || (obj is string text && Equals(text));
        } // Equals

        public override int GetHashCode()
        {
            return (Text != null ? StringComparer.CurrentCulture.GetHashCode(Text) : 0);
        } // GetHashCode

        public static bool operator ==(XmlCDataText left, XmlCDataText right)
        {
            if (ReferenceEquals(left, right)) return true;
            return !(left is null) && left.Equals(right);
        } // operator ==

        public static bool operator !=(XmlCDataText left, XmlCDataText right)
        {
            return !(left == right);
        } // operator !=

        public static bool operator ==(XmlCDataText left, string right)
        {
            return !(left is null) && left.Equals(right);
        } // operator ==

        public static bool operator !=(XmlCDataText left, string right)
        {
            return !(left == right);
        } // operator !=

        public static bool operator ==(string left, XmlCDataText right)
        {
            return !(right is null) && right.Equals(left);
        } // operator ==

        public static bool operator !=(string left, XmlCDataText right)
        {
            return !(left == right);
        } // operator !=

        #endregion

        #region Relational members: XmlCDataText

        public int CompareTo(XmlCDataText other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;
            return string.Compare(Text, other.Text, StringComparison.CurrentCulture);
        }

        public int CompareTo(object obj)
        {
            if (obj is null) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            switch (obj)
            {
                case XmlCDataText other:
                    return CompareTo(other);
                case string text:
                    return CompareTo(text);
                default:
                    throw new ArgumentException($"CompareTo: Object must be of type {nameof(XmlCDataText)} -or- {nameof(String)}");
            } // switch
        } // CompareTo

        public static bool operator <(XmlCDataText left, XmlCDataText right)
        {
            return Comparer<XmlCDataText>.Default.Compare(left, right) < 0;
        } // operator <

        public static bool operator >(XmlCDataText left, XmlCDataText right)
        {
            return Comparer<XmlCDataText>.Default.Compare(left, right) > 0;
        } // operator >

        public static bool operator <=(XmlCDataText left, XmlCDataText right)
        {
            return Comparer<XmlCDataText>.Default.Compare(left, right) <= 0;
        } // operator <=

        public static bool operator >=(XmlCDataText left, XmlCDataText right)
        {
            return Comparer<XmlCDataText>.Default.Compare(left, right) >= 0;
        } // operator >=

        #endregion

        #region Relational members: string

        public int CompareTo(string other)
        {
            return (other is null) ? 1 : string.Compare(Text, other, StringComparison.CurrentCulture);
        } // CompareTo

        #endregion
    } // class XmlCDataText
} // namespace
