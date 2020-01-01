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
            try
            {
                var filename = LicensingMaintenance.Helper.GetLicensingFilename(project, standalone);
                if (!File.Exists(filename))
                {
                    Writer.WriteLine("ERROR: file '{0}' not found", Path.GetFileName(filename));
                    return;
                } // if

                Writer.WriteLine("Loading '{0}'", Path.GetFileName(filename));
                var data = XmlSerialization.Deserialize<LicensingData>(filename);
                data.FilePath = filename;
                Update(data, project);
            }
            finally
            {
                Writer.DecreaseIndent();
            } // finally
        } // Do

        public override void End()
        {
            UpdateSolution();
            UpdateSubSolutions();
        } // End

        private void Update(LicensingData data, VsProject project)
        {
            // clear all dependencies, except runtime
            data.Dependencies?.Libraries?.RemoveAll(library => library.DependencyType != LicensedDependencyType.Dynamic);

            if ((project.ReferencedProjects != null) && (project.ReferencedProjects.Count != 0))
            {
                if (data.Dependencies == null) data.Dependencies = new LicensingDependencies();
                if (data.Dependencies.Libraries == null) data.Dependencies.Libraries = new List<LibraryDependency>(project.ReferencedProjects.Count);
                var libraries = data.Dependencies.Libraries;

                foreach (var projectGuid in project.ReferencedProjects)
                {
                    if (!Solution.GuidDictionary.TryGetValue(projectGuid, out var referenced))
                    {
                        Writer.WriteLine("WARNING: unable to find referenced project {0:B}", projectGuid);
                        continue;
                    } // if

                    // add a skeleton reference
                    // missing values will be filled later, at End()
                    libraries.Add(new LibraryDependency
                    {
                        DependencyType = LicensedDependencyType.Direct,
                        Name = referenced.Namespace
                    });
                } // foreach
            } // if

            AllLicensingData.Add(data);
            NamespaceData.Add(data.Licensed.Name, data);
        } // Update

        private void UpdateSolution()
        {
            CompleteSkeletonDependencies();

            Writer.WriteLine("Expanding dependencies...");
            try
            {
                AllLicensingData.ExpandDependencies();
            }
            catch (Exception e)
            {
                Writer.WriteException(e);
                Writer.WriteLine("ERROR: unable to expand dependencies. Aborting execution.");
                return;
            } // try-catch

            Writer.WriteLine("Adjusting licenses...");
            Writer.IncreaseIndent();
            foreach (var data in AllLicensingData)
            {
                AdjustLicenses(data);
            } // foreach

            Writer.DecreaseIndent();

            Writer.WriteLine("Saving licensing files...");
            Writer.IncreaseIndent();
            AllLicensingData.ForEach((data) =>
            {
                try
                {
                    // TODO: add to options
                    // File.Copy(data.FilePath, data.FilePath + ".backup", true);
                    XmlSerialization.Serialize(data.FilePath, data);
                }
                catch (Exception e)
                {
                    Writer.WriteException(e, $"Unable to save '{data.FilePath}'");
                } // try-catch
            });
            Writer.DecreaseIndent();
        } // UpdateSolution

        private void CompleteSkeletonDependencies()
        {
            var libraries = from data in AllLicensingData
                where data.Dependencies?.LibrariesSpecified ?? false
                from library in data.Dependencies.Libraries
                select library;

            foreach (var library in libraries)
            {
                if (!NamespaceData.TryGetValue(library.Name, out var referencedData))
                {
                    Writer.WriteLine("ERROR: unable to find licensing data for '{0}'", library.Name);
                    continue;
                } // if

                referencedData.Licensed.CopyTo(library);
            } // foreach
        } // CompleteSkeletonDependencies

        private void AdjustLicenses(LicensingData data)
        {
            if ((data.Dependencies?.LibrariesSpecified ?? false) == false) return;

            if (data.Licenses == null) data.Licenses = new List<License>();
            var set = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            data.Licenses.ForEach(license => set.Add(license.Id));

            foreach (var library in data.Dependencies.Libraries)
            {
                var referenced = NamespaceData[library.Name];
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

        private void UpdateSubSolutions()
        {
            if (Solution.SubSolutions == null) return;
            foreach (var subVsSolution in Solution.SubSolutions)
            {
                var updater = new Updater(subVsSolution, Writer, Token);
                LicensingMaintenance.Helper.ForEachProject(updater, "Update solution dependencies");
            } // foreach
        } // UpdateSubSolutions
    } // class Updater
} // namespace
