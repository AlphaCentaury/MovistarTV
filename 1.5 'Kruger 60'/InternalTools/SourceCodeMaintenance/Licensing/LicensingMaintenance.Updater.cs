using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Threading;
using AlphaCentaury.Licensing.Data;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using IpTviewr.Common.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public sealed partial class LicensingMaintenance
    {
        internal sealed class Updater : ProjectAction
        {
            private readonly List<LicensingData> AllLicensingData;
            private readonly IDictionary<string, LicensingData> NamespaceData;

            public Updater(VsSolution solution, IToolOutputWriter writer, CancellationToken token) : base(solution, writer, token)
            {
                AllLicensingData = new List<LicensingData>(solution.AllProjects.Count);
                NamespaceData = new Dictionary<string, LicensingData>(StringComparer.InvariantCulture);
            } // constructor

            public override void Do(VsProject project, bool standalone)
            {
                Token.ThrowIfCancellationRequested();
                Writer.WriteLine("Project '{0}'", project.Name);
                Writer.IncreaseIndent();
                var filename = Helper.GetLicensingFilename(project, standalone);
                if (!File.Exists(filename))
                {
                    Writer.WriteLine("ERROR: file '{0}' not found", Path.GetFileName(filename));
                    return;
                } // if

                var data = XmlSerialization.Deserialize<LicensingData>(filename);
                Writer.WriteLine("Loading '{0}'", Path.GetFileName(filename));
                data.FilePath = filename;
                Update(data, project);
                Writer.DecreaseIndent();
            } // Do

            private void Update(LicensingData data, VsProject project)
            {
                // clear all dependencies, except runtime
                data.Dependencies?.Libraries?.RemoveAll(library => !library.IsDynamicDependency);

                if ((project.ReferencedProjects != null) && (project.ReferencedProjects.Count != 0))
                {
                    if (data.Dependencies == null) data.Dependencies = new Dependencies();
                    if (data.Dependencies.Libraries == null) data.Dependencies.Libraries = new List<LibraryDependency>(project.ReferencedProjects.Count);
                    var libraries = data.Dependencies.Libraries;

                    foreach (var projectGuid in project.ReferencedProjects)
                    {
                        if (!Solution.GuidDictionary.TryGetValue(projectGuid, out var referenced))
                        {
                            Writer.WriteLine("WARNING: unable to find referenced project {0:B}", projectGuid);
                            continue;
                        } // if

                        libraries.Add(new LibraryDependency
                        {
                            IsDirectDependency = true,
                            Namespace = referenced.Namespace,
                            Assembly = referenced.AssemblyName,
                            // LicenseId = ??? Proper licenseId will be added when all projects are processed, at End()
                        });
                    } // foreach
                } // if

                AllLicensingData.Add(data);
                NamespaceData.Add(data.Licensed.Name, data);
            } // Update

            public override void End()
            {
                Writer.WriteLine("Expanding dependencies...");
                try
                {
                    AllLicensingData.ExpandDependencies();
                }
                catch (Exception e)
                {
                    Writer.WriteException(e);
                    Writer.WriteLine("ERROR: unable to expand dependencies. Aborting execution.");
                } // try-catch

                foreach (var data in AllLicensingData)
                {
                    AdjustLicenses(data);
                } // foreach

                Writer.WriteLine("Saving licensing files...");
                //AllLicensingData.ForEach((data) =>
                //{
                //    try
                //    {
                //        XmlSerialization.Serialize(data.FilePath, data);
                //    }
                //    catch (Exception e)
                //    {
                //        Writer.WriteException(e);
                //        Writer.WriteLine("ERROR: unable to save '{0}'", data.FilePath);
                //    } // try-catch
                //});
            } // End

            private void AdjustLicenses(LicensingData data)
            {
                if ((data.Dependencies?.LibrariesSpecified ?? false) == false) return;

                if (data.Licenses == null) data.Licenses = new List<License>();
                var set = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
                data.Licenses.ForEach(license => set.Add(license.Id));

                foreach (var library in data.Dependencies.Libraries)
                {
                    var referenced = NamespaceData[library.Namespace];
                    var licenseId = referenced.Licensed.LicenseId;
                    library.LicenseId = licenseId;
                    if (set.Contains(licenseId)) continue;

                    var license = referenced.GetLicense(licenseId);
                    if (license == null)
                    {
                        Writer.WriteLine("ERROR: license id '{0}' not found for {1} {2}", referenced.Licensed.Type, referenced.Licensed.Name);
                        continue;
                    } // if

                    data.Licenses.Add(license);
                } // foreach
            } // AdjustLicenses
        } // class Updater
    } // partial class LicensingMaintenance
} // namespace