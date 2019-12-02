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
        public static LicensingFile Clone(this LicensingFile item)
        {
            var clone = new LicensingFile
            {
                Dependencies = item.Dependencies.Clone(),
                Licensing = item.Licensing.Clone(),
                Licenses  = item.Licenses.Clone()
            };

            return clone;
        } // Clone:LicensingFile

        public static Serialization.Licensing Clone(this Serialization.Licensing item)
        {
            var clone = new Serialization.Licensing
            {
                Licensed  = item.Licensed.Clone(),
                ThirdParty  = item.ThirdParty.Clone()
            };

            return clone;
        } // Clone:Licensing

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

        public static List<DependencyLibrary> Clone(this List<DependencyLibrary> list)
        {
            var clone = new List<DependencyLibrary>(list.Count);
            clone.AddRange(list.Select(item => item.Clone()));

            return clone;
        } // Clone:List<License>

        public static List<ThirdPartyLibrary> Clone(this List<ThirdPartyLibrary> list)
        {
            var clone = new List<ThirdPartyLibrary>(list.Count);
            clone.AddRange(list.Select(item => item.Clone()));

            return clone;
        } // Clone:List<ThirdPartyLibrary>

        public static DependencyLibrary Clone(this DependencyLibrary item)
        {
            var clone = new DependencyLibrary
            {
                Name = item.Name,
                AssemblyName = item.AssemblyName,
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

        public static ThirdPartyLibrary Clone(this ThirdPartyLibrary item)
        {
            var clone = new ThirdPartyLibrary
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
    } // class LicenseFileUtils
} // namespace
