// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class MultilineText : IXmlSerializable, IEquatable<MultilineText>, IEquatable<string>, IComparable<MultilineText>, IComparable<string>, IComparable
    {
        public MultilineText()
        {
            // no-op
        } // constructor

        public MultilineText(string text)
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

        public static implicit operator MultilineText(string text) => new MultilineText(text);
        public static implicit operator string(MultilineText multiline) => multiline?.Text;

        #region Equality members

        public bool Equals(MultilineText other)
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
            return ReferenceEquals(this, obj) || (obj is MultilineText other && Equals(other)) || (obj is string text && Equals(text));
        } // Equals

        public override int GetHashCode()
        {
            return (Text != null ? StringComparer.CurrentCulture.GetHashCode(Text) : 0);
        } // GetHashCode

        public static bool operator ==(MultilineText left, MultilineText right)
        {
            if (ReferenceEquals(left, right)) return true;
            return !(left is null) && left.Equals(right);
        } // operator ==

        public static bool operator !=(MultilineText left, MultilineText right)
        {
            return !(left == right);
        } // operator !=

        public static bool operator ==(MultilineText left, string right)
        {
            return !(left is null) && left.Equals(right);
        } // operator ==

        public static bool operator !=(MultilineText left, string right)
        {
            return !(left == right);
        } // operator !=

        public static bool operator ==(string left, MultilineText right)
        {
            return !(right is null) && right.Equals(left);
        } // operator ==

        public static bool operator !=(string left, MultilineText right)
        {
            return !(left == right);
        } // operator !=

        #endregion

        #region Relational members: MultilineText

        public int CompareTo(MultilineText other)
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
                case MultilineText other:
                    return CompareTo(other);
                case string text:
                    return CompareTo(text);
                default:
                    throw new ArgumentException($"CompareTo: Object must be of type {nameof(MultilineText)} -or- {nameof(String)}");
            } // switch
        } // CompareTo

        public static bool operator <(MultilineText left, MultilineText right)
        {
            return Comparer<MultilineText>.Default.Compare(left, right) < 0;
        } // operator <

        public static bool operator >(MultilineText left, MultilineText right)
        {
            return Comparer<MultilineText>.Default.Compare(left, right) > 0;
        } // operator >

        public static bool operator <=(MultilineText left, MultilineText right)
        {
            return Comparer<MultilineText>.Default.Compare(left, right) <= 0;
        } // operator <=

        public static bool operator >=(MultilineText left, MultilineText right)
        {
            return Comparer<MultilineText>.Default.Compare(left, right) >= 0;
        } // operator >=

        #endregion

        #region Relational members: string

        public int CompareTo(string other)
        {
            return (other is null) ? 1 : string.Compare(Text, other, StringComparison.CurrentCulture);
        } // CompareTo

        #endregion
    } // class MultilineText
} // namespace
