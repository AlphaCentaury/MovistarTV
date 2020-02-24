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
using System.Windows.Forms;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using IpTviewr.Common;
using IpTviewr.Common.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    partial class LicensingMaintenance
    {
        internal static class Helper
        {
            [NotNull]
            public static string GetLicensingFilename(VsProject project, bool standalone)
            {
                return GetLicenseFilename(project, standalone, "licensing.xml");
            } // GetLicensingFilename

            [NotNull]
            public static string GetLicenseFilename(VsProject project, bool standalone, string filename)
            {
                if (project == null) throw new ArgumentNullException(nameof(project));
                if (project.Filename == null) throw new ArgumentException();
                if (VsSolutionProject.IsSolutionFolderProject(project)) standalone = true;

                var projectFolder = Path.GetDirectoryName(project.Filename) ?? Path.GetPathRoot(project.Filename);
                return standalone switch
                {
                    true => Path.Combine(projectFolder, filename),
                    false => Path.Combine(projectFolder, project.Name + "." + filename)
                };
            } // GetLicenseFilename

            public static LicensingDefaultsPool ReadLicensingDefaultsPool(string path, IToolOutputWriter writer)
            {
                try
                {
                    writer.IncreaseIndent();
                    return new LicensingDefaultsPool(path, writer);
                }
                finally
                {
                    writer.DecreaseIndent();
                } // try-finally
            } // ReadLicensingDefaultsPool

            public static IReadOnlyDictionary<string, License> ReadLicensesPool(string path)
            {
                var poolFile = Path.Combine(path, "licensing.pool.licenses.xml");
                if (!File.Exists(poolFile)) return new Dictionary<string, License>();

                var pool = XmlSerialization.Deserialize<LicensesPool>(poolFile);
                return pool.Licenses == null ? new Dictionary<string, License>() : pool.Licenses.ToDictionary(license => license.Id, StringComparer.InvariantCulture);
            } // ReadLicensesPool

            public static LicensingThirdPartyPool ReadThirdPartyPool(string path)
            {
                var poolFile = Path.Combine(path, "licensing.pool.thirdparty.xml");
                if (!File.Exists(poolFile)) return new LicensingThirdPartyPool();

                return XmlSerialization.Deserialize<LicensingThirdPartyPool>(poolFile);
            } // ReadThirdPartyPool

            public static string GetImplicitDefaultsPath(VsSolution solution)
            {
                return Path.Combine(solution.SolutionPath, ".defaults");
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
                    writer.WriteLine("ERROR: no solution folder or file has been specified");
                    return null;
                } // if

                try
                {
                    if (string.Equals(Path.GetExtension(args.Arguments[0]), ".sln", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!File.Exists(args.Arguments[0]))
                        {
                            writer.WriteLine("ERROR: solution file '{0}' not found", args.Arguments[0]);
                            return null;
                        } // if

                        writer.WriteLine("Loading solution from file '{0}'...", args.Arguments[0]);
                        solution = VsSolution.FromFile(args.Arguments[0], ProjectReaders);
                    }
                    else
                    {
                        if (!Directory.Exists(args.Arguments[0]))
                        {
                            writer.WriteLine("ERROR: solution folder '{0}' not found", args.Arguments[0]);
                            return null;
                        } // if

                        writer.WriteLine("Loading solution from folder '{0}'...", args.Arguments[0]);
                        solution = VsSolution.FromFolder(args.Arguments[0], ProjectReaders);
                    } // if-else
                }
                catch (Exception e)
                {
                    writer.WriteException(e);
                    return null;
                } // try

                defaultsPath = GetImplicitDefaultsPath(solution);
                if (args.Switches.ContainsKey("Defaults"))
                {
                    var value = defaultsPath = args.Switches["Defaults"];
                    if (!Directory.Exists(defaultsPath))
                    {
                        writer.WriteLine("WARNING: defaults folder '{0}' not found. Assuming '{1}'", value, defaultsPath);
                    }
                    else
                    {
                        defaultsPath = value;
                    } // if-else
                }
                else
                {
                    writer.WriteLine("Info: defaults path: '{0}'", defaultsPath);
                } // if

                if (!args.MultiValueSwitches.ContainsKey("Action"))
                {
                    writer.WriteLine("Info: no actions have been specified. Ending execution");
                    return null;
                } // if

                if (args.Switches.ContainsKey("Options"))
                {
                    var optionsFile = args.Switches["Options"];
                    if (File.Exists(optionsFile))
                    {
                        writer.WriteLine("Loading options file '{0}'", optionsFile);
                        try
                        {
                            options = XmlSerialization.Deserialize<LicensingToolOptions>(optionsFile);
                            return args;
                        }
                        catch (Exception e)
                        {
                            writer.WriteException(e, $"Unable to read options from file '{optionsFile}'");
                        } // try-catch
                    }
                    else
                    {
                        writer.WriteLine("Options file '{0}' not found", optionsFile);
                    } // if-else
                } // if

                writer.WriteLine("Info: using default options");
                options = new LicensingToolOptions(true);

                return args;
            } // GetCliArguments

            #region ForEach

            public static void ForEachProject<TAction>(TAction action, string operation) where TAction : ProjectAction
            {
                bool writeTimestamps, absoluteTimestamps;

                if (action == null) throw new ArgumentNullException(nameof(action));
                if (action.Solution == null) throw new ArgumentException();
                if (action.Writer == null) throw new ArgumentException();

                var where = "action.Init()";
                try
                {
                    PushTimestamps(true, true);
                    var started = action.Writer.ElapsedTime;
                    action.Writer.WriteLine("{0} started", operation);
                    PopTimestamps();

                    action.Init();

                    if (!ForEachProject(action, action.Solution.RootFolder))
                    {
                        action.Writer.WriteLine();
                        action.Writer.WriteLine(">>>> {0} <<<<", new OperationCanceledException().Message);
                        action.Writer.WriteLine();
                    } // if

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

            public static bool ForEachProject<TAction>(TAction action, VsFolder folder) where TAction : ProjectAction
            {
                if ((folder.Projects?.Count ?? 0) > 0)
                {
                    foreach (var project in folder.Projects)
                    {
                        try
                        {
                            action.Do(project, folder.Projects.Count == 1);
                        }
                        catch (OperationCanceledException)
                        {
                            return false;
                        }
                        catch (Exception e)
                        {
                            action.Writer.WriteException(e, project.Filename);
                        } // try-catch
                    } // foreach
                } // if

                if ((folder.Folders?.Count ?? 0) == 0) return true;

                foreach (var subFolder in folder.Folders)
                {
                    if (!ForEachProject(action, subFolder)) return false;
                } // foreach

                return true;
            } // ForEachProject

            #endregion
        } // class Helper
    } // partial class LicensingMaintenance
} // namespace
