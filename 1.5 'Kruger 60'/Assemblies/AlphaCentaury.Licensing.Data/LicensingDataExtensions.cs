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
        public static List<License> Clone(this List<License> list)
        {
            var clone = new List<License>(list.Count);
            clone.AddRange(list.Select(item => item.Clone()));

            return clone;
        } // Clone:List<License>

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
                TermsConditions = library.TermsConditions,
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
                TermsConditions = program.TermsConditions,
                Product = program.Product,
                Assembly = program.Assembly,
            };
        } // Morph LicensedProgram
    } // class LicenseFileUtils
} // namespace
