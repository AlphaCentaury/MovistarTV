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
using System.Linq;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    public static class LicensingDataExtensions
    {
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

        public static List<T> Clone<T>(this IList<T> list) where T : ICloneable<T>
        {
            if (list == null) return null;

            var clone = new List<T>(list.Count);
            clone.AddRange(list.Select(item => item.Clone()));

            return clone;
        } // Clone

        public static LicensedProgram MorphToProgram(this LicensedLibrary library, bool isGuiApp)
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
                IsGuiApp = isGuiApp
            };
        } // MorphToProgram LicensedLibrary

        public static LicensedLibrary MorphToLibrary(this LicensedProgram program)
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
        } // MorphToLibrary LicensedProgram
    } // class LicenseFileUtils
} // namespace
