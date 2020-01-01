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
using System.Linq;
using System.Threading;
using AlphaCentaury.Licensing.Data;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    internal sealed class Checker : ProjectAction
    {
        private readonly LicensingDefaultsPool Defaults;
        private readonly IReadOnlyDictionary<string, License> LicensesPool;
        private readonly LicensingThirdPartyPool ThirdPartyPool;
        private readonly CheckerOptions _options;
        private readonly HashSet<string> _usedLicenses;
        private LicensingData _data;
        private VsProject _project;
        private LicensingDefaults _projectDefaults;
        private LicensedItem _projectLicensedDefaults;
        private bool _changed;

        public Checker(VsSolution solution, IToolOutputWriter writer, string defaultsPath, CheckerOptions options, CancellationToken token) : base(solution, writer, token)
        {
            _options = options;
            _usedLicenses = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            writer.WriteLine("Reading licensing defaults...");
            Defaults = LicensingMaintenance.Helper.ReadLicensingDefaultsPool(defaultsPath, writer);
            writer.WriteLine("Reading licenses pool...");
            LicensesPool = LicensingMaintenance.Helper.ReadLicensesPool(defaultsPath);
            writer.WriteLine("Reading third-party pool...");
            ThirdPartyPool = LicensingMaintenance.Helper.ReadThirdPartyPool(defaultsPath);
        } // constructor

        public override void Init()
        {
            base.Init();
            _usedLicenses.Clear();
            CheckDefaults();
            CheckLicensesPool();

            void CheckDefaults()
            {
                if (Defaults.Pool.Count == 0)
                {
                    Writer.WriteLine("WARNING: no licensing defaults have been specified");
                } // if

                foreach (var defaults in Defaults.Pool.Values)
                {
                    if ((defaults.ForLibraries == null) || (defaults.ForPrograms == null))
                    {
                        Writer.WriteLine("WARNING: missing data for defaults '{0}'", defaults.AppliesTo ?? "<default>");
                    } // if
                } // foreach
            } // local CheckDefaults

            void CheckLicensesPool()
            {
                if (LicensesPool.Count == 0)
                {
                    Writer.WriteLine("WARNING: the licenses pool is empty -or- pool file not found");
                } // if

                foreach (var license in LicensesPool.Values)
                {
                    if (string.IsNullOrWhiteSpace(license.Id) || string.IsNullOrWhiteSpace(license.Name) || string.IsNullOrWhiteSpace(license.Text))
                    {
                        Writer.WriteLine("WARNING: missing data for license '{0}' in licenses pool", license.Id ?? "<null>");
                    } // if
                } // foreach
            } // local CheckLicensesPool
        } // Init

        public override void Do(VsProject project, bool standalone)
        {
            Token.ThrowIfCancellationRequested();
            Writer.WriteLine("Project '{0}'", project.Name);
            Writer.IncreaseIndent();
            try
            {
                Check(project, standalone);
            }
            finally
            {
                Writer.DecreaseIndent();
            } // try-finally
        } // Do

        private void Check(VsProject project, bool standalone)
        {
            _changed = false;
            var filename = LicensingMaintenance.Helper.GetLicensingFilename(project, standalone);
            if (!File.Exists(filename))
            {
                Writer.WriteLine("ERROR: file '{0}' not found", Path.GetFileName(filename));
                return;
            } // if

            _data = XmlSerialization.Deserialize<LicensingData>(filename);
            _project = project;

            // check licensed
            if (!CheckProjectLicensedItem()) return; // unable to continue
            OverrideLicensed();
            CheckLicensed();

            // check third-party
            CheckNuGetPackages();
            CheckThirdParty(_data.Dependencies?.ThirdParty, "<Dependencies><ThirdParty>");

            // check licenses
            _data.Dependencies?.Libraries?.ForEach(library => SetLicenseUsed(library.LicenseId));
            var ids = CheckDuplicatedLicenses();
            CheckLicensesReferences(ids);
            CheckLicensesText();
            CheckUnusedLicenses();

            if (!_changed) return;

            Writer.WriteLine("Info: saving changes to '{0}'", filename);
            XmlSerialization.Serialize(filename, _data);
        } // Check

        #region Licensed item

        private bool CheckProjectLicensedItem()
        {
            var licensed = _data.Licensed;
            _projectDefaults = Creator.GetLicensingDefaults(Defaults, _project, false);
            if (_projectDefaults == null)
            {
                Writer.WriteLine("ERROR: licensing defaults key '{0}' not found", _project.LicensingDefaultsKey ?? "<default>");
                return false;
            } // if

            switch (_data.Licensed)
            {
                case LicensedSolution _:
                    _projectLicensedDefaults = _projectDefaults.ForLibraries;
                    return true;

                case LicensedLibrary library:
                    if (!_project.IsLibrary)
                    {
                        Writer.WriteLine("WARNING: licensed item is marked as '{0}', but the project is not a library", licensed.Type);
                        _data.Licensed = library.MorphToProgram(_project.IsGui);
                        _changed = true;
                        _projectLicensedDefaults = _projectDefaults.ForPrograms;
                    }
                    else
                    {
                        _projectLicensedDefaults = _projectDefaults.ForLibraries;
                    } // if-else

                    return true;

                case LicensedInstaller _:
                    _projectLicensedDefaults = _projectDefaults.ForLibraries;

                    return true;

                case LicensedProgram program:
                    if (_project.IsLibrary)
                    {
                        Writer.WriteLine("WARNING: licensed item is marked as '{0}', but the project is a library", licensed.Type);
                        _data.Licensed = program.MorphToLibrary();
                        _changed = true;
                        _projectLicensedDefaults = _projectDefaults.ForLibraries;
                    }
                    else
                    {
                        _projectLicensedDefaults = _projectDefaults.ForPrograms;
                        if (program.IsGuiApp != _project.IsGui)
                        {
                            Writer.WriteLine($"Info: replacing wrong IsGuiApp '{program.IsGuiApp}' with '{_project.IsGui}'");
                            program.IsGuiApp = _project.IsGui;
                            _changed = true;
                        } // if
                    } // if-else

                    return true;

                default:
                    Writer.WriteLine("ERROR: licensed item type '{0}' is not supported", licensed.Type);
                    return false;
            } // switch
        } // CheckProjectLicensedItem

        private void CheckLicensed()
        {
            var licensed = _data.Licensed;

            if (licensed.Assembly != _project.AssemblyName)
            {
                Writer.WriteLine("Info: replacing wrong Assembly '{0}' with '{1}'", licensed.Assembly ?? "<null>", _project.AssemblyName);
                licensed.Assembly = _project.AssemblyName;
                _changed = true;
            } // if

            if (licensed.Name != _project.Namespace)
            {
                Writer.WriteLine("Info: replacing wrong Namespace '{0}' with '{1}'", licensed.Name ?? "<null>", _project.Namespace);
                licensed.Name = _project.Namespace;
                _changed = true;
            } // if

            var oldLicenseId = licensed.LicenseId;
            licensed.Product = Validate(licensed.Product, _projectLicensedDefaults.Product, nameof(licensed.Product));
            licensed.Authors = Validate(licensed.Authors, _projectLicensedDefaults.Authors, nameof(licensed.Authors));
            licensed.Copyright = Validate(licensed.Copyright, _projectLicensedDefaults.Copyright, nameof(licensed.Copyright));
            licensed.Remarks = Validate(licensed.Remarks, _projectLicensedDefaults.Remarks, nameof(licensed.Remarks), false);
            licensed.LicenseId = Validate(licensed.LicenseId, _projectLicensedDefaults.LicenseId, nameof(licensed.LicenseId));
            licensed.TermsConditions = ValidateTerms(licensed.TermsConditions, _projectLicensedDefaults.TermsConditions, nameof(licensed.TermsConditions), licensed);

            if (string.IsNullOrWhiteSpace(licensed.Authors) && string.IsNullOrWhiteSpace(licensed.Copyright))
            {
                Writer.WriteLine("ERROR: {0} '{1}' has no authors or no copyright", licensed.Type, licensed.Name ?? "<null>");
            } // if

            if (oldLicenseId != licensed.LicenseId)
            {
                // add missing license
                var newId = licensed.LicenseId;
                if (_data.Licenses.All(license => license.Id != newId))
                {
                    var license = _projectDefaults.Licenses.FirstOrDefault(lic => lic.Id == newId);
                    if (license == null)
                    {
                        Writer.WriteLine("ERROR: license id '{0}' not found in defaults for '{1}", newId, _project.LicensingDefaultsKey ?? "<default>");
                    }
                    else
                    {
                        _data.Licenses.Add(license);
                        _changed = true;
                    } // if-else
                } // if
            } // if

            SetLicenseUsed(licensed.LicenseId);

            T Validate<T>(T value, T defaultValue, string name, bool warning = true) where T : class, IEquatable<T>
            {
                if (value != null) return value;

                if (defaultValue == null)
                {
                    if (warning) Writer.WriteLine("WARNING: <{0}><{1}> is <null>", licensed.Type, name);
                    return null;
                } // if

                Writer.WriteLine("Info: <{0}><{1}> is <null>. Replacing with '{2}'", licensed.Type, name, defaultValue);
                _changed = true;
                return defaultValue;
            } // local Validate
        } // CheckLicensed

        private List<TermsAndConditions> ValidateTerms(List<TermsAndConditions> terms, List<TermsAndConditions> defaultTerms, string nameof, LicensedItem licensed)
        {
            if ((terms == null) || (terms.Count == 0))
            {
                Writer.WriteLine("WARNING: <{0}><{1}> is <null> or empty", licensed.Type, nameof);
                if ((defaultTerms != null) && (defaultTerms.Count > 0)) return defaultTerms;
                Writer.WriteLine("WARNING: default {0} is also <null> or empty", nameof);
                return null;
            } // if

            terms.ForEach(term =>
            {
                if (!string.IsNullOrWhiteSpace(term.Text)) return;

                Writer.WriteLine("WARNING: <{0}><{1} lang='{2}'> is <null> or empty", licensed.Type, nameof, term.Language ?? "<default>");
                var replacement = defaultTerms.FirstOrDefault(def => string.Equals(def.Language, term.Language, StringComparison.InvariantCultureIgnoreCase));
                if (replacement is null)
                {
                    Writer.WriteLine("WARNING: can't find a replacement for <{0}><{1} lang='{2}'> in defaults", licensed.Type, nameof, term.Language ?? "<default>");
                }
                else
                {
                    replacement.CopyTo(term);
                    _changed = true;
                } // if-else
            });

            // add missing languages
            defaultTerms?.ForEach(defaultTerm =>
            {
                if (!(terms.FirstOrDefault(term => string.Equals(term.Language, defaultTerm.Language, StringComparison.InvariantCultureIgnoreCase)) is null)) return;

                terms.Add(defaultTerm);
                _changed = true;
            });

            return terms;
        } // ValidateTerms

        private void OverrideLicensed()
        {
            var licensed = _data.Licensed;

            licensed.Product = Override(_options.OverrideProduct, licensed.Product, _projectLicensedDefaults.Product, nameof(licensed.Product));
            licensed.Authors = Override(_options.OverrideAuthors, licensed.Authors, _projectLicensedDefaults.Authors, nameof(licensed.Authors));
            licensed.Copyright = Override(_options.OverrideCopyright, licensed.Copyright, _projectLicensedDefaults.Copyright, nameof(licensed.Copyright));
            licensed.LicenseId = Override(_options.OverrideLicense, licensed.LicenseId, _projectLicensedDefaults.LicenseId, nameof(licensed.LicenseId));
            licensed.TermsConditions = OverrideList(_options.OverrideTerms, licensed.TermsConditions, _projectLicensedDefaults.TermsConditions, nameof(licensed.TermsConditions));
            licensed.Remarks = OverrideText(_options.OverrideRemarks, licensed.Remarks, _projectLicensedDefaults.Remarks, nameof(licensed.Remarks));
            licensed.Notes = OverrideText(_options.OverrideNotes, licensed.Notes, _projectLicensedDefaults.Notes, nameof(licensed.Notes));

            T Override<T>(bool @override, T value, T newValue, string item) where T : IEquatable<T>
            {
                if (!@override) return value;
                if (value.Equals(newValue)) return value;

                Writer.WriteLine("Info: overriding <{0}><{1}>", licensed.Type, item);
                _changed = true;
                return newValue;
            } // local Override

            List<T> OverrideList<T>(bool @override, List<T> value, List<T> newValue, string item)
            {
                if (!@override) return value;
                if (ReferenceEquals(value, newValue)) return value; // both null?

                Writer.WriteLine("Info: overriding <{0}><{1}>", licensed.Type, item);
                _changed = true;
                return newValue;
            } // local OverrideList

            FormattedMultilineText OverrideText(bool @override, FormattedMultilineText value, FormattedMultilineText newValue, string item)
            {
                if (!@override) return value;
                if (ReferenceEquals(value, newValue)) return value; // both null?

                Writer.WriteLine("Info: overriding <{0}><{1}>", licensed.Type, item);
                _changed = true;
                return newValue;
            } // OverrideText
        } // OverrideLicensed

        #endregion

        #region Third-party

        private void CheckThirdParty(IList<ThirdPartyDependency> thirdParty, string listName)
        {
            if ((thirdParty == null) || (thirdParty.Count == 0)) return;

            for (var index = 0; index < thirdParty.Count; index++)
            {
                var component = thirdParty[index];

                // check if component is in third-party components pool
                var pool = ThirdPartyPool[component];
                if (pool != null)
                {
                    var dependencyType = component.DependencyType;
                    component.DependencyType = pool.DependencyType;
                    if (!component.Equals(pool))
                    {
                        Writer.WriteLine("Info: replacing third-party {0}: '{1}' from data from pool", component.Type, component.Name);
                        component = pool.Clone();
                        _changed = true;
                        thirdParty[index] = component;
                    } // if

                    component.DependencyType = dependencyType;
                } // if

                if (string.IsNullOrWhiteSpace(component.Name))
                {
                    Writer.WriteLine("ERROR: a component in {0} has no name", listName);
                } // if

                if (!Enum.IsDefined(typeof(ThirdPartyDependencyType), component.Type))
                {
                    Writer.WriteLine("ERROR: invalid type {0} for {1} '{2}'", component.Type, listName, component.Name ?? "<null>");
                } // if

                if (string.IsNullOrWhiteSpace(component.Description))
                {
                    Writer.WriteLine("Info: component {0} '{1}' has no description", listName, component.Name ?? "<null>");
                } // if

                if (string.IsNullOrWhiteSpace(component.LicenseId))
                {
                    Writer.WriteLine("ERROR: component {0} '{1}' has no license", listName, component.Name ?? "<null>");
                } // if

                if (string.IsNullOrWhiteSpace(component.Authors) && string.IsNullOrWhiteSpace(component.Copyright))
                {
                    Writer.WriteLine("ERROR: component {0} '{1}' has no authors or no copyright", listName, component.Name ?? "<null>");
                } // if

                SetLicenseUsed(component.LicenseId);
            } // for index
        } // CheckThirdParty

        private void CheckNuGetPackages()
        {
            if (string.IsNullOrEmpty(_project.Filename)) return;

            var configFile = Path.Combine(Path.GetDirectoryName(_project.Filename) ?? Path.GetPathRoot(_project.Filename), "packages.config");
            if (!File.Exists(configFile)) return;

            var config = XmlSerialization.Deserialize<PackagesConfig>(configFile);
            if (!config.PackagesSpecified) return;

            var thirdParty = _data.Dependencies?.ThirdParty;
            thirdParty ??= new List<ThirdPartyDependency>();

            var q = from component in thirdParty
                    where component.Type == ThirdPartyDependencyType.NugetPackage
                    where component.DependencyType == LicensedDependencyType.Direct
                    select component;
            var third = q.ToDictionary(component => component.Name, StringComparer.InvariantCultureIgnoreCase);

            // check missing packages

            foreach (var package in config.Packages)
            {
                if (!third.TryGetValue(package.Id, out var component))
                {
                    Writer.WriteLine("WARNING: nuget package '{0}' is missing in third party dependencies", package.Id);
                    Writer.WriteLine("Adding incomplete dependency");
                    thirdParty.Add(new ThirdPartyDependency
                    {
                        Name = package.Id,
                        Type = ThirdPartyDependencyType.NugetPackage,
                        DependencyType = LicensedDependencyType.Direct,
                        Version = package.Version
                    });
                    _changed = true;
                }
                else
                {
                    if (string.Equals(component.Version, package.Version, StringComparison.InvariantCultureIgnoreCase)) continue;

                    if (string.IsNullOrEmpty(component.Version))
                    {
                        Writer.WriteLine("Info: adding missing version '{0}' to nuget dependency '{1}'", package.Version, component.Name);
                        component.Version = package.Version;
                        _changed = true;
                    }
                    else
                    {
                        Writer.WriteLine("WARNING: version mismatch ('{0}' vs '{1}') in nuget dependency '{2}'", package.Version, component.Version, component.Name);
                    } // if-else
                } // if-else
            } // foreach package

            // check extra packages
            var nuget = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var package in config.Packages)
            {
                nuget.Add(package.Id);
            } // foreach

            var extra = from component in third.Values
                        where !nuget.Contains(component.Name)
                        select component;
            foreach (var component in extra)
            {
                Writer.WriteLine("Info: removing extra nuget dependency '{0}", component.Name);
                thirdParty.Remove(component);
                _changed = true;
            } // foreach

            if (thirdParty.Count == 0) thirdParty = null;
            if (thirdParty != null)
            {
                _data.Dependencies ??= new LicensingDependencies();
                _data.Dependencies.ThirdParty = thirdParty;
            }
            else if (_data.Dependencies != null)
            {
                _data.Dependencies.ThirdParty = null;
            } // if-else
        } // CheckNuGetPackages

        #endregion

        #region Licenses

        private HashSet<string> CheckDuplicatedLicenses()
        {
            if (!_data.LicensesSpecified)
            {
                Writer.WriteLine("WARNING: <Licenses> is null or empty");
                return new HashSet<string>();
            } // if

            var ids = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            var licenses = new List<License>(_data.Licenses.Count);
            var duplicated = false;
            foreach (var license in _data.Licenses)
            {
                if (ids.Contains(license.Id))
                {
                    Writer.WriteLine("ERROR: duplicated license id: '{0}'", license.Id);
                    duplicated = true;
                }
                else
                {
                    ids.Add(license.Id);
                    licenses.Add(license);
                } // if-else
            } // foreach license

            if (duplicated)
            {
                Writer.WriteLine("Removing duplicated licenses...");
                _data.Licenses.Clear();
                _data.Licenses.AddRange(licenses);
                _changed = true;
            } // if

            return ids;
        } // CheckDuplicatedLicenses

        private void CheckLicensesReferences(HashSet<string> allIds)
        {
            CheckLicenseId(_data.Licensed.LicenseId, $"<{_data.Licensed.Type} name='{_data.Licensed.Name}'>");

            if ((_data.Dependencies == null) || !_data.Dependencies.ThirdPartySpecified) return;

            foreach (var component in _data.Dependencies.ThirdParty)
            {
                CheckLicenseId(component.LicenseId, $"<Component name='{component.Name}'>");
            } // foreach

            void CheckLicenseId(string id, string from)
            {
                if (allIds.Contains(id)) return;

                Writer.WriteLine("WARNING: license id '{0}' referenced in {1} not found", id ?? "<null>", @from);
                if ((id != null) && (LicensesPool.TryGetValue(id, out var license)))
                {
                    Writer.WriteLine("INFO: adding license '{0}' ({1}) from pool to <Licenses>", id, license.Name);
                    _data.Licenses.Add(license);
                    allIds.Add(id);
                    _changed = true;
                }
                else
                {
                    Writer.WriteLine("ERROR: license id '{0}' not found in licenses pool", id ?? "<null>");
                } // if
            } // CheckLicenseId
        } // CheckLicensesReferences

        private void CheckLicensesText()
        {
            if ((_data.Licenses == null) || _data.Licenses.Count == 0) return;

            for (var index = 0; index < _data.Licenses.Count; index++)
            {
                var license = _data.Licenses[index];
                if (LicensesPool.TryGetValue(license.Id, out var poolLicense))
                {
                    if (string.Equals(license.Format, poolLicense.Format, StringComparison.InvariantCultureIgnoreCase) &&
                        string.Equals(license.Name, poolLicense.Name, StringComparison.InvariantCulture) &&
                        string.Equals(license.Text, poolLicense.Text, StringComparison.InvariantCulture)) continue;

                    Writer.WriteLine("Info: updating license '{0}' text with license from the pool", license.Id);
                    _data.Licenses[index] = poolLicense;
                    _changed = true;
                }
                else
                {
                    Writer.WriteLine("WARNING: license '{0}' ({1}) not found in licenses pool", license.Id, license.Name);
                } // if-else
            } // foreach
        } // CheckLicensesText

        private void CheckUnusedLicenses()
        {
            if (_usedLicenses.Count == _data.Licenses.Count) return;

            for (var index = _data.Licenses.Count - 1; index >= 0; index--)
            {
                var licenseId = _data.Licenses[index].Id;
                if (_usedLicenses.Contains(licenseId)) continue;
                Writer.WriteLine("Info: license '{0}' is never used. Removing from list", licenseId);
                _data.Licenses.RemoveAt(index);
                _changed = true;
            } // for
        } // CheckUnusedLicenses

        private void SetLicenseUsed(string licenseId)
        {
            if (_usedLicenses.Contains(licenseId)) return;
            _usedLicenses.Add(licenseId);
        } // SetLicenseUsed

        #endregion
    } // class Checker
} // namespace
