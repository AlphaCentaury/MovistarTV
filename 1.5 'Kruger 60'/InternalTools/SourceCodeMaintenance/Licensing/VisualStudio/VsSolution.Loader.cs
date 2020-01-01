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
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public partial class VsSolution
    {
        private struct ProjectData
        {
            public string GuidType;
            public string Name;
            public string Path;
            public Guid Guid;
        } // struct ProjectData

        private class LoaderFolder
        {
            public string Partial { get; set; }

            public List<LoaderFolder> Folders { get; set; }

            public IEnumerable<ProjectData> Projects { get; set; }

            public override string ToString()
            {
                return $"{Partial} = {Folders?.Count}";
            } // ToString
        } // LoaderFolder

        private class Loader
        {
            private const string SolutionFolderProjectGuid = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";

            private readonly CancellationToken _token;
            private readonly Dictionary<string, IVsProjectReader> _projectReaders;
            private readonly string[] _extensions;
            private readonly List<VsProject> _allProjects;
            private List<ProjectData> _solutionProjects;
            private Guid _guid;
            private Guid _solutionFolderProjectGuid;
            private string _solutionFolder;

            public Loader(IEnumerable<IVsProjectReader> readers, CancellationToken token) : this(GetProjectReaders(readers), token)
            {
                // no-op
            } // constructor

            private Loader(Dictionary<string, IVsProjectReader> readers, CancellationToken token)
            {
                _token = token;
                _projectReaders = readers;
                _extensions = _projectReaders.Keys.ToArray();
                _allProjects = new List<VsProject>();
                _solutionFolderProjectGuid = Guid.Parse(SolutionFolderProjectGuid);
            } // constructor

            public VsSolution FromFolder(string solutionFolder)
            {
                _solutionFolder = solutionFolder;

                var rootFolder = GetRootVsFolder(solutionFolder);
                var subSolutions = GetSubSolutions(solutionFolder);
                var solution = new VsSolution(true, solutionFolder, rootFolder, _allProjects, Guid.NewGuid(), subSolutions)
                {
                    LicensingDefaultsScope = GetLicensingDefaultsKey(_solutionFolder),
                };

                PropagateLicensingDefaultsKey(solution);

                return solution;
            } // FromFolder

            public VsSolution FromFile(string solutionFile)
            {
                return FromFile(solutionFile, null);
            } // FromFile

            public VsSolution FromFile(VsSolution parentSolution, string solutionFile)
            {
                return FromFile(solutionFile, parentSolution?.GuidDictionary);
            } // FromFile

            private VsSolution FromFile(string solutionFile, IReadOnlyDictionary<Guid, VsProject> guidDictionary)
            {
                _solutionFolder = Path.GetDirectoryName(solutionFile) ?? Path.GetPathRoot(solutionFile);

                ReadSolutionFile(solutionFile);
                var q = from data in _solutionProjects
                        let path = Path.GetDirectoryName(data.Path) ?? Path.GetPathRoot(data.Path)
                        group data by path;
                var root = GetLoaderFolderRoot(q);
                var rootFolder = ToVsFolder(root, guidDictionary);

                var solution = new VsSolution(false, solutionFile, rootFolder, _allProjects, _guid)
                {
                    LicensingDefaultsScope = GetLicensingDefaultsKey(_solutionFolder),
                };

                PropagateLicensingDefaultsKey(solution);

                return solution;
            } // FromFile

            List<VsSolution> GetSubSolutions(string solutionFolder)
            {
                var guidDictionary = _allProjects.ToDictionary(project => project.Guid);
                var solutionFiles = Directory.EnumerateFiles(solutionFolder, "*.sln", SearchOption.AllDirectories);
                var subSolutions = from file in solutionFiles
                                   let loader = new Loader(_projectReaders, _token)
                                   select loader.FromFile(file, guidDictionary);
                var subSolutionsList = subSolutions.ToList();
                if (subSolutionsList.Count == 0) subSolutionsList = null;

                return subSolutionsList;
            } // GetSubSolutions

            private VsFolder GetRootVsFolder(string solutionFolder)
            {
                return GetVsFolder(solutionFolder);
            } // GetRootVsFolder

            private VsFolder GetVsFolder(string fromFolder)
            {
                _token.ThrowIfCancellationRequested();
                var projects = from file in Directory.EnumerateFiles(fromFolder)
                               let reader = GetReader(Path.GetExtension(file))
                               where reader != null
                               select ReadVsProject(reader, file, _token);
                var projectsList = projects.ToList();

                _token.ThrowIfCancellationRequested();
                var folders = from folder in Directory.EnumerateDirectories(fromFolder)
                              let vsFolder = GetVsFolder(folder)
                              where vsFolder != null
                              select vsFolder;
                var foldersList = folders.ToList();

                if (projectsList.Count == 0) projectsList = null;
                if (foldersList.Count == 0) foldersList = null;
                if ((projectsList == null) && (foldersList == null))
                {
                    return null;
                } // if

                if (projectsList != null)
                {
                    _allProjects.AddRange(projectsList);
                } // if

                return new VsFolder(Path.GetFileName(fromFolder), projectsList, foldersList, GetLicensingDefaultsKey(fromFolder));
            } // GetVsFolder

            private static LoaderFolder GetLoaderFolderRoot(IEnumerable<IGrouping<string, ProjectData>> projectFolders)
            {
                var folders = new Dictionary<string, LoaderFolder>(StringComparer.InvariantCulture);
                var root = new LoaderFolder() { Folders = new List<LoaderFolder>(), Partial = "" };
                folders.Add("", root);

                var chSeparator = new string(Path.DirectorySeparatorChar, 1);
                var parent = root;
                foreach (var pf in projectFolders)
                {
                    var fragments = pf.Key.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    for (var count = 0; count <= fragments.Length; count++)
                    {
                        var partialPath = string.Join(chSeparator, fragments, 0, count);
                        if (folders.TryGetValue(partialPath, out var folder))
                        {
                            parent = folder;
                        }
                        else
                        {
                            folder = new LoaderFolder { Folders = new List<LoaderFolder>(), Partial = Path.GetFileName(partialPath) };
                            parent.Folders.Add(folder);
                            folders.Add(partialPath, folder);
                            parent = folder;
                        } // if-else
                    } // for count

                    parent.Projects = pf;
                } // foreach

                return root;
            } // GetLoaderFolderRoot

            private VsFolder ToVsFolder(LoaderFolder loaderFolder, IReadOnlyDictionary<Guid, VsProject> guidDictionary)
            {
                var fromFolder = Path.Combine(_solutionFolder, loaderFolder.Partial);

                List<VsProject> projectsList;
                List<VsFolder> foldersList;

                if (loaderFolder.Projects != null)
                {
                    var projects = from data in loaderFolder.Projects
                                   let vsProject = GetVsProject(data, guidDictionary)
                                   where vsProject != null
                                   select vsProject;
                    projectsList = projects.ToList();
                    _allProjects.AddRange(projectsList);
                }
                else
                {
                    projectsList = null;
                } // if-else

                if (loaderFolder.Folders.Count > 0)
                {
                    var folders = from folder in loaderFolder.Folders
                                  orderby folder.Partial
                                  select ToVsFolder(folder, guidDictionary);

                    foldersList = folders.ToList();
                }
                else
                {
                    foldersList = null;
                } // if-else

                return new VsFolder(loaderFolder.Partial, projectsList, foldersList, GetLicensingDefaultsKey(fromFolder));
            } // ToVsFolder

            private void ReadSolutionFile([NotNull] string solutionFile)
            {
                string line;

                var haveHeader = false;
                var inGlobals = false;
                var inGuidSection = false;
                var skip = (string)null;
                var list = new List<ProjectData>();
                using var reader = new StreamReader(solutionFile);
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length == 0) continue;
                    if (line.StartsWith("#")) continue;
                    if (line.StartsWith(@"Microsoft Visual Studio Solution File", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (haveHeader) throw new InvalidDataException();
                        haveHeader = true;
                    } // if

                    if (line.StartsWith(@"Project", StringComparison.InvariantCulture))
                    {
                        var data = GetProjectData(line);
                        if (!string.Equals(data.GuidType, SolutionFolderProjectGuid, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (data.GuidType != null) list.Add(data);
                        } // if

                        skip = "EndProject";
                    } // if Project

                    if (string.Equals(line, @"Global", StringComparison.InvariantCulture))
                    {
                        inGlobals = true;
                        continue;
                    } // if Global

                    if (inGlobals && line.StartsWith(@"GlobalSection", StringComparison.InvariantCulture))
                    {
                        if (!string.Equals(GetGlobalSectionName(line), "ExtensibilityGlobals", StringComparison.InvariantCulture))
                        {
                            skip = "EndGlobalSection";
                            continue;
                        } // if

                        inGuidSection = true;
                        continue;
                    } // if GlobalSection

                    if (inGuidSection && line.StartsWith(@"SolutionGuid", StringComparison.InvariantCulture))
                    {
                        var index = line.IndexOf('=');
                        if (index < 0) throw new InvalidDataException();
                        var guidText = line.Substring(index + 1);
                        _guid = Guid.Parse(guidText);

                        inGuidSection = false;
                        skip = "EndGlobalSection";
                        continue;
                    } // if SolutionGuid

                    if (string.Equals(line, @"EndGlobal", StringComparison.InvariantCulture))
                    {
                        inGlobals = false;
                        continue;
                    } // if EndGlobal

                    if ((skip != null) && line.StartsWith(skip, StringComparison.InvariantCulture))
                    {
                        skip = null;
                    } // if
                } // while

                _solutionProjects = list;
            } // ReadSolutionFile

            private static Dictionary<string, IVsProjectReader> GetProjectReaders(IEnumerable<IVsProjectReader> readers)
            {
                if (readers == null) return new Dictionary<string, IVsProjectReader>();

                var q = from reader in readers
                        let supported = reader.SupportedExtensions
                        where supported != null
                        from extension in supported
                        select new { Extension = extension, Reader = reader };

                var projectReaders = q.ToDictionary(item => item.Extension, item => item.Reader, StringComparer.InvariantCultureIgnoreCase);

                return projectReaders;
            } // GetProjectReaders

            private static VsProject ReadVsProject(IVsProjectReader reader, string file, CancellationToken token)
            {
                if (reader == null) return null;
                token.ThrowIfCancellationRequested();

                var extension = Path.GetExtension(file);
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                var project = reader.Read(stream, extension);
                project.Name = Path.GetFileNameWithoutExtension(file);
                project.Filename = file;

                return project;
            } // ReadVsProject

            private VsProject GetVsProject(ProjectData data, IReadOnlyDictionary<Guid, VsProject> guidDictionary)
            {
                if (guidDictionary != null)
                {
                    if (guidDictionary.TryGetValue(data.Guid, out var vsProject))
                    {
                        return vsProject;
                    } // if
                } // if

                var reader = GetReader(Path.GetExtension(data.Path));
                return ReadVsProject(reader, Path.Combine(_solutionFolder, data.Path), _token);

            } // GetVsProject

            private IVsProjectReader GetReader(string extension)
            {
                return _projectReaders.TryGetValue(extension, out var value) ? value : null;
            } // GetReader

            private static ProjectData GetProjectData(string line)
            {
                string guidType;
                var result = new ProjectData();

                var start = line.IndexOf('(');
                if (start < 0) return result;

                start++;
                var index = line.IndexOf(')', start);
                if (index < 0) return result;
                guidType = line.Substring(start, index - start);

                index = line.IndexOf('=', index + 1);
                if (index < 0) return result;

                var fragment = new StringBuilder();
                var fragments = new List<string>();

                var quotedText = false;
                index++;
                for (; index < line.Length; index++)
                {
                    var c = line[index];

                    if ((c == ',') && !quotedText)
                    {
                        fragments.Add(fragment.ToString().Trim());
                        fragment.Clear();
                        continue;
                    } // if

                    if (c == '"')
                    {
                        quotedText = !quotedText;
                        continue;
                    } // if

                    fragment.Append(c);
                } // for

                var last = fragment.ToString().Trim();
                if (last.Length > 0) fragments.Add(last);

                if (fragments.Count != 3) return result;

                result.GuidType = Unquote(guidType);
                result.Name = fragments[0];
                result.Path = fragments[1];
                result.Guid = Guid.Parse(Unquote(fragments[2]));
                return result;

                string Unquote(string text)
                {
                    if (text[0] != '"') return text;
                    if (text[text.Length - 1] != '"') return text.Substring(1);
                    return text.Substring(1, text.Length - 2);
                } // Unquote
            } // GetProjectPath

            private static string GetGlobalSectionName(string line)
            {
                var index = line.IndexOf('(');
                if (index < 0) return null;
                var endIndex = line.IndexOf(')', index + 1);
                if (endIndex < 0) return null;

                var name = line.Substring(index + 1, endIndex - index - 1);
                return name;
            } // GetGlobalSectionName
        } // class Loader
    } // partial class VsSolution
} // namespace
