// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    internal class ExpandDependencies
    {
        private readonly List<LicensingData> _allData;
        private readonly Dictionary<string, LicensingData> _libraries;
        private readonly Dictionary<string, License> _allLicenses;

        public ExpandDependencies(List<LicensingData> allData)
        {
            // validate arguments
            if (allData == null) throw new ArgumentNullException(nameof(allData));
            if (allData.Count == 0) return;
            _allData = allData;

            // create dictionary of libraries
            var q = from file in allData
                    where file?.Licensed is LicensedLibrary
                    select file;
            _libraries = q.ToDictionary(file => file.Licensed.Name);

            // create dictionary of licenses
            _allLicenses = new Dictionary<string, License>(StringComparer.InvariantCultureIgnoreCase);
        } // constructor

        public void Expand()
        {
            if (_allData.Count == 0) return;

            // validate data
            var q = from data in _allData
                    where (data.Licensed == null)
                    select data;
            if (q.FirstOrDefault() != null) throw new ArgumentException();

            // create dictionary of licenses
            var licenses = from data in _allData
                           where data.LicensesSpecified
                           from license in data.Licenses
                           select license;
            licenses.ForEach(license => _allLicenses[license.Id] = license);

            AddIndirectLibraryDependencies();
            SortDirectThirdParty();
            AddIndirectThirdParty();
            AddMissingLicenses();
        } // Expand

        private void AddIndirectLibraryDependencies()
        {
            // clear indirect dependencies
            _allData.Where(data => data.Dependencies != null).ForEach(data =>
            {
                data.Dependencies.Libraries?.RemoveAll(library => !library.IsDirectDependency && !library.IsDynamicDependency);
                data.Dependencies.ThirdParty = null;
            });

            // re-add indirect dependencies
            var comparer = new DependencyLibraryComparer();
            var visited = new HashSet<string>(StringComparer.InvariantCulture);
            _allData.ForEach(data =>
            {
                AddIndirectLibraryDependencies(data, visited);
                data.Dependencies?.Libraries?.Sort(comparer);
            });
        } // AddIndirectLibraryDependencies

        private void SortDirectThirdParty()
        {
            var comparer = new ThirdPartyDependencyComparer();
            _allData.Where(data => data.ThirdParty != null).ForEach(data =>
            {
                data.Dependencies?.ThirdParty?.Sort(comparer);
            });
        } // ShortThirdParty

        private void AddIndirectThirdParty()
        {
            _allData.Where(data => data.Dependencies?.Libraries != null).ForEach(AddIndirectThirdParty);
        } // AddIndirectThirdParty

        private void AddIndirectLibraryDependencies(LicensingData data, HashSet<string> visited)
        {
            if (data.Dependencies?.Libraries == null) return;

            // avoid circular references
            if (visited.Contains(data.Licensed.Name)) return;
            visited.Add(data.Licensed.Name);

            // add indirect library dependencies
            var q = from dependency in data.Dependencies.Libraries
                    select _libraries[dependency.Namespace];
            q.ForEach(AddIndirectLibraryDependencies, visited);

            // create hashset to avoid adding duplicated dependencies
            var added = new HashSet<string>();
            data.Dependencies.Libraries.ForEach(lib => added.Add(lib.Namespace ?? throw new ArgumentException()));

            var libraries = (from dependency in data.Dependencies.Libraries
                             select _libraries[dependency.Namespace]).ToArray();

            var dependencies = from library in libraries
                               where library.Dependencies?.Libraries != null
                               from dependency in library.Dependencies.Libraries
                               select dependency;

            dependencies.ForEach(dependency =>
            {
                if (added.Contains(dependency.Namespace)) return;

                data.Dependencies.Libraries.Add(new LibraryDependency
                {
                    Namespace = dependency.Namespace,
                    Assembly = dependency.Assembly,
                    LicenseId = dependency.LicenseId,
                    IsDirectDependency = false
                });
                added.Add(dependency.Namespace);
            });
        } // AddIndirectLibraryDependencies

        private void AddIndirectThirdParty(LicensingData data)
        {
            var added = new HashSet<string>();
            data.ThirdParty?.ForEach(library => added.Add(library.Name));

            var q = from dependency in data.Dependencies.Libraries
                    let library = _libraries[dependency.Namespace]
                    where library.ThirdParty != null
                    from thirdParty in library.ThirdParty
                    select thirdParty;

            q.ForEach(library =>
            {
                if (added.Contains(library.Name)) return;
                if (data.Dependencies.ThirdParty == null) data.Dependencies.ThirdParty = new List<ThirdPartyDependency>();

                data.Dependencies.ThirdParty.Add(library);
                added.Add(library.Name);
            });

            data.Dependencies.ThirdParty?.Sort(new ThirdPartyDependencyComparer());
        } // AddIndirectThirdParty

        private void AddMissingLicenses()
        {
            _allData.Where(data => data.Dependencies?.ThirdParty != null).ForEach(data =>
            {
                data.Dependencies.ThirdParty.ForEach(library =>
                {
                    var license = _allLicenses[library.LicenseId];
                    if (data.GetLicense(license.Id) == null)
                    {
                        data.Licenses.Add(license);
                    } // if
                });
            });
        } // AddMissingLicenses
    } // class ExpandDependencies
} // namespace
