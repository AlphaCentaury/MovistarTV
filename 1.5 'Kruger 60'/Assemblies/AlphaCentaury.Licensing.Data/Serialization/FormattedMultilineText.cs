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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class FormattedMultilineText : IXmlSerializable, ICloneable<FormattedMultilineText>, IEquatable<FormattedMultilineText>, IEquatable<string>, IComparable<FormattedMultilineText>, IComparable<string>, IComparable
    {
        public FormattedMultilineText()
        {
            // no-op
        } // constructor

        public FormattedMultilineText(string text, string format)
        {
            Text = text;
            Format = format;
        } // constructor

        public string Format { get; set; }

        public string Text { get; set; }

        public override string ToString() => Text;

        public FormattedMultilineText Clone()
        {
            return new FormattedMultilineText(Text, Format);
        } // Clone

        object ICloneable.Clone() => Clone();

        protected void CopyTo(FormattedMultilineText other)
        {
            other.Text = Text;
            other.Format = Format;
        } // CopyTo

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        } // IXmlSerializable.GetSchema

        void IXmlSerializable.ReadXml(XmlReader reader) => ReadXml(reader);

        void IXmlSerializable.WriteXml(XmlWriter writer) => WriteXml(writer);

        protected virtual void ReadXml([NotNull] XmlReader reader)
        {
            if (reader.MoveToAttribute("format"))
            {
                Format = reader.Value;
                reader.MoveToElement();
            } // if

            Text = reader.ReadElementContentAsString();
        } // ReadXml

        protected virtual void WriteXml([NotNull] XmlWriter writer)
        {
            if (string.IsNullOrWhiteSpace(Text)) return;
            if (Format != null) writer.WriteAttributeString("format", Format);

            if (Text.Contains("\r") || Text.Contains("\n"))
            {
                writer.WriteCData(Text);
            }
            else
            {
                writer.WriteString(Text);
            } // if-else
        } // WriteXml

        public static implicit operator FormattedMultilineText(string text) => new FormattedMultilineText(text, null);

        public static implicit operator string(FormattedMultilineText multiline) => multiline?.Text;

        #region Equality members

        public bool Equals(FormattedMultilineText other)
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
            return ReferenceEquals(this, obj) || (obj is FormattedMultilineText other && Equals(other)) || (obj is string text && Equals(text));
        } // Equals

        public override int GetHashCode()
        {
            return (Text != null ? StringComparer.CurrentCulture.GetHashCode(Text) : 0);
        } // GetHashCode

        public static bool operator ==(FormattedMultilineText left, FormattedMultilineText right)
        {
            if (ReferenceEquals(left, right)) return true;
            return !(left is null) && left.Equals(right);
        } // operator ==

        public static bool operator !=(FormattedMultilineText left, FormattedMultilineText right)
        {
            return !(left == right);
        } // operator !=

        public static bool operator ==(FormattedMultilineText left, string right)
        {
            return !(left is null) && left.Equals(right);
        } // operator ==

        public static bool operator !=(FormattedMultilineText left, string right)
        {
            return !(left == right);
        } // operator !=

        public static bool operator ==(string left, FormattedMultilineText right)
        {
            return !(right is null) && right.Equals(left);
        } // operator ==

        public static bool operator !=(string left, FormattedMultilineText right)
        {
            return !(left == right);
        } // operator !=

        #endregion

        #region Relational members: MultilineText

        public int CompareTo(FormattedMultilineText other)
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
                case FormattedMultilineText other:
                    return CompareTo(other);
                case string text:
                    return CompareTo(text);
                default:
                    throw new ArgumentException($"CompareTo: Object must be of type {nameof(FormattedMultilineText)} -or- {nameof(String)}");
            } // switch
        } // CompareTo

        public static bool operator <(FormattedMultilineText left, FormattedMultilineText right)
        {
            return Comparer<FormattedMultilineText>.Default.Compare(left, right) < 0;
        } // operator <

        public static bool operator >(FormattedMultilineText left, FormattedMultilineText right)
        {
            return Comparer<FormattedMultilineText>.Default.Compare(left, right) > 0;
        } // operator >

        public static bool operator <=(FormattedMultilineText left, FormattedMultilineText right)
        {
            return Comparer<FormattedMultilineText>.Default.Compare(left, right) <= 0;
        } // operator <=

        public static bool operator >=(FormattedMultilineText left, FormattedMultilineText right)
        {
            return Comparer<FormattedMultilineText>.Default.Compare(left, right) >= 0;
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
