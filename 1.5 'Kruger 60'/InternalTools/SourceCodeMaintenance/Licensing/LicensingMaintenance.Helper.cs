using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public sealed partial class LicensingMaintenance
    {
        internal static class Helper
        {
            public static string GetLicensingFilename(VsProject project, bool standalone)
            {
                if (project == null) throw new ArgumentNullException(nameof(project));
                if (project.Filename == null) throw new ArgumentException();

                var projectFolder = Path.GetDirectoryName(project.Filename) ?? "";
                return standalone switch
                {
                    true => Path.Combine(projectFolder, "licensing.xml"),
                    false => Path.Combine(projectFolder, project.Name + ".licensing.xml")
                };
            } // GetLicensingFilename

            #region ForEach

            public static void ForEachProject<TData>(TData data, Action<TData, VsProject, bool> projectAction, Action<TData> init = null) where TData : OperationData
            {
                if (data == null) throw new ArgumentNullException(nameof(data));
                if (projectAction == null) throw new ArgumentNullException(nameof(projectAction));
                if (data.Solution == null) throw new ArgumentException();
                if (data.Writer == null) throw new ArgumentException();

                init?.Invoke(data);
                if (data.Solution.RootFolder == null)
                {
                    data.Writer.WriteLine("Solution has no root folder. Aborting execution.");
                    return;
                } // if

                ForEachProject(data, data.Solution.RootFolder, projectAction);
            } // ForEachProject

            public static void ForEachProject<TData>(TData data, VsSolutionFolder solutionFolder, Action<TData, VsProject, bool> projectAction) where TData : OperationData
            {
                if ((solutionFolder.Projects?.Count ?? 0) > 0)
                {
                    foreach (var project in solutionFolder.Projects)
                    {
                        try
                        {
                            projectAction(data, project, solutionFolder.Projects.Count == 1);
                        }
                        catch (Exception e)
                        {
                            data.Writer.WriteException(e, project.Filename);
                        } // try-catch
                    } // foreach
                } // if

                if ((solutionFolder.Folders?.Count ?? 0) == 0) return;

                foreach (var folder in solutionFolder.Folders)
                {
                    ForEachProject(data, folder, projectAction);
                } // foreach
            } // ForEachProject

            #endregion

            #region Operation: CreateMissingLicensingFiles

            public static void CreateLicensingFile(CreatorData data, VsProject project, bool standalone)
            {
                data.Token.ThrowIfCancellationRequested();

                data.Writer.WriteLine("Project {0}: <{1}>", project.Name, project.Filename);
                var filename = LicensingMaintenance.Helper.GetLicensingFilename(project, standalone);

                data.Writer.IncreaseIndent();
                if (File.Exists(filename))
                {
                    data.Writer.WriteLine("<{0}> not needed", Path.GetFileName(filename));
                }
                else
                {
                    data.Writer.WriteLine("Creating <{0}>", Path.GetFileName(filename));
                    var licensingData = GetLicensingData(data, project);
                    XmlSerialization.Serialize(filename, licensingData);
                } // if-else
                data.Writer.DecreaseIndent();
            } // CreateLicensingFile

            private static LicensingData GetLicensingData(CreatorData data, VsProject project)
            {
                var defaults = data.Defaults[project.LicensingDefaultsKey ?? ""];
                var licensed = project.IsLibrary switch
                {
                    true => (LicensedItem)new LicensedLibrary(),
                    false => new LicensedProgram { IsConsoleApp = (project.Type != "WinExe") }
                };

                var licensingData = new LicensingData
                {
                    Licensing = new AlphaCentaury.Licensing.Data.Serialization.Licensing
                    {
                        Licensed = licensed
                    },
                    Licenses = defaults.Licenses
                };

                var licensedDefaults = project.IsLibrary ? defaults.Libraries : defaults.Programs;
                licensed.Product = licensedDefaults.Product;
                licensed.Terms = licensedDefaults.Terms;
                licensed.Authors = licensedDefaults.Authors;
                licensed.Copyright = licensedDefaults.Copyright;
                licensed.LicenseId = licensedDefaults.LicenseId;
                licensed.Remarks = licensedDefaults.Remarks;

                return licensingData;
            } // GetLicensingData

            #endregion

            #region Operation: CheckLicensingFiles

            #endregion
        } // class Helper
    } // partial class LicensingMaintenance
} // namespace