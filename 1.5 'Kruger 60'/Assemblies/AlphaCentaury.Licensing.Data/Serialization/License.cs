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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public sealed class License : IXmlSerializable, IEquatable<License>, ICloneable<License>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Format { get; set; }

        public string Text { get; set; }

        public override string ToString() => Id;

        public License Clone()
        {
            return new License
            {
                Id = Id,
                Name = Name,
                Format = Format,
                Text = Text
            };
        } // Clone

        object ICloneable.Clone() => Clone();

        public void Inherit(License from)
        {
            if (from == null) return;

            Id ??= from.Id;
            Name ??= from.Name;
            Format ??= from.Format;
            Text ??= from.Format;
        } // Inherit

        #region IEquatable<License>

        public bool Equals(License other)
        {
            return string.Compare(Id, other?.Id, StringComparison.InvariantCulture) == 0;
        } // Equals

        public override int GetHashCode()
        {
            unchecked
            {
                return 539060726 + Id.GetHashCode();
            } // unchecked
        } // GetHashCode

        public override bool Equals(object obj)
        {
            if ((obj is null) || !(obj is License other)) return false;

            return Equals(other);
        } // Equals

        public static bool operator ==(License left, License right)
        {
            if (ReferenceEquals(left, right)) return true;
            return left?.Equals(right) ?? false;
        } // operator ==

        public static bool operator !=(License left, License right)
        {
            return !(left == right);
        } // operator !=

        #endregion

        #region IXmlSerializable

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        } // IXmlSerializable.GetSchema

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.MoveToAttribute("id"))
            {
                Id = reader.Value;
                reader.MoveToElement();
            } // if

            if (reader.MoveToAttribute("name"))
            {
                Name = reader.Value;
                reader.MoveToElement();
            } // if

            if (reader.MoveToAttribute("format"))
            {
                Format = reader.Value;
                reader.MoveToElement();
            } // if

            Text = reader.ReadElementContentAsString();
        } // IXmlSerializable.ReadXml

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (Id != null) writer.WriteAttributeString("id", Id);
            if (Name != null) writer.WriteAttributeString("name", Name);
            if (Format != null) writer.WriteAttributeString("format", Format);

            if (Text == null) return;
            if (Text.Contains("'\r") || Text.Contains("\n"))
            {
                writer.WriteCData(Text);
            }
            else
            {
                writer.WriteString(Text);
            } // if-else
        } // IXmlSerializable.WriteXml

        #endregion
    } // class License
} // namespace
