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
using System.ComponentModel;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public sealed class ThirdPartyDependency: LicensedComponentDependency, ICloneable<ThirdPartyDependency>, IEquatable<ThirdPartyDependency>
    {
        [XmlAttribute("type")]
        [DefaultValue(ThirdPartyDependencyType.Other)]
        public ThirdPartyDependencyType Type { get; set; }

        public string Description { get; set; }

        public override string ToString() => $"{DependencyTypeMark}{Type}:{Name}";

        public ThirdPartyDependency Clone()
        {
            var clone = new ThirdPartyDependency
            {
                Type = Type,
                Description = Description,
            };
            CopyToDependency(clone);

            return clone;
        } // Clone

        object ICloneable.Clone() => Clone();

        #region Equality members

        public bool Equals(ThirdPartyDependency x, ThirdPartyDependency y)
        {
            return (x?.Equals(y)) ?? false;
        } // Equals

        public bool Equals(ThirdPartyDependency other) => Equals(other, StringComparison.InvariantCulture);

        public bool Equals(ThirdPartyDependency other, StringComparison stringComparison)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (Type != other.Type) return false;
            if (LicenseId != other.LicenseId) return false;
            if (!string.Equals(Name, other.Name, stringComparison)) return false;
            if (!string.Equals(Version, other.Version, StringComparison.InvariantCultureIgnoreCase)) return false;
            if (!string.Equals(LicenseId, other.LicenseId, StringComparison.InvariantCultureIgnoreCase)) return false;
            if (!string.Equals(Copyright, other.Copyright, stringComparison)) return false;
            if (!string.Equals(Authors, other.Authors, stringComparison)) return false;
            if (!string.Equals(Description, other.Description, stringComparison)) return false;
            if (!string.Equals(Remarks?.Text, other.Remarks?.Text, stringComparison)) return false;
            if (!string.Equals(Remarks?.Format, other.Remarks?.Format, stringComparison)) return false;

            return true;
        } // Equals

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || (obj is ThirdPartyDependency other && Equals(other));
        } // Equals

        public override int GetHashCode()
        {
            return StringComparer.CurrentCulture.GetHashCode(GetKey());
        } // GetHashCode

        public string GetKey() => Type + "~" + Name + (string.IsNullOrEmpty(Version) ? "" : " #") + Version;

        public static bool operator ==(ThirdPartyDependency left, ThirdPartyDependency right)
        {
            if (ReferenceEquals(left, right)) return true;
            return !(left is null) && left.Equals(right);
        } // operator ==

        public static bool operator !=(ThirdPartyDependency left, ThirdPartyDependency right)
        {
            return !(left == right);
        } // operator !=

        #endregion
    } // class ThirdPartyDependency
} // namespace
