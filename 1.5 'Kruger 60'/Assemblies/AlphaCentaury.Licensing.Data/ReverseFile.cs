using System;
using System.Collections.Generic;
using System.IO;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    internal class ReverseFile
    {
        private LicensingData _file;
        private Dictionary<string, LicenseUsedBy> _licenses;
        private Dictionary<string, HashSet<string>> _libraries;
        private Dictionary<string, HashSet<string>> _thirdParty;

        public ReverseFile(LicensingData file)
        {
            _file = file;
        } // constructor

        public ReversedLicensingFile Reverse()
        {
            if (_file.Licenses == null) throw new InvalidDataException();
            if (_file.Licenses.Count == 0) throw new InvalidDataException();

            _licenses = new Dictionary<string, LicenseUsedBy>(_file.Licenses.Count, StringComparer.InvariantCulture);
            _libraries = new Dictionary<string, HashSet<string>>(StringComparer.InvariantCulture);
            _thirdParty = new Dictionary<string, HashSet<string>>(StringComparer.InvariantCulture);

            foreach (var license in _file.Licenses)
            {
                _licenses.Add(license.Id, new LicenseUsedBy
                {
                    Libraries = new List<LibraryDependency>(),
                    ThirdParty = new List<ThirdPartyDependency>()
                });
                _libraries.Add(license.Id, new HashSet<string>(StringComparer.InvariantCulture));
                _thirdParty.Add(license.Id, new HashSet<string>(StringComparer.InvariantCulture));
            } // foreach

            AddLicensed(_file.Licensing.Licensed);
            AddThirdParty(_file.Licensing.ThirdParty);
            AddLibraries(_file.Dependencies.Libraries);
            AddThirdParty(_file.Dependencies.ThirdParty);
            Sort();

            var result = new ReversedLicensingFile
            {
                Licenses = new List<LicenseExpanded>(_file.Licenses.Count)
            };

            for (var index = 0; index < _file.Licenses.Count;index++)
            {
                var license = _file.Licenses[index];
                var expanded = result.Licenses[index];
                expanded.Text = license;
                expanded.UsedBy = _licenses[license.Id];
                if ((expanded.UsedBy.Libraries == null) && (expanded.UsedBy.ThirdParty == null))
                {
                    expanded.UsedBy = null;
                } // if
            } // for index

            return result;
        } // Reverse

        private void AddLicensed(LicensedItem item)
        {
            var library = new LibraryDependency
            {
                Namespace = item.Name,
                Assembly = item.Assembly + (item is LicensedProgram? ".exe" : ""),
                IsDirectDependency = true,
                LicenseId = item.LicenseId,
            };
            AddLibrary(library);
        } // AddLicensed

        private void AddLibraries(IEnumerable<LibraryDependency> libraries)
        {
            libraries.ForEach(AddLibrary);
        } // AddLibraries

        private void AddLibrary(LibraryDependency library)
        {
            var key = $"{library.Namespace}:{library.Assembly}";
            var set = _libraries[library.LicenseId];

            if (set.Contains(key)) return;

            _licenses[library.LicenseId].Libraries.Add(library);
            set.Add(key);
        } // AddLibrary

        private void AddThirdParty(IEnumerable<ThirdPartyDependency> licensingThirdParty)
        {
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
            var libraryComparer = new DependencyLibraryComparer();
            var thirdPartyComparer = new ThirdPartyDependencyComparer();

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
    } // class ReverseFile
} // namespace