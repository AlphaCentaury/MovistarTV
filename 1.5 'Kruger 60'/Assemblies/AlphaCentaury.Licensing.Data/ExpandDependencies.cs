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
    internal class ExpandDependencies
    {
        private readonly List<LicensingFile> _files;
        private readonly Dictionary<string, LicensingFile> _libraries;

        public ExpandDependencies(List<LicensingFile> files)
        {
            // validate arguments
            if (files == null) throw new ArgumentNullException(nameof(files));
            if (files.Count == 0) return;
            _files = files;

            // validate files
            var q = from file in files
                    where (file.Licensing?.Licensed == null)
                    select file;
            if (q.FirstOrDefault() != null) throw new ArgumentException();

            // create dictionary of libraries
            var q2 = from file in files
                     where file.Licensing.Licensed is LicensedLibrary
                     select file;
            _libraries = q2.ToDictionary(file => file.Licensing.Licensed.Name);

            // create empty visited 'list'
            
        } // constructor

        public void Expand()
        {
            if (_files.Count == 0) return;

            AddIndirectLibraryDependencies();
            SortDirectThirdParty();
            AddIndirectThirdParty();
            AddMissingLicenses();
        } // Expand

        private void AddIndirectLibraryDependencies()
        {
            // clear indirect dependencies
            _files.Where(file => file.Dependencies != null).ForEach(file =>
            {
                file.Dependencies.Libraries?.RemoveAll(library => !library.IsDirectDependency);
                file.Dependencies.ThirdParty = null;
            });

            // re-add indirect dependencies
            var comparer = new DependencyLibraryComparer();
            var visited = new HashSet<string>(StringComparer.InvariantCulture);
            _files.ForEach(file =>
            {
                AddIndirectLibraryDependencies(file, visited);
                file.Dependencies.Libraries?.Sort(comparer);
            });
        } // AddIndirectLibraryDependencies

        public void SortDirectThirdParty()
        {
            var comparer = new ThirdPartyLibraryComparer();
            _files.Where(file => file.Licensing.ThirdParty != null).ForEach(file =>
            {
                file.Dependencies.ThirdParty?.Sort(comparer);
            });
        } // ShortThirdParty

        public void AddIndirectThirdParty()
        {
            _files.Where(file => file.Dependencies?.Libraries != null).ForEach(AddIndirectThirdParty);
        } // AddIndirectThirdParty

        public void AddMissingLicenses()
        {
            var q = from file in _files
                    where (file.Licenses != null) && (file.Licenses.Count > 0)
                    from license in file.Licenses
                    select license;

            var licenses = new Dictionary<string, License>();
            q.ForEach(license => licenses[license.Id] = license);

            _files.Where(file => file.Dependencies?.ThirdParty != null).ForEach(file =>
            {
                file.Dependencies.ThirdParty.ForEach(library =>
                {
                    var license = licenses[library.LicenseId];
                    if (!file.Licenses.Contains(license))
                    { 
                        file.Licenses.Add(license);
                    } // if
                });
            });
        } // AddMissingLicenses

        private void AddIndirectLibraryDependencies(LicensingFile file, HashSet<string> visited)
        {
            if (file.Dependencies?.Libraries == null) return;

            // avoid circular references
            if (visited.Contains(file.Licensing.Licensed.Name)) return;
            visited.Add(file.Licensing.Licensed.Name);

            // add indirect library dependencies
            var q = from dependency in file.Dependencies.Libraries
                    select _libraries[dependency.Name];
            q.ForEach(AddIndirectLibraryDependencies, visited);

            // create hashset to avoid adding duplicated dependencies
            var added = new HashSet<string>();
            file.Dependencies.Libraries.ForEach(lib => added.Add(lib.Name ?? throw new ArgumentException()));

            var libraries = (from dependency in file.Dependencies.Libraries
                             select _libraries[dependency.Name]).ToArray();

            var dependencies = from library in libraries
                               where library.Dependencies.Libraries != null
                               from dependency in library.Dependencies.Libraries
                               select dependency;

            dependencies.ForEach(dependency =>
            {
                if (added.Contains(dependency.Name)) return;

                file.Dependencies.Libraries.Add(new DependencyLibrary
                {
                    Name = dependency.Name,
                    AssemblyName = dependency.AssemblyName,
                    LicenseId = dependency.LicenseId,
                    IsDirectDependency = false
                });
                added.Add(dependency.Name);
            });
        } // AddIndirectLibraryDependencies

        private void AddIndirectThirdParty(LicensingFile file)
        {
            var added = new HashSet<string>();
            file.Licensing.ThirdParty?.ForEach(library => added.Add(library.Name));

            var q = from dependency in file.Dependencies.Libraries
                    let library = _libraries[dependency.Name]
                    where library.Licensing?.ThirdParty != null
                    from thirdParty in library.Licensing.ThirdParty
                    select thirdParty;

            q.ForEach(library =>
            {
                if (added.Contains(library.Name)) return;
                if (file.Dependencies.ThirdParty == null) file.Dependencies.ThirdParty = new List<ThirdPartyLibrary>();

                file.Dependencies.ThirdParty.Add(library);
                added.Add(library.Name);
            });

            file.Dependencies.ThirdParty.Sort(new ThirdPartyLibraryComparer());
        } // AddIndirectThirdParty
    } // class ExpandDependencies
} // namespace
