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
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    internal class ExpandDependencies
    {
        private readonly List<LicensingData> _dataList;
        private readonly Dictionary<string, LicensingData> _libraries;
        private readonly Dictionary<string, License> _allLicenses;

        public ExpandDependencies(List<LicensingData> list)
        {
            // validate arguments
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (list.Count == 0) return;
            _dataList = list;

            // create dictionary of libraries
            _libraries = list.ToDictionary(data => data.Licensed.Name);

            // create dictionary of licenses
            _allLicenses = new Dictionary<string, License>(StringComparer.InvariantCultureIgnoreCase);
        } // constructor

        public void Expand()
        {
            if (_dataList.Count == 0) return;

            // validate data
            var q = from data in _dataList
                    where (data.Licensed == null)
                    select data;
            if (q.FirstOrDefault() != null) throw new ArgumentException();

            // create dictionary of licenses
            var licenses = from data in _dataList
                           where data.LicensesSpecified
                           from license in data.Licenses
                           select license;
            licenses.ForEach(license => _allLicenses[license.Id] = license);

            AddIndirectLibraryDependencies();
            AddIndirectThirdParty();
            SortThirdParty();
            AddMissingLicenses();
        } // Expand

        private void AddIndirectLibraryDependencies()
        {
            // clear indirect dependencies
            _dataList.Where(data => data.Dependencies != null).ForEach(data =>
            {
                data.Dependencies.Libraries?.RemoveAll(library => library.DependencyType == LicensedDependencyType.Indirect);
                data.Dependencies.ThirdParty?.RemoveAll(component => component.DependencyType == LicensedDependencyType.Indirect);
            });

            // re-add indirect dependencies
            var comparer = new DependencyLibraryNameComparer();
            var visited = new HashSet<string>(StringComparer.InvariantCulture);
            _dataList.ForEach(data =>
            {
                AddIndirectLibraryDependencies(data, visited);
                data.Dependencies?.Libraries?.Sort(comparer);
            });
        } // AddIndirectLibraryDependencies
        private void SortThirdParty()
        {
            var comparer = new ThirdPartyDependencyNameComparer();
            _dataList.Where(data => data.Dependencies?.ThirdPartySpecified ?? false).ForEach(data =>
            {
                data.Dependencies.ThirdParty.Sort(comparer);
            });
        } // ShortThirdParty

        private void AddIndirectThirdParty()
        {
            _dataList.Where(data => data.Dependencies?.Libraries != null).ForEach(AddIndirectThirdParty);
        } // AddIndirectThirdParty

        private void AddIndirectLibraryDependencies(LicensingData data, HashSet<string> visited)
        {
            if (data.Dependencies?.Libraries == null) return;

            // avoid circular references
            if (visited.Contains(data.Licensed.Name)) return;
            visited.Add(data.Licensed.Name);

            // add indirect library dependencies
            var q = from dependency in data.Dependencies.Libraries
                    select _libraries[dependency.Name];
            q.ForEach(AddIndirectLibraryDependencies, visited);

            // create hashset to avoid adding duplicated dependencies
            var added = new HashSet<string>();
            data.Dependencies.Libraries.ForEach(lib => added.Add(lib.Name ?? throw new ArgumentException()));

            var libraries = (from dependency in data.Dependencies.Libraries
                             select _libraries[dependency.Name]).ToArray();

            var dependencies = from library in libraries
                               where library.Dependencies?.Libraries != null
                               from dependency in library.Dependencies.Libraries
                               select dependency.Clone();

            dependencies.ForEach(dependency =>
            {
                if (added.Contains(dependency.Name)) return;

                var library = dependency.Clone();
                library.DependencyType = LicensedDependencyType.Indirect;
                data.Dependencies.Libraries.Add(library);
                added.Add(dependency.Name);
            });
        } // AddIndirectLibraryDependencies

        private void AddIndirectThirdParty(LicensingData data)
        {
            if ((data.Dependencies == null) || !data.Dependencies.LibrariesSpecified) return;

            var added = new HashSet<string>();
            data.Dependencies.ThirdParty?.ForEach(component => added.Add(component.Name));

            var q = from dependency in data.Dependencies.Libraries
                    let library = _libraries[dependency.Name]
                    where (library.Dependencies != null) && library.Dependencies.ThirdPartySpecified
                    from thirdParty in library.Dependencies.ThirdParty
                    select thirdParty.Clone();

            q.ForEach(dependency =>
            {
                if (added.Contains(dependency.Name)) return;

                dependency.DependencyType = LicensedDependencyType.Indirect;
                data.Dependencies.ThirdParty ??= new List<ThirdPartyDependency>();
                data.Dependencies.ThirdParty.Add(dependency);
                added.Add(dependency.Name);
            });

            data.Dependencies.ThirdParty?.Sort(new ThirdPartyDependencyNameComparer());
        } // AddIndirectThirdParty

        private void AddMissingLicenses()
        {
            var q = from data in _dataList
                    where data.DependenciesSpecified
                    select data.Dependencies;

            _dataList.Where(data => data.DependenciesSpecified).ForEach(data =>
            {
                var missingFromLibraries = MissingLicenses(data, data.Dependencies.Libraries);
                var missingFromThirdParty = MissingLicenses(data, data.Dependencies.ThirdParty);
                var missing = missingFromLibraries.Concat(missingFromThirdParty);

                data.Licenses.AddRange(missing);
            });

            IEnumerable<License> MissingLicenses(LicensingData data, IEnumerable<LicensedComponentDependency> list)
            {
                if (list == null) return Enumerable.Empty<License>();

                return from dependency in list
                       where !string.IsNullOrEmpty(dependency.LicenseId)
                       where data.GetLicense(dependency.LicenseId) == null
                       let license = GetLicenseFromPool(dependency.LicenseId)
                       where license != null
                       select license;
            } // local MissingLicenses

            License GetLicenseFromPool(string licenseId)
            {
                return _allLicenses.TryGetValue(licenseId, out var license) ? license : null;
            } // local GetLicenseFromPool
        } // AddMissingLicenses
    } // class ExpandDependencies
} // namespace
