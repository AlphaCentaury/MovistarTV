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
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public abstract class LicensedComponent
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("License")]
        public string LicenseId { get; set; }

        public string Authors { get; set; }

        public string Copyright { get; set; }

        public FormattedMultilineText Remarks { get; set; }

        public void Inherit(LicensedComponent from)
        {
            if (from == null) return;

            Name ??= from.Name;
            LicenseId ??= from.LicenseId;
            Authors ??= from.Authors;
            Copyright ??= from.Copyright;
            Remarks ??= from.Remarks;
        } // Inherit

        public override string ToString() => Name;

        protected void CopyToComponent(LicensedComponent other)
        {
            other.Name = Name;
            other.LicenseId = LicenseId;
            other.Authors = Authors;
            other.Copyright = Copyright;
            other.Remarks = Remarks?.Clone();
        } // CopyToComponent
    } // class Component
} // namespace
