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
using System.Threading;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    internal sealed class Creator : ProjectAction
    {
        public Creator(VsSolution solution, IToolOutputWriter writer, string defaultsPath, CancellationToken token) : base(solution, writer, token)
        {
            writer.WriteLine("Reading licensing defaults...");
            Defaults = LicensingMaintenance.Helper.ReadLicensingDefaultsPool(defaultsPath, writer);
        } // constructor

        public LicensingDefaultsPool Defaults { get; }

        public override void Do(VsProject project, bool standalone)
        {
            Token.ThrowIfCancellationRequested();

            Writer.WriteLine("Project '{0}'", project.Name);
            var filename = LicensingMaintenance.Helper.GetLicensingFilename(project, standalone);

            Writer.IncreaseIndent();
            if (File.Exists(filename))
            {
                // Writer.WriteLine(Verbose, "File '{0}' not needed", Path.GetFileName(filename));
            }
            else
            {
                Writer.WriteLine("Info: Creating '{0}'...", Path.GetFileName(filename));
                var licensingData = GetLicensingData(project);
                licensingData.FilePath = filename;
                XmlSerialization.Serialize(filename, licensingData);
            } // if-else
            Writer.DecreaseIndent();
        } // ForProject

        public LicensingData GetLicensingData(VsProject project)
        {
            return project.GetLicensingData(GetLicensingDefaults(Defaults, project, true));
        } // GetLicensingData

        public static LicensingDefaults GetLicensingDefaults(LicensingDefaultsPool defaultsPool, VsProject project, bool getDefault)
        {
            var isSolution = VsSolutionProject.IsSolutionProject(project);
            var defaults = defaultsPool[project.LicensingDefaultsKey] ?? (isSolution ? defaultsPool["#solution#"] : null);
            if (getDefault) defaults ??= defaultsPool[null];

            return defaults;
        } // GetLicensingDefaults
    } // class Creator
} // namespace
