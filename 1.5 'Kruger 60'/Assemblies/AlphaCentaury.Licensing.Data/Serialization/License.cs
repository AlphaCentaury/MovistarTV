// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class License : IEquatable<License>
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("format")]
        public string Format { get; set; }

        [XmlText]
        public string Text { get; set; }

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

        public override string ToString()
        {
            return Id;
        } // ToString

        public static bool operator ==(License left, License right)
        {
            return left?.Equals(right) ?? false;
        } // operator ==

        public static bool operator !=(License left, License right)
        {
            return !(left == right);
        } // operator !=
    } // class License
} // namespace
