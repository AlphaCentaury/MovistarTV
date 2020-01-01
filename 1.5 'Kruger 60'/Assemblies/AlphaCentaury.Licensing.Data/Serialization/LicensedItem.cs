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

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public abstract class LicensedItem : LicensedComponent, ICloneable<LicensedItem>
    {
        [XmlAttribute("file")]
        public string Assembly { get; set; }

        public string Product { get; set; }

        [XmlElement("TermsAndConditions")]
        public List<TermsAndConditions> TermsConditions { get; set; }

        public FormattedMultilineText Notes { get; set; }

        [XmlIgnore]
        public abstract LicensedItemType Type { get; }

        public override string ToString()
        {
            return $"{Type}:{Name}";
        } // ToString

        public LicensedItem Clone()
        {
            var clone = CreateNewForCloning();
            clone.Assembly = Assembly;
            clone.Product = Product;
            clone.Notes = Notes?.Clone();
            if (TermsConditionsSpecified)
            {
                clone.TermsConditions = TermsConditions.Select(term => term.Clone()).ToList();
            } // if

            return clone;
        } // Clone

        object ICloneable.Clone() => Clone();

        public abstract void Inherit([CanBeNull] LicensedItem from);

        public void CopyTo(LibraryDependency item) => CopyToLibraryDependency(item);

        public void CopyTo(LicensedItem item)
        {
            if (item == null) return;
            item.Assembly = Assembly;
            item.Product = Product;
            item.TermsConditions = TermsConditions.Clone();
            item.Notes = Notes.Clone();

            CopyToComponent(item);
        } // CopyTo

        public LibraryDependency ConvertToLibraryDependency()
        {
            var item = new LibraryDependency();
            CopyToLibraryDependency(item);

            return item;
        } // ConvertTo

        public bool TermsConditionsSpecified => (TermsConditions != null) && (TermsConditions.Count > 0);

        protected abstract LicensedItem CreateNewForCloning();

        protected virtual void CopyToLibraryDependency(LibraryDependency item)
        {
            item.Type = Type;
            item.Name = Name;
            item.Assembly = Assembly;
            item.LicenseId = LicenseId;
            item.Authors = Authors;
            item.Copyright = Copyright;
            item.Remarks = Remarks?.Clone();
        } // CopyToLibraryDependency

        protected void ProtectedInherit([CanBeNull] LicensedItem from)
        {
            if (from == null) return;

            Assembly ??= from.Assembly;
            Product ??= from.Product;
            TermsConditions = TermsAndConditions.Inherit(TermsConditions, from.TermsConditions);
            Notes ??= from.Notes?.Clone();

            Inherit((LicensedComponent)from);
        } // ProtectedOverride
    } // class LicensedItem
} // namespace
