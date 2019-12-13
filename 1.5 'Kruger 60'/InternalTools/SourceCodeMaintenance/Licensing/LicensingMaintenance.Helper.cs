using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using IpTviewr.Common;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    partial class LicensingMaintenance
    {
        internal static class Helper
        {
            public static string GetLicensingFilename(VsProject project, bool standalone)
            {
                return GetLicenseFilename(project, standalone, "licensing.xml");
            } // GetLicensingFilename

            public static string GetLicenseFilename(VsProject project, bool standalone, string filename)
            {
                if (project == null) throw new ArgumentNullException(nameof(project));
                if (project.Filename == null) throw new ArgumentException();

                var projectFolder = Path.GetDirectoryName(project.Filename) ?? "";
                return standalone switch
                {
                    true => Path.Combine(projectFolder, filename),
                    false => Path.Combine(projectFolder, project.Name + "." + filename)
                };
            } // GetLicenseFilename

            public static IReadOnlyDictionary<string, LicensingDefaults> ReadLicensingDefaults(string path)
            {
                var defaultsFiles = from file in Directory.EnumerateFiles(path, "licensing.defaults*.xml", SearchOption.TopDirectoryOnly)
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

            public static LicensingThirdPartyPool ReadThirdPartyPool(string path)
            {
                var poolFile = Path.Combine(path, "thirdparty.pool.xml");
                if (!File.Exists(poolFile)) return new LicensingThirdPartyPool();

                return XmlSerialization.Deserialize<LicensingThirdPartyPool>(poolFile);
            } // ReadThirdPartyPool

            public static string GetImplicitDefaultsPath()
            {
                return Path.Combine(Application.StartupPath, "Licensing\\Defaults");
            } // GetImplicitDefaultsPath

            public static CommandLineArguments GetCliArguments(IReadOnlyList<string> arguments, IToolOutputWriter writer, out VsSolution solution, out LicensingToolOptions options, out string defaultsPath)
            {
                solution = null;
                options = new LicensingToolOptions(true);
                defaultsPath = null;

                var args = new CommandLineArguments(StringComparer.InvariantCultureIgnoreCase);
                args.Parse(arguments);

                if (args.Arguments.Count == 0)
                {
                    writer.WriteLine("ERROR: no solution folder has been specified");
                    return null;
                } // if

                if (!Directory.Exists(args.Arguments[0]))
                {
                    writer.WriteLine("ERROR: solution folder '{0}' not found", args.Arguments[0]);
                    return null;
                } // if

                if (args.Switches.ContainsKey("Defaults"))
                {
                    var value = defaultsPath = args.Switches["Defaults"];
                    if (!Directory.Exists(defaultsPath))
                    {
                        defaultsPath = GetImplicitDefaultsPath();
                        writer.WriteLine("WARNING: '/defaults:{0}' is invalid. Assuming '{1}'", value, defaultsPath);
                    } // if
                }
                else
                {
                    defaultsPath = GetImplicitDefaultsPath();
                    writer.WriteLine("Info: defaults path: '{0}'", defaultsPath);
                } // if

                if (!args.MultiValueSwitches.ContainsKey("Action"))
                {
                    writer.WriteLine("Info: no actions have been specified. Ending execution");
                    return null;
                } // if

                try
                {
                    writer.WriteLine("Loading solution from folder '{0}'...", args.Arguments[0]);
                    solution = VsSolution.FromFolder(args.Arguments[0], LicensingMaintenance.ProjectReaders);
                }
                catch (Exception e)
                {
                    writer.WriteException(e);
                    return null;
                } // try

                if (!args.Switches.ContainsKey("Options")) return args;

                var optionsFile = args.Switches["Options"];
                if (optionsFile.Length == 0)
                {
                    optionsFile = Path.Combine(Application.StartupPath, "LicensingTool.options.xml");
                    writer.WriteLine("Using default options file '{0}'", optionsFile);
                } // if

                if (Directory.Exists(optionsFile))
                {
                    optionsFile = Path.Combine(optionsFile, "LicensingTool.options.xml");
                    writer.WriteLine("Using options file '{0}'", optionsFile);
                } // if

                try
                {
                    options = XmlSerialization.Deserialize<LicensingToolOptions>(optionsFile);
                }
                catch (Exception e)
                {
                    writer.WriteException(e, $"Unable to read options from file '{optionsFile}'");
                } // try-catch

                return args;
            } // GetCliArguments

            #region ForEach

            public static void ForEachProject<TAction>(TAction action, string operation) where TAction : ProjectAction
            {
                bool writeTimestamps, absoluteTimestamps;

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
                    PushTimestamps(true, true);
                    var started = action.Writer.ElapsedTime;
                    action.Writer.WriteLine("{0} started", operation);
                    PopTimestamps();

                    action.Init();

                    ForEachProject(action, action.Solution.RootFolder);

                    where = "action.End()";
                    action.End();

                    PushTimestamps(true, true);
                    action.Writer.WriteLine("{0} ended. {1:g} elapsed", operation, action.Writer.ElapsedTime - started);
                    PopTimestamps();
                }
                catch (Exception e)
                {
                    action.Writer.WriteException(e, where);
                } // try-catch

                void PushTimestamps(bool write, bool absolute)
                {
                    writeTimestamps = action.Writer.WriteTimestamps;
                    absoluteTimestamps = action.Writer.AbsoluteTimestamps;
                    action.Writer.WriteTimestamps = write;
                    action.Writer.AbsoluteTimestamps = absolute;
                } // PushTimestamps

                void PopTimestamps()
                {
                    action.Writer.WriteTimestamps = writeTimestamps;
                    action.Writer.AbsoluteTimestamps = absoluteTimestamps;
                } // PropTimestamps
            } // ForEachProject

            public static void ForEachProject<TAction>(TAction action, VsSolutionFolder solutionFolder) where TAction : ProjectAction
            {
                if ((solutionFolder.Projects?.Count ?? 0) > 0)
                {
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