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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public sealed partial class LicensingMaintenance
    {

        internal sealed class Checker : ProjectAction
        {
            private readonly IReadOnlyDictionary<string, LicensingDefaults> Defaults;
            private readonly IReadOnlyDictionary<string, License> LicensesPool;
            private readonly CheckerOptions _options;
            private LicensingData _data;
            private VsProject _project;
            private LicensingDefaults _projectDefaults;
            private LicensedItem _projectLicensedDefaults;
            private bool _changed;

            public Checker(VsSolution solution, IToolOutputWriter writer, string defaultsPath, CheckerOptions options, CancellationToken token) : base(solution, writer, token)
            {
                _options = options;
                Defaults = Helper.ReadLicensingDefaults(defaultsPath);
                LicensesPool = Helper.ReadLicensesPool(defaultsPath);
            } // constructor

            public override void Init()
            {
                base.Init();
                CheckDefaults();
                CheckLicensesPool();

                void CheckDefaults()
                {
                    if (Defaults.Count == 0)
                    {
                        Writer.WriteLine("WARNING: no licensing defaults have been specified");
                    } // if

                    foreach (var defaults in Defaults.Values)
                    {
                        if ((defaults.Libraries == null) || (defaults.Programs == null) || (defaults.Licenses == null) || (defaults.Licenses.Count == 0))
                        {
                            Writer.WriteLine("WARNING: missing data for licensing type '{0}'", defaults.AppliesTo ?? "<default>");
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
                Check(project, standalone);
                Writer.DecreaseIndent();
            } // CheckLicensingFile

            private void Check(VsProject project, bool standalone)
            {
                _changed = false;
                var filename = Helper.GetLicensingFilename(project, standalone);
                if (!File.Exists(filename))
                {
                    Writer.WriteLine("ERROR: file '{0}' not found", Path.GetFileName(filename));
                    return;
                } // if

                _data = XmlSerialization.Deserialize<LicensingData>(filename);
                _project = project;

                // check licensed
                _data.Licensed = CheckProjectLicensedItem();
                if (_data.Licensed == null) return; // unable to continue
                CheckLicensed();
                OverrideLicensed();

                // check third-party
                CheckThirdParty(_data.ThirdParty , "<Licensing><ThirdParty>");
                CheckThirdParty(_data.Dependencies?.ThirdParty, "<Dependencies><ThirdParty>");

                // check licenses
                CheckLicensesReferences();
                CheckLicensesText();

                if (!_changed) return;

                Writer.WriteLine("Info: saving changes to '{0}'", filename);
                XmlSerialization.Serialize(filename, _data);
            } // Check

            #region Licensed item

            private LicensedItem CheckProjectLicensedItem()
            {
                var licensed = _data.Licensed;
                if (!Defaults.TryGetValue(_project.LicensingDefaultsKey ?? "", out _projectDefaults))
                {
                    Writer.WriteLine("ERROR: licensing defaults key '{0}' not found", _project.LicensingDefaultsKey ?? "<default>");
                    return null;
                } // if

                switch (licensed)
                {
                    case LicensedProgram program:
                        if (_project.IsLibrary)
                        {
                            Writer.WriteLine("WARNING: licensed item is marked as '{0}', but the project is a library", licensed.Type);
                            licensed = program.Morph();
                            _changed = true;
                            _projectLicensedDefaults = _projectDefaults.Libraries;
                        }
                        else
                        {
                            _projectLicensedDefaults = _projectDefaults.Programs;
                        } // if-else

                        break;

                    case LicensedLibrary library:
                        if (!_project.IsLibrary)
                        {
                            Writer.WriteLine("WARNING: licensed item is marked as '{0}', but the project is not a library", licensed.Type);
                            licensed = library.Morph();
                            _changed = true;
                            _projectLicensedDefaults = _projectDefaults.Programs;
                        }
                        else
                        {
                            _projectLicensedDefaults = _projectDefaults.Libraries;
                        }

                        break;

                    default:
                        Writer.WriteLine("ERROR: licensed item type '{0}' is not supported", licensed.Type);
                        break;
                } // switch

                return licensed;
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
                licensed.Terms = Validate(licensed.Terms, _projectLicensedDefaults.Terms, nameof(licensed.Terms));
                licensed.LicenseId = Validate(licensed.LicenseId, _projectLicensedDefaults.LicenseId, nameof(licensed.LicenseId));

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
                } // Validate
            } // CheckLicensed

            private void OverrideLicensed()
            {
                var licensed = _data.Licensed;

                licensed.Product = Override(_options.OverrideProduct, licensed.Product, _projectLicensedDefaults.Product, nameof(licensed.Product));
                licensed.Authors = Override(_options.OverrideAuthors, licensed.Authors, _projectLicensedDefaults.Authors, nameof(licensed.Authors));
                licensed.Copyright = Override(_options.OverrideCopyright, licensed.Copyright, _projectLicensedDefaults.Copyright, nameof(licensed.Copyright));
                licensed.LicenseId = Override(_options.OverrideLicense, licensed.LicenseId, _projectLicensedDefaults.LicenseId, nameof(licensed.LicenseId));
                licensed.Terms = Override(_options.OverrideTerms, licensed.Terms, _projectLicensedDefaults.Terms, nameof(licensed.Terms));

                T Override<T>(bool @override, T value, T newValue, string item) where T : IEquatable<T>
                {
                    if (!@override) return value;
                    if (value.Equals(newValue)) return value;

                    Writer.WriteLine("Info: overriding <{0}><{1}>", licensed.Type, item);
                    _changed = true;
                    return newValue;
                } // local override
            } // OverrideLicensed

            #endregion

            #region Third-party

            private void CheckThirdParty(List<ThirdPartyDependency> thirdParty, string listName)
            {
                if ((thirdParty == null) || (thirdParty.Count == 0)) return;

                foreach (var component in thirdParty)
                {
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
                        Writer.WriteLine("Info: component {0} '{1}' has no license", listName, component.Name ?? "<null>");
                    } // if

                    if (string.IsNullOrWhiteSpace(component.Authors) && string.IsNullOrWhiteSpace(component.Copyright))
                    {
                        Writer.WriteLine("ERROR: Info: component {0} '{1}' has no authors or no copyright", listName, component.Name ?? "<null>");
                    } // if
                } // foreach component
            } // CheckThirdParty

            #endregion

            #region Licenses

            private void CheckLicensesReferences()
            {
                var licensesIds = CheckLicensesIds();

                if (!licensesIds.Contains(_data.Licensed.LicenseId))
                {
                    LicenseIdNotFound(_data.Licensed.LicenseId, $"<Licensing><{_data.Licensed.Type}>{_data.Licensed.Name}");
                } // if

                CheckThirdPartyLicensesIds(_data.ThirdParty, "ThirdParty", "Data");
                CheckThirdPartyLicensesIds(_data.Dependencies?.ThirdParty, "ThirdParty", "Dependencies");

                HashSet<string> CheckLicensesIds()
                {
                    if ((_data.Licenses == null) || (_data.Licenses.Count == 0))
                    {
                        Writer.WriteLine("WARNING: <Licenses> is null or empty");
                        return new HashSet<string>();
                    } // if

                    var ids = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
                    foreach (var id in _data.Licenses.Select(license => license.Id))
                    {
                        if (ids.Contains(id))
                        {
                            Writer.WriteLine("ERROR: duplicated license id: '{0}'", id);
                        }
                        else
                        {
                            ids.Add(id);
                        } // if-else
                    } // foreach license

                    return ids;
                } // CheckLicensesIds

                void LicenseIdNotFound(string id, string from)
                {
                    Writer.WriteLine("WARNING: license id '{0}' referenced in '{1}' not found", id, from);
                    Writer.IncreaseIndent();
                    if (LicensesPool.TryGetValue(id, out var license))
                    {
                        Writer.WriteLine("INFO: adding license '{0}' ({1}) from pool to <Licenses>", id, license.Name);
                        _data.Licenses.Add(license);
                        _changed = true;
                    }
                    else
                    {
                        Writer.WriteLine("ERROR: license id '{0}' not found in licenses pool", id);
                    } // if

                    Writer.DecreaseIndent();
                } // LicenseIdNotFound

                void CheckThirdPartyLicensesIds(IReadOnlyCollection<ThirdPartyDependency> components, string nodeName, string parent)
                {
                    if ((components == null) || (components.Count == 0)) return;

                    var missing = from component in components
                                  where !licensesIds.Contains(component.LicenseId)
                                  select component;

                    foreach (var component in missing)
                    {
                        var id = component.LicenseId;
                        LicenseIdNotFound(id, $"<{parent}><{nodeName}>{component.Name}");
                    } // foreach
                } //CheckThirdPartyLicensesIds
            } // CheckLicensesReferences

            private void CheckLicensesText()
            {
                if ((_data.Licenses == null) || _data.Licenses.Count == 0) return;

                for (var index = 0; index < _data.Licenses.Count; index++)
                {
                    var license = _data.Licenses[index];
                    if (LicensesPool.TryGetValue(license.Id, out var poolLicense))
                    {
                        if (string.Compare(license.Text, poolLicense.Text, StringComparison.InvariantCulture) == 0) continue;

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

            #endregion
        } // class Checker
    } // partial class LicensingMaintenance
} // namespace