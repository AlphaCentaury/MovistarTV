// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    [Export(typeof(IMaintenanceTool))]
    [ExportMetadata("Name", "Licensing maintenance")]
    [ExportMetadata("Guid", "{13B1F04C-F4E9-4C13-832F-8FCBC5673098}")]
    [ExportMetadata("HasParameters", false)]
    [ExportMetadata("HasUi", true)]
    public sealed partial class LicensingMaintenance : IMaintenanceTool
    {
        #region Implementation of IMaintenanceTool

        public void Execute([NotNull] IReadOnlyList<string> arguments, IToolOutputWriter writer)
        {
            throw new NotImplementedException();
        } // Execute

        public void ShowUsage(IToolOutputWriter writer)
        {
            throw new NotSupportedException();
        } // ShowUsage

        public Form GetUi() => new LicensingForm();

        public string SelectFileFilter => throw new NotSupportedException();

        #endregion

        #region Operations: Create

        public static void CreateMissingLicensingFiles(VsSolution solution, IToolOutputWriter writer)
        {
            CreateMissingLicensingFiles(solution, writer, CancellationToken.None);
        } // CheckLicensingFiles

        public static void CreateMissingLicensingFiles(VsSolution solution, IToolOutputWriter writer, CancellationToken token)
        {
            try
            {
                var data = new CreatorData(solution, writer, token);
                Helper.ForEachProject(data, Helper.CreateLicensingFile);
            }
            catch (Exception e)
            {
                writer.WriteException(e);
            } // try-catch
        } // Creator

        public static Task CreateMissingLicensingFilesAsync(VsSolution solution, IToolOutputWriter writer, CancellationToken token)
        {
            var task = new Task(() => CreateMissingLicensingFiles(solution, writer, token), token, TaskCreationOptions.LongRunning);
            task.Start();

            return task;
        } // CreateMissingLicensingFilesAsync


        #endregion
    } // class LicensingMaintenance
} // namespace
