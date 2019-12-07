using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            public static IReadOnlyDictionary<string, LicensingDefaults> ReadLicensingDefaults(string path)
            {
                var defaultsFiles = from file in Directory.EnumerateFiles(path, "*licensing.defaults.xml", SearchOption.TopDirectoryOnly)
                                    select XmlSerialization.Deserialize<LicensingDefaults>(file);

                return defaultsFiles.ToDictionary(@default => @default.AppliesTo ?? "", StringComparer.InvariantCulture);
            } // ReadLicensingDefaults

            public static IReadOnlyDictionary<string, License> ReadLicensesPool(string path)
            {
                var poolFile = Path.Combine(path, "licenses.pool.xml");
                if (!File.Exists(poolFile)) return new Dictionary<string, License>();

                var pool = XmlSerialization.Deserialize<LicensesPool>(poolFile);
                return pool.Licenses == null ? new Dictionary<string, License>() : pool.Licenses.ToDictionary(license => license.Id, StringComparer.InvariantCulture);
            } // ReadLicensesPool

            #region ForEach

            public static void ForEachProject<TAction>(TAction action) where TAction : ProjectAction
            {
                if (action == null) throw new ArgumentNullException(nameof(action));
                if (action.Solution == null) throw new ArgumentException();
                if (action.Writer == null) throw new ArgumentException();

                if (action.Solution.RootFolder == null)
                {
                    action.Writer.WriteLine("Solution has no root folder. Aborting execution.");
                    return;
                } // if

                var where = "action.Init()";
                try
                {
                    action.Init();
                    
                    ForEachProject(action, action.Solution.RootFolder);
                    
                    where = "action.End()";
                    action.End();
                }
                catch (Exception e)
                {
                    action.Writer.WriteException(e, where);
                } // try-catch
            } // ForEachProject

            public static void ForEachProject<TAction>(TAction action, VsSolutionFolder solutionFolder) where TAction : ProjectAction
            {
                if ((solutionFolder.Projects?.Count ?? 0) > 0)
                {
                    action.Writer.WriteLine("Solution folder: {0}", solutionFolder.Name);
                    action.Writer.IncreaseIndent();
                    foreach (var project in solutionFolder.Projects)
                    {
                        try
                        {
                            action.Do(project, solutionFolder.Projects.Count == 1);
                        }
                        catch (Exception e)
                        {
                            action.Writer.WriteException(e, project.Filename);
                        } // try-catch
                    } // foreach
                    action.Writer.DecreaseIndent();
                } // if

                if ((solutionFolder.Folders?.Count ?? 0) == 0) return;

                foreach (var folder in solutionFolder.Folders)
                {
                    ForEachProject(action, folder);
                } // foreach
            } // ForEachProject

            #endregion
        } // class Helper
    } // partial class LicensingMaintenance
} // namespace