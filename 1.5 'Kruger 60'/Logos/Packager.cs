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
using System.Drawing;
using System.IO;
using IpTviewr.Native;
using System.IO.Compression;

namespace IpTViewr.Setup.Logos
{
    internal sealed class Packager
    {
        private readonly string _fromFolder;
        private readonly short[] _sizes;
        private readonly WindowsIcon.SaveAs[] _saveAs;
        private readonly bool _isService;
        private readonly int _fromRelativeIndex;

        public Packager(string fromFolder, short[] sizes, WindowsIcon.SaveAs[] saveAs, bool isService = true)
        {
            if (string.IsNullOrEmpty(fromFolder)) throw new ArgumentNullException(nameof(fromFolder));
            if ((sizes == null) || (sizes.Length == 0)) throw new ArgumentNullException(nameof(sizes));
            if ((sizes == null) || (sizes.Length == 0)) throw new ArgumentNullException(nameof(saveAs));
            if (sizes.Length != saveAs.Length) throw new ArgumentException();

            _fromFolder = fromFolder;
            _sizes = sizes;
            _saveAs = saveAs;
            _isService = isService;

            _fromRelativeIndex = _fromFolder.EndsWith("" + Path.DirectorySeparatorChar)
                ? _fromFolder.Length
                : _fromFolder.Length + 1;
        } // constructor

        public void PackLogos(string toZipFile, CompressionLevel compression)
        {
            CreateIcons();

            using (var output = new FileStream(toZipFile, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var zip = new ZipArchive(output, ZipArchiveMode.Create))
                {
                    foreach (var folder in Directory.EnumerateDirectories(_fromFolder, "*", SearchOption.AllDirectories))
                    {
                        if (Program.Verbose)
                        {
                            Console.WriteLine($"- Packing {folder.Substring(_fromRelativeIndex)}");
                        } // if

                        foreach (var file in Directory.EnumerateFiles(folder, "*", SearchOption.TopDirectoryOnly))
                        {
                            var entryName = file.Substring(_fromRelativeIndex);
                            var entry = zip.CreateEntry(entryName, compression);
                            using (var stream = entry.Open())
                            {
                                var bytes = File.ReadAllBytes(file);
                                stream.Write(bytes, 0, bytes.Length);
                            } // using
                        } // foreach file
                    } // foreach folder
                } // using zip
            } // using output
        } // PackLogos

        private void CreateIcons()
        {
            foreach (var folder in Directory.EnumerateDirectories(_fromFolder, "*", SearchOption.AllDirectories))
            {
                // folder has images?
                if (!File.Exists(Path.Combine(folder, $"{_sizes[0]}.png"))) continue;

                // icon file exists?
                var iconFilename = GetIconFilename(folder);
                if (!File.Exists(iconFilename))
                {
                    CreateIcon(folder, iconFilename);
                    break;
                } // if

                // any image is more recent than icon file?
                // if so, recreate icon
                var iconDate = GetFileDateTime(iconFilename);

                foreach (var size in _sizes)
                {
                    var imageFile = Path.Combine(folder, $"{size}.png");
                    if (!File.Exists(imageFile))
                    {
                        throw new FileNotFoundException($"Image file not found at folder '{folder}'", imageFile);
                    } // if

                    if (GetFileDateTime(imageFile) <= iconDate) continue;

                    CreateIcon(folder, iconFilename);
                    break;
                } // foreach
            } // foreach folder
        } // CreateIcon

        private void CreateIcon(string folder, string iconFilename)
        {
            if (Program.Verbose)
            {
                Console.WriteLine($"> Recreating icon: {iconFilename.Substring(_fromRelativeIndex)}");
            } // if

            var icon = new WindowsIcon(_sizes.Length);
            for (var index = 0; index < _sizes.Length; index++)
            {
                var imageFile = Path.Combine(folder, $"{_sizes[index]}.png");
                if (!File.Exists(imageFile))
                {
                    throw new FileNotFoundException($"Image file not found at folder '{folder}'", imageFile);
                } // if
                icon.AddImage((Bitmap)Image.FromFile(imageFile), _saveAs[index]);
            } // for index

            // delete existing icon(s)
            if (File.Exists(iconFilename)) File.Delete(iconFilename);
            icon.Save(iconFilename);
            icon.Dispose();
        } // CreateIcon

        private string GetIconFilename(string folder)
        {
            var iconName = _isService
                ? $"{Path.GetFileName(Path.GetDirectoryName(folder))}.ico"
                : $"{Path.GetFileName(folder)}.ico";

            return Path.Combine(folder, iconName);
        } // GetIconFilename

        private static DateTime GetFileDateTime(string filename)
        {
            var info = new FileInfo(filename);
            var date = info.CreationTimeUtc;
            if (info.LastWriteTimeUtc > date) date = info.LastWriteTimeUtc;

            return date;
        } // GetFileDateTime
    } // class Packager
} // namespace
