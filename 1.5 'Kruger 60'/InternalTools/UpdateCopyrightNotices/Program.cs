// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlphaCentaury.IPTViewr.Internal.UpdateCopyrightNotices
{
    class Program
    {
        public static readonly string CopyrightHeader =
            "Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury\0" +
            "\0" +
            "All rights reserved, except those granted by the governing license of this software.\0" +
            "See 'license.txt' file in the project root for complete license information.\0" +
            "\0" +
            "http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury";

        private static string ReadLine;
        private static string[] CopyrightHeaderLines;
        private static string[] ExcludedPaths;
        private static IList<string> FileHeaderLines;

        static void Main(string[] args)
        {
            Console.WriteLine("Update/add copyright notices to project code");
            Console.WriteLine("Copyright (C) 2014-2019, GitHub user AlphaCentaury");
            Console.WriteLine("https://github.com/AlphaCentaury");
            Console.WriteLine();

            if (args.Length == 0)
            {
                Console.Error.WriteLine("Error: arguments not specified");
                return;
            } // if

            var arguments = new CommandLineArguments();
            arguments.Parse(args);
            if (!arguments.IsOk)
            {
                Console.Error.WriteLine("Error: unable to parse arguments");
                return;
            } // Console.WriteLine

            if (arguments.Switches.ContainsKey("exclude"))
            {
                ExcludedPaths = arguments.Switches["exclude"].Split(Path.PathSeparator);
            } // if

            var searchOptions = arguments.Switches.ContainsKey("r") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            CopyrightHeaderLines = CopyrightHeader.Split('\0');

            foreach (var path in arguments.Arguments)
            {
                Func<TextReader, bool> locateCopyrightHeaderFunc;
                Action<TextWriter> writeCopyrightHeaderAction;

                if (!Directory.Exists(path))
                {
                    Console.Error.WriteLine("Path not found: {0}", path);
                    continue;
                } // if

                if (IsPathExcluded(path)) continue;

                var lastPath = (string)null;
                foreach (var filename in Directory.EnumerateFiles(path, "*", searchOptions))
                {
                    // Is path excluded?
                    var currentPath = Path.GetDirectoryName(filename);
                    if (IsPathExcluded(currentPath)) continue;

                    var extension = Path.GetExtension(filename).ToLowerInvariant();
                    switch (extension)
                    {
                        case ".cs":
                            locateCopyrightHeaderFunc = LocateCsharpCopyrightHeader;
                            writeCopyrightHeaderAction = WriteCsharpCopyrightHeader;
                            break;

                        case ".xml":
                        case ".wxs":
                        case ".wxl":
                            locateCopyrightHeaderFunc = LocateXmlCopyrightHeader;
                            writeCopyrightHeaderAction = WriteXmlCopyrightHeader;
                            break;

                        default:
                            continue;
                    } // switch

                    // new path?
                    if (currentPath != lastPath)
                    {
                        Console.WriteLine(currentPath);
                        lastPath = currentPath;
                    } // if

                    // Process file
                    ProcessFile(filename, locateCopyrightHeaderFunc, writeCopyrightHeaderAction);
                } // foreach file
            } // foreach
        } // Main

        private static bool IsPathExcluded(string path)
        {
            if (ExcludedPaths == null) return false;

            foreach (var excluded in ExcludedPaths)
            {
                if (path.StartsWith(excluded))
                {
                    Console.Write("(Excluded) ");
                    Console.WriteLine(path);

                    return true;
                } // if
            } // foreach

            return false;
        } // IsPathExcluded

        private static void ProcessFile(string filename, Func<TextReader, bool> locateCopyrightHeaderFunc, Action<TextWriter> writeCopyrightHeaderAction)
        {
            if (locateCopyrightHeaderFunc == null) throw new ArgumentNullException(nameof(locateCopyrightHeaderFunc));
            if (writeCopyrightHeaderAction == null) throw new ArgumentNullException(nameof(writeCopyrightHeaderAction));

            Console.Write("\t");
            Console.Write(Path.GetFileName(filename));

            try
            {
                var result = UpdateHeaders(filename, locateCopyrightHeaderFunc, writeCopyrightHeaderAction);
                Console.WriteLine(result ? ": Ok" : ": not needed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(": Err");
                Console.Error.WriteLine("Exception: {0}", ex.GetType().Name);
                Console.Error.WriteLine(">> {0}", ex.Message);
                Console.Error.WriteLine(">> {0}", filename);
            } // tryHeader
        } // ProcessFile

        private static bool UpdateHeaders(string filename, Func<TextReader, bool> locateCopyrightHeaderFunc, Action<TextWriter> writeCopyrightHeaderAction)
        {
            var tempFilename = (string)null;

            using (var reader = new StreamReader(filename, Encoding.UTF8, true, short.MaxValue))
            {
                FileHeaderLines = null;

                if (!locateCopyrightHeaderFunc(reader))
                {
                    tempFilename = filename + "~";
                    using (var writer = new StreamWriter(tempFilename, false, new UTF8Encoding(false, true), short.MaxValue))
                    {
                        if (FileHeaderLines != null)
                        {
                            foreach (var line in FileHeaderLines)
                            {
                                writer.WriteLine(line);
                            } // foreach
                        } // if

                        writeCopyrightHeaderAction(writer);
                        CopyContents(reader, writer);
                    } // using writer
                } // if
            } // using reader

            if (tempFilename == null) return false;

            CopyBack(tempFilename, filename);

            return true;
        } // UpdateHeaders

        private static void CopyContents(TextReader reader, TextWriter writer)
        {
            if (ReadLine != null) writer.WriteLine(ReadLine);

            while ((ReadLine = reader.ReadLine()) != null)
            {
                writer.WriteLine(ReadLine);
            } // while
        } // CopyContents

        private static void CopyBack(string tempFilename, string filename)
        {
            var buffer = new byte[4096 * 16];

            using (var writer = new FileStream(filename, FileMode.Truncate, FileAccess.Write))
            {
                using (var reader = new FileStream(tempFilename, FileMode.Open, FileAccess.Read))
                {
                    var readBytes = 0;

                    while ((readBytes = reader.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        writer.Write(buffer, 0, readBytes);
                    } // while
                } // using
            } // using

            File.Delete(tempFilename);
        } // CopyBack

        private static void AddFileHeaderLine(string line)
        {
            if (FileHeaderLines == null) FileHeaderLines = new List<string>();

            FileHeaderLines.Add(line);
        } // AddFileHeaderLine

        #region C# files

        private static bool LocateCsharpCopyrightHeader(TextReader reader)
        {
            var index = 0;

            while ((ReadLine = reader.ReadLine()) != null)
            {
                if (index >= CopyrightHeaderLines.Length) return true;

                if (!ReadLine.StartsWith("//")) return false;

                var line = ReadLine.Substring(2, ReadLine.Length - 2).Trim();
                if (line != CopyrightHeaderLines[index++]) break;
            } // while

            SkipWrongCsharpCopyrightHeader(reader);

            return false;
        } // LocateCsharpCopyrightHeader

        private static void SkipWrongCsharpCopyrightHeader(TextReader reader)
        {
            while ((ReadLine = reader.ReadLine()) != null)
            {
                var line = ReadLine.Trim();
                if (line.Length == 0) continue;
                if (line.StartsWith("//")) continue;

                break;
            } // while
        } // SkipWrongCsharpCopyrightHeader

        private static void WriteCsharpCopyrightHeader(TextWriter writer)
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

        private static bool LocateXmlCopyrightHeader(TextReader reader)
        {
            var index = 0;
            var isHeader = false;

            while ((ReadLine = reader.ReadLine()) != null)
            {
                if (index >= CopyrightHeaderLines.Length) return true;

                var line = ReadLine.Trim();
                if (!isHeader)
                {
                    if (line.Length == 0) continue;

                    if (line.StartsWith("<?"))
                    {
                        AddFileHeaderLine(ReadLine);
                        continue;
                    } // if

                    if (!line.StartsWith("<!--")) return false;
                    if (line.EndsWith("-->")) // one line comment
                    {
                        AddFileHeaderLine(ReadLine);
                        return false;
                    } // if

                    isHeader = true;
                }
                else
                {
                    if (line != CopyrightHeaderLines[index++])
                    {
                        if (line.EndsWith("-->")) return false;
                        break;
                    } // if
                } // if-else
            } // while

            SkipWrongXmlCopyrightHeader(reader);

            return false;
        } // LocateXmlCopyrightHeader

        private static void SkipWrongXmlCopyrightHeader(TextReader reader)
        {
            while ((ReadLine = reader.ReadLine()) != null)
            {
                var line = ReadLine.Trim();
                if (line.Length == 0) continue;
                if (line.StartsWith("<")) break;
                if (line.EndsWith("-->"))
                {
                    ReadLine = null;
                    break;
                } // if
            } // while
        } // SkipWrongXmlCopyrightHeader

        private static void WriteXmlCopyrightHeader(TextWriter writer)
        {
            writer.WriteLine("<!--");
            foreach (var line in CopyrightHeaderLines)
            {
                writer.WriteLine(line);
            } // foreach line
            writer.WriteLine("-->");
        } // WriteXmlCopyrightHeader

        #endregion
    } // Program
} // namespace
