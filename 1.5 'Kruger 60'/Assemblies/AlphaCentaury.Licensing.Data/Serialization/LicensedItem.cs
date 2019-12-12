// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public abstract class LicensedItem: LicensedComponent
    {
        [XmlAttribute("file")]
        public string Assembly { get; set; }

        public string Product { get; set; }

        [XmlElement("TermsAndConditions")]
        public List<TermsAndConditions> TermsConditions { get; set; }

        [XmlIgnore]
        public abstract LicensedItemType Type { get; }

        public  override string ToString()
        {
            return $"{Type}:{Name}";
        } // ToString

        public LicensedItem Clone()
        {
            var clone = CreateNewForCloning();
            clone.Assembly = Assembly;
            clone.Product = Product;
            if (TermsConditionsSpecified)
            {
                clone.TermsConditions = TermsConditions.Select(term => term.Clone()).ToList();
            } // if

            return clone;
        } // Clone

        public void CopyTo(LibraryDependency item) => CopyToLibraryDependency(item);

        public LibraryDependency ConvertToLibraryDependency(string language = null)
        {
            var item = new LibraryDependency();
            CopyToLibraryDependency(item);

            return item;
        } // ConvertTo

        public bool TermsConditionsSpecified => (TermsConditions != null) && (TermsConditions.Count > 0);

        protected abstract LicensedItem CreateNewForCloning();

        protected virtual void CopyToLibraryDependency(LibraryDependency item)
        {
            item.Name = Name;
            item.Assembly = Assembly;
            item.LicenseId = LicenseId;
            item.Authors = Authors;
            item.Copyright = Copyright;
            item.Remarks = Remarks?.Clone();
        } // CopyToLibraryDependency
    } // class LicensedItem
} // namespace
