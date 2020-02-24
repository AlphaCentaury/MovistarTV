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
using System.IO;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    internal class GetLicensingUsage
    {
        private readonly LicensingData _data;
        private Dictionary<string, LicenseAppliesTo> _licenses;
        private Dictionary<string, HashSet<string>> _libraries;
        private Dictionary<string, HashSet<string>> _thirdParty;

        public GetLicensingUsage(LicensingData data)
        {
            _data = data;
        } // constructor

        public LicensingUsage Get()
        {
            if (_data.Licenses == null) throw new InvalidDataException();
            if (_data.Licenses.Count == 0) throw new InvalidDataException();

            _licenses = new Dictionary<string, LicenseAppliesTo>(_data.Licenses.Count, StringComparer.InvariantCultureIgnoreCase);
            _libraries = new Dictionary<string, HashSet<string>>(StringComparer.InvariantCultureIgnoreCase);
            _thirdParty = new Dictionary<string, HashSet<string>>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var license in _data.Licenses)
            {
                _licenses.Add(license.Id, new LicenseAppliesTo
                {
                    Libraries = new List<LibraryDependency>(),
                    ThirdParty = new List<ThirdPartyDependency>()
                });
                _libraries.Add(license.Id, new HashSet<string>(StringComparer.InvariantCulture));
                _thirdParty.Add(license.Id, new HashSet<string>(StringComparer.InvariantCulture));
            } // foreach

            AddLibrary(_data.Licensed.ConvertToLibraryDependency());
            AddLibraries(_data.Dependencies?.Libraries);
            AddThirdParty(_data.Dependencies?.ThirdParty);
            Sort();

            var result = new LicensingUsage
            {
                Licensed = _data.Licensed,
                Usage = new List<LicenseUsage>(_data.Licenses.Count)
            };

            foreach (var license in _data.Licenses)
            {
                var expanded = new LicenseUsage {License = license, AppliesTo = _licenses[license.Id]};
                if (!expanded.AppliesTo.LibrariesSpecified && !expanded.AppliesTo.ThirdPartySpecified)
                {
                    expanded.AppliesTo = null;
                } // if

                result.Usage.Add(expanded);
            } // foreach license

            return result;
        } // Get

        private void AddLibraries(IEnumerable<LibraryDependency> libraries)
        {
            libraries?.ForEach(AddLibrary);
        } // AddLibraries

        private void AddLibrary(LibraryDependency library)
        {
            var key = $"{library.Name}:{library.Assembly}";
            var set = _libraries[library.LicenseId];

            if (set.Contains(key)) return;

            _licenses[library.LicenseId].Libraries.Add(library);
            set.Add(key);
        } // AddLibrary

        private void AddThirdParty(IEnumerable<ThirdPartyDependency> licensingThirdParty)
        {
            if (licensingThirdParty == null) return;
            foreach (var dependency in licensingThirdParty)
            {
                var key = $"{dependency.Type}:{dependency.Name}";
                var set = _thirdParty[dependency.LicenseId];

                if (set.Contains(key)) continue;

                _licenses[dependency.LicenseId].ThirdParty.Add(dependency);
                set.Add(key);
            } // foreach
        } // AddThirdParty

        private void Sort()
        {
            var libraryComparer = new DependencyLibraryNameComparer();
            var thirdPartyComparer = new ThirdPartyDependencyNameComparer();

            foreach (var item in _licenses)
            {
                var usedBy = item.Value;
                if (usedBy.Libraries.Count == 0)
                {
                    usedBy.Libraries = null;
                }
                else
                {
                    usedBy.Libraries.Sort(libraryComparer);
                } // if-else

                if (usedBy.ThirdParty.Count == 0)
                {
                    usedBy.ThirdParty = null;
                }
                else
                {
                    usedBy.ThirdParty.Sort(thirdPartyComparer);
                } // if-else
            } // foreach
        } // Sort
    } // class GetLicensingUsage
} // namespace
