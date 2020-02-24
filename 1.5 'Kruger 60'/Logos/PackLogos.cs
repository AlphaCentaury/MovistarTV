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
using System.IO.Compression;
using SaveAs = IpTviewr.Native.WindowsIcon.SaveAs;

namespace IpTViewr.Setup.Logos
{
    internal sealed class PackLogos
    {
        private string _fromFolder;
        private string _toFolder;

        public bool ProcessArguments(IDictionary<string, string> arguments)
        {
            _fromFolder = GetFromFolder(arguments);
            if (_fromFolder == null) return false;

            _toFolder = GetToFolder(arguments);
            if (_toFolder == null) return false;

            return true;
        } // ProcessArguments

        public Program.Result Execute()
        {
            const CompressionLevel compression = CompressionLevel.Fastest;

            var sizes = new short[] { 32, 48, 64, 128, 256 };
            var saveAs = new [] { SaveAs.Bmp, SaveAs.Bmp, SaveAs.Bmp, SaveAs.Png, SaveAs.Png };

            // providers
            var fromFolder = Path.Combine(_fromFolder, "Providers");
            var toFolder = Path.Combine(_toFolder, "Providers");
            Directory.CreateDirectory(toFolder);
            foreach (var folder in Directory.EnumerateDirectories(fromFolder, "*", SearchOption.TopDirectoryOnly))
            {
                if (Program.Verbose) Console.WriteLine($"PROVIDER: {folder}");
                var packager = new Packager(folder, sizes, saveAs, false);
                var zipFile = Path.Combine(toFolder, $"{Path.GetFileName(folder)}.zip");
                packager.PackLogos(zipFile, compression);
            } // foreach

            // services
            fromFolder = Path.Combine(_fromFolder, "Services");
            toFolder = Path.Combine(_toFolder, "Services");
            Directory.CreateDirectory(toFolder);
            foreach (var folder in Directory.EnumerateDirectories(fromFolder, "*", SearchOption.TopDirectoryOnly))
            {
                if (Program.Verbose) Console.WriteLine($"SERVICES: {folder}");
                var packager = new Packager(folder, sizes, saveAs);
                var zipFile = Path.Combine(toFolder, $"{Path.GetFileName(folder)}.zip");
                packager.PackLogos(zipFile, compression);
            } // foreach

            return Program.Result.Ok;
        } // Execute

        private static string GetFromFolder(IDictionary<string, string> arguments)
        {
            string fromFolder;
            var assemblyLocation = Path.GetDirectoryName(Program.EntryAssembly.Location);

            // FromFolder
            if (arguments.TryGetValue("from", out var value))
            {
                fromFolder = value;
            }
            else
            {
#if DEBUG
                if (assemblyLocation.EndsWith("\\bin\\debug", StringComparison.InvariantCultureIgnoreCase))
                {
                    fromFolder = Path.GetFullPath(Path.Combine(assemblyLocation, "..\\..\\"));
                }
                else
#endif
                {
                    Program.DisplayError("No 'from' folder has been specified");
                    return null;
                } // if-else
            } // if-else

            if (Directory.Exists(fromFolder)) return fromFolder;

            Program.DisplayError($"The specified 'from' folder does not exists: {fromFolder}");
            return null;
        } // GetFromFolder

        private static string GetToFolder(IDictionary<string, string> arguments)
        {
            string toFolder;

            if (arguments.TryGetValue("to", out var value))
            {
                toFolder = value;
                Directory.CreateDirectory(toFolder);
            }
            else
            {
                toFolder = Directory.GetCurrentDirectory();
            } // if-else

            return toFolder;
        } // GetToFolder
    } // class PackLogos
} // namespace
