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

using IpTviewr.Common;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.CopyrightNotices
{
    [Export(typeof(IMaintenanceTool))]
    [ExportMetadata("Name", "Update Copyright notices")]
    [ExportMetadata("Guid", "{BA624943-EAAB-49CC-A9CD-FD9CEA6EC3CE}")]
    [ExportMetadata("HasParameters", true)]
    [ExportMetadata("HasFileParameters", true)]
    [ExportMetadata("CliName", "UpdateCopyright")]

    public class UpdateCopyrightNotices : IMaintenanceTool
    {
        private readonly string[] CopyrightHeaderLines = SolutionVersion.CopyrightHeaderLines;
        private string[] ExcludedPaths;
        private IList<string> FileHeaderLines;
        private IToolOutputWriter Writer;
        private string CurrentLine;

        public string SelectFileFilter => "";

        public void Execute([NotNull] IReadOnlyList<string> args, [NotNull] IToolOutputWriter writer, CancellationToken token)
        {
            Writer = writer;

            Writer.WriteLine("Update/add copyright notices to project code");
            Writer.WriteLine(SolutionVersion.DefaultCopyright);
            Writer.WriteLine(SolutionVersion.AssemblyCompany);
            Writer.WriteLine();

            if (args.Count == 0)
            {
                Writer.WriteLine("Error: arguments not specified");
                return;
            } // if

            var arguments = new CommandLineArguments();
            arguments.Parse(args);
            if (!arguments.IsOk)
            {
                Writer.WriteLine("Error: unable to parse arguments");
                return;
            } // Writer.WriteLine

            if (arguments.Switches.ContainsKey("exclude"))
            {
                ExcludedPaths = arguments.Switches["exclude"].Split(Path.PathSeparator);
            } // if

            var searchOptions = arguments.Switches.ContainsKey("r") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            ProcessPaths(arguments, searchOptions);

            ExcludedPaths = null;
            FileHeaderLines = null;
            Writer = null;
            CurrentLine = null;
        } // Execute

        public Form GetUi() => throw new NotSupportedException();

        public void ShowUsage(IToolOutputWriter writer) => throw new NotSupportedException();

        #region Implementation

        private void ProcessPaths(CommandLineArguments arguments, SearchOption searchOptions)
        {
            foreach (var path in arguments.Arguments)
            {
                if (!Directory.Exists(path))
                {
                    Writer.WriteLine($"Path not found: {path}");
                    continue;
                } // if

                if (IsPathExcluded(path)) continue;

                ProcessPath(searchOptions, path);
            } // foreach
        } // ProcessPaths

        private void ProcessPath(SearchOption searchOptions, string path)
        {
            var lastPath = (string)null;
            Writer.IncreaseIndent();
            foreach (var filename in Directory.EnumerateFiles(path, "*", searchOptions))
            {
                // is path excluded?
                var currentPath = Path.GetDirectoryName(filename);
                if (IsPathExcluded(currentPath)) continue;
                if (filename == null) continue;

                var extension = Path.GetExtension(filename).ToLowerInvariant();

                var (locateFunc, writeAction) = GetFromExtension(extension);
                if (locateFunc == null) continue;

                // new path?
                if (currentPath != lastPath)
                {
                    Writer.DecreaseIndent();
                    Writer.WriteLine(currentPath);
                    Writer.IncreaseIndent();
                    lastPath = currentPath;
                } // if

                // Process file
                ProcessFile(filename, locateFunc, writeAction);
            } // foreach file
            Writer.DecreaseIndent();
        } // ProcessPath

        private (Func<TextReader, bool> LocateFunc, Action<TextWriter> WriteAction) GetFromExtension(string extension)
        {
            Func<TextReader, bool> locateFunc;
            Action<TextWriter> writeAction;

            switch (extension)
            {
                case ".cs":
                    locateFunc = LocateCsharpCopyrightHeader;
                    writeAction = WriteCsharpCopyrightHeader;
                    break;

                case ".xml":
                case ".wxs":
                case ".wxl":
                case ".wxi":
                    locateFunc = LocateXmlCopyrightHeader;
                    writeAction = WriteXmlCopyrightHeader;
                    break;

                default:
                    return (null, null);
            } // switch

            return (locateFunc, writeAction);
        } // GetFromExtension

        private bool IsPathExcluded(string path)
        {
            if (ExcludedPaths == null) return false;

            if (!ExcludedPaths.Any(path.StartsWith)) return false;

            Writer.WriteLine($"(Excluded) {path}");
            return true;
        } // IsPathExcluded

        private void ProcessFile([NotNull] string filename, [NotNull] Func<TextReader, bool> locateFunc, [NotNull] Action<TextWriter> writeAction)
        {
            try
            {
                var result = UpdateHeaders(filename, locateFunc, writeAction);
                if (result)
                {
                    Writer.WriteLine($"{Path.GetFileName(filename)}: Updated headers");
                } // if
            }
            catch (Exception ex)
            {
                Writer.WriteException(ex, filename);
            } // tryHeader
        } // ProcessFile

        private bool UpdateHeaders([NotNull] string filename, [NotNull] Func<TextReader, bool> locateFunc, [NotNull] Action<TextWriter> writeAction)
        {
            var tempFilename = (string)null;

            using (var reader = new StreamReader(filename, XmlSerialization.Utf8NoBomEncoding.Value, true, short.MaxValue))
            {
                FileHeaderLines = null;

                if (!locateFunc(reader))
                {
                    tempFilename = Path.GetTempFileName();
                    using var writer = new StreamWriter(tempFilename, false, XmlSerialization.Utf8NoBomEncoding.Value, short.MaxValue);
                    if (FileHeaderLines != null)
                    {
                        foreach (var line in FileHeaderLines)
                        {
                            writer.WriteLine(line);
                        } // foreach
                    } // if

                    writeAction(writer);
                    CopyContents(reader, writer);
                } // if
            } // using reader

            if (tempFilename == null) return false;

            CopyBack(tempFilename, filename);

            return true;
        } // UpdateHeaders

        private void CopyContents(TextReader reader, TextWriter writer)
        {
            if (CurrentLine != null) writer.WriteLine(CurrentLine);

            while ((CurrentLine = reader.ReadLine()) != null)
            {
                writer.WriteLine(CurrentLine);
            } // while
        } // CopyContents

        private static void CopyBack(string tempFilename, string filename)
        {
            var buffer = new byte[4096 * 16];

            using (var writer = new FileStream(filename, FileMode.Truncate, FileAccess.Write))
            {
                using var reader = new FileStream(tempFilename, FileMode.Open, FileAccess.Read);
                int readBytes;

                while ((readBytes = reader.Read(buffer, 0, buffer.Length)) != 0)
                {
                    writer.Write(buffer, 0, readBytes);
                } // while
            } // using

            File.Delete(tempFilename);
        } // CopyBack

        private void AddFileHeaderLine(string line)
        {
            if (FileHeaderLines == null) FileHeaderLines = new List<string>();

            FileHeaderLines.Add(line);
        } // AddFileHeaderLine

        #endregion

        #region C# files

        private bool LocateCsharpCopyrightHeader(TextReader reader)
        {
            var index = 0;
            while ((CurrentLine = reader.ReadLine()) != null)
            {
                if (index >= CopyrightHeaderLines.Length) return true;

                if (!CurrentLine.StartsWith("// ")) return false;

                var line = CurrentLine.Substring(3).TrimEnd();
                if (line != CopyrightHeaderLines[index++]) break;
            } // while

            SkipWrongCsharpCopyrightHeader(reader);

            return false;
        } // LocateCsharpCopyrightHeader

        private void SkipWrongCsharpCopyrightHeader(TextReader reader)
        {
            while ((CurrentLine = reader.ReadLine()) != null)
            {
                var line = CurrentLine.Trim();
                if (line.Length == 0) continue;
                if (line.StartsWith("//")) continue;

                break;
            } // while
        } // SkipWrongCsharpCopyrightHeader

        private void WriteCsharpCopyrightHeader(TextWriter writer)
        {
            foreach (var line in CopyrightHeaderLines)
            {
                writer.Write("// ");
                writer.WriteLine(line);
            } // foreach line
            writer.WriteLine();
        } // WriteCsharpCopyrightHeader

        #endregion

        #region XML files

        private bool LocateXmlCopyrightHeader(TextReader reader)
        {
            var index = 0;
            var isHeader = false;

            while ((CurrentLine = reader.ReadLine()) != null)
            {
                if (index >= CopyrightHeaderLines.Length) return true;

                var line = CurrentLine.TrimEnd();
                if (!isHeader)
                {
                    if (line.Length == 0) continue;

                    if (line.StartsWith("<?"))
                    {
                        AddFileHeaderLine(CurrentLine);
                        continue;
                    } // if

                    if (!line.StartsWith("<!--")) return false;
                    if (line.EndsWith("-->")) // one line comment
                    {
                        AddFileHeaderLine(CurrentLine);
                        return false;
                    } // if

                    isHeader = true;
                }
                else
                {
                    if (line == CopyrightHeaderLines[index++]) continue;
                    if (line.EndsWith("-->")) return false;
                    break;
                } // if-else
            } // while

            SkipWrongXmlCopyrightHeader(reader);

            return false;
        } // LocateXmlCopyrightHeader

        private void SkipWrongXmlCopyrightHeader(TextReader reader)
        {
            while ((CurrentLine = reader.ReadLine()) != null)
            {
                var line = CurrentLine.Trim();
                if (line.Length == 0) continue;
                if (line.StartsWith("<")) break;
                if (!line.EndsWith("-->")) continue;
                CurrentLine = null;
                break;
            } // while
        } // SkipWrongXmlCopyrightHeader

        private void WriteXmlCopyrightHeader(TextWriter writer)
        {
            writer.WriteLine("<!--");
            foreach (var line in CopyrightHeaderLines)
            {
                writer.WriteLine(line);
            } // foreach line
            writer.WriteLine("-->");
        } // WriteXmlCopyrightHeader

        #endregion
    } // UpdateCopyrightNotices
} // namespace
