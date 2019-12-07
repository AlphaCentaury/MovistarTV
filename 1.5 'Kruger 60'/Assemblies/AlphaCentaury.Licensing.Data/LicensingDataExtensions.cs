// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    public static class LicensingDataExtensions
    {
        public static LicensingData Clone(this LicensingData item)
        {
            var clone = new LicensingData
            {
                Licensed = item.Licensed.Clone(),
                ThirdParty = item.ThirdParty.Clone(),
                Dependencies = item.Dependencies.Clone(),
                Licenses  = item.Licenses.Clone()
            };

            return clone;
        } // Clone:LicensingData

        public static Dependencies Clone(this Dependencies item)
        {
            var clone = new Dependencies
            {
                Libraries  = item.Libraries.Clone(),
                ThirdParty  = item.ThirdParty.Clone()
            };

            return clone;
        } // Clone:Dependencies

        public static List<License> Clone(this List<License> list)
        {
            var clone = new List<License>(list.Count);
            clone.AddRange(list.Select(item => item.Clone()));

            return clone;
        } // Clone:List<License>

        public static List<LibraryDependency> Clone(this List<LibraryDependency> list)
        {
            var clone = new List<LibraryDependency>(list.Count);
            clone.AddRange(list.Select(item => item.Clone()));

            return clone;
        } // Clone:List<License>

        public static List<ThirdPartyDependency> Clone(this List<ThirdPartyDependency> list)
        {
            var clone = new List<ThirdPartyDependency>(list.Count);
            clone.AddRange(list.Select(item => item.Clone()));

            return clone;
        } // Clone:List<ThirdPartyLibrary>

        public static LibraryDependency Clone(this LibraryDependency item)
        {
            var clone = new LibraryDependency
            {
                Namespace = item.Namespace,
                Assembly = item.Assembly,
                LicenseId = item.LicenseId
            };

            return clone;
        } // Clone:DependencyLibrary

        public static License Clone(this License item)
        {
            var clone = new License
            {
                Id = item.Id,
                Name = item.Name,
                Format = item.Format,
                Text = item.Text
            };

            return clone;
        } // Clone:License

        public static LicensedItem Clone(this LicensedItem item)
        {
            LicensedItem clone;

            switch (item)
            {
                case LicensedLibrary _:
                    clone = new LicensedLibrary();
                    break;

                case LicensedProgram _:
                    clone = new LicensedProgram();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(item));
            } // switch

            item.CopyTo(clone);
            clone.Product = item.Product;
            clone.Terms  = item.Terms.Clone();

            return clone;
        } // Clone:LicensedItem

        public static TermsAndConditions Clone(this TermsAndConditions item)
        {
            var clone = new TermsAndConditions
            {
                Format = item.Format,
                Type = item.Type,
                Text = item.Text
            };

            return clone;
        } // Clone:TermsAndConditions

        public static ThirdPartyDependency Clone(this ThirdPartyDependency item)
        {
            var clone = new ThirdPartyDependency
            {
                Description = item.Description
            };
            item.CopyTo(clone);

            return clone;
        } // Clone:ThirdPartyLibrary

        public static void CopyTo(this BaseLibrary from, BaseLibrary to)
        {
            if (from == null) throw new ArgumentNullException(nameof(from));
            if (to == null) throw new ArgumentNullException(nameof(to));

            to.Authors = from.Authors;
            to.Copyright = from.Copyright;
            to.LicenseId = from.LicenseId;
            to.Name = from.Name;
        } // CopyTo

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null) return;
            foreach (var item in enumerable)
            {
                    action(item);
            } // foreach
        } // ForEach

        public static void ForEach<T,TData>(this IEnumerable<T> enumerable, Action<T, TData> action, TData data)
        {
            if (enumerable == null) return;
            foreach (var item in enumerable)
            {
                action(item, data);
            } // foreach
        } // ForEach

        public static LicensedProgram Morph(this LicensedLibrary library)
        {
            return new LicensedProgram
            {
                Name = library.Name,
                LicenseId = library.LicenseId,
                Copyright = library.Copyright,
                Authors = library.Authors,
                Remarks = library.Remarks,
                Terms = library.Terms,
                Product = library.Product,
                Assembly = library.Assembly,
                IsGuiApp = true
            };
        } // Morph LicensedLibrary

        public static LicensedLibrary Morph(this LicensedProgram program)
        {
            return new LicensedLibrary
            {
                Name = program.Name,
                LicenseId = program.LicenseId,
                Copyright = program.Copyright,
                Authors = program.Authors,
                Remarks = program.Remarks,
                Terms = program.Terms,
                Product = program.Product,
                Assembly = program.Assembly,
            };
        } // Morph LicensedProgram
    } // class LicenseFileUtils
} // namespace
