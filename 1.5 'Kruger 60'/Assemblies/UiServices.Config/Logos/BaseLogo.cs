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
using System.Collections.Concurrent;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using IpTviewr.Common;

namespace IpTviewr.UiServices.Configuration.Logos
{
    public abstract class BaseLogo
    {
        private static readonly ConcurrentDictionary<string, ZipArchive> Zips = new ConcurrentDictionary<string, ZipArchive>();

        private BaseLogo()
        {
            // no-op
        } // constructor

        internal BaseLogo(ILogoMapping mapping, string mappingKey, string entry, string uniqueKey)
        {
            Mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
            MappingKey = mappingKey ?? throw new ArgumentNullException(nameof(mappingKey));
            MappingEntry = entry ?? throw new ArgumentNullException(nameof(entry));
            Key = uniqueKey ?? throw new ArgumentNullException(nameof(uniqueKey));
        } // constructor

        internal ILogoMapping Mapping { get; }

        internal string MappingKey { get; }

        internal string MappingEntry { get; }

        public string Key { get; }

        public Image GetImage(LogoSize logoSize, bool noExceptions = true)
        {
            if (!IsSizeAvailable(logoSize))
            {
                throw new NotSupportedException();
            } // if

            var stream = (Stream)null;
            try
            {
                stream = Mapping.GetImage(MappingKey, MappingEntry, logoSize);
                if (stream != null) return Image.FromStream(stream);

                if (noExceptions) return GetBrokenFile(logoSize);
                throw new FileNotFoundException(ImageNotFoundExceptionText, $@"{MappingKey}: {MappingEntry}");
            }
            catch (FileNotFoundException ex) when (noExceptions == false)
            {
                throw new FileNotFoundException(ImageLoadExceptionText, $@"{MappingKey}: {MappingEntry}", ex);
            }
            catch (OutOfMemoryException ex) when (noExceptions == false)
            {
                throw new InvalidOperationException(string.Format(ImageLoadExceptionText, $@"{MappingKey}: {MappingEntry}"), ex);
            }
            catch (Exception) when (noExceptions)
            {
                return GetBrokenFile(logoSize);
            }
            finally
            {
                stream?.Dispose();
            } // try-finally
        } // GetImage

#if DEBUG
        public bool ImageExists(LogoSize logoSize, out bool substituted)
        {
            return Mapping.ImageExists(MappingKey, MappingEntry, logoSize, out substituted);
        } // ImageExists
#endif

        public string GetLogoIconPath()
        {
            var iconPath = Path.Combine(AppConfig.Current.Folders.Logos.Cache,
                $"{TextUtils.SanitizeFilename(Key, "~")}.ico");

            var zipEntry = Mapping.GetIcon(MappingKey, MappingEntry, out var lastModifiedUtc);
            if (zipEntry == null) return null;

            if (File.Exists(iconPath))
            {
                File.SetAttributes(iconPath, FileAttributes.Normal);
                var info = new FileInfo(iconPath);
                var last = info.CreationTimeUtc;
                if (info.LastWriteTimeUtc > last) last = info.LastWriteTimeUtc;

                if (lastModifiedUtc <= last) return iconPath;
            } // if

            Directory.CreateDirectory(AppConfig.Current.Folders.Logos.Cache);
            using (var output = new FileStream(iconPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var buffer = new byte[1024];
                int read;

                using var stream = zipEntry.Open();
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, read);
                } // while
            } // using

            // ReSharper disable once AssignmentIsFullyDiscarded [assignment has a side effect]
            _ = new FileInfo(iconPath)
            {
                CreationTimeUtc = lastModifiedUtc,
                LastWriteTimeUtc = lastModifiedUtc,
                LastAccessTimeUtc = lastModifiedUtc
            };

            return iconPath;
        } // GetLogoIconPath

        #region Static methods

        public static Size LogoSizeToSize(LogoSize logoSize)
        {
            switch (logoSize)
            {
                case LogoSize.Size32: return new Size(32, 32);
                case LogoSize.Size48: return new Size(48, 48);
                case LogoSize.Size64: return new Size(64, 64);
                case LogoSize.Size96: return new Size(96, 96);
                case LogoSize.Size128: return new Size(128, 128);
                case LogoSize.Size256: return new Size(256, 256);
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // LogoSizeToSize

        public static string LogoSizeToString(LogoSize logoSize, bool withSize)
        {
            string text;

            switch (logoSize)
            {
                case LogoSize.Size32: text = Properties.Texts.LogoSize32; break;
                case LogoSize.Size48: text = Properties.Texts.LogoSize48; break;
                case LogoSize.Size64: text = Properties.Texts.LogoSize64; break;
                case LogoSize.Size96: text = Properties.Texts.LogoSize96; break;
                case LogoSize.Size128: text = Properties.Texts.LogoSize128; break;
                case LogoSize.Size256: text = Properties.Texts.LogoSize256; break;
                default:
                    throw new IndexOutOfRangeException();
            } // switch

            if (!withSize) return text;

            var size = LogoSizeToSize(logoSize);
            return string.Format(Properties.Texts.LogoSizeWithSizeFormat, text, size.Width, size.Height);
        } // LogoSizeToString

        public static List<KeyValuePair<LogoSize, string>> GetListLogoSizes(bool withSize)
        {
            var result = new List<KeyValuePair<LogoSize, string>>(6)
            {
                new KeyValuePair<LogoSize, string>(LogoSize.Size32, LogoSizeToString(LogoSize.Size32, withSize)),
                new KeyValuePair<LogoSize, string>(LogoSize.Size48, LogoSizeToString(LogoSize.Size48, withSize)),
                new KeyValuePair<LogoSize, string>(LogoSize.Size64, LogoSizeToString(LogoSize.Size64, withSize)),
                new KeyValuePair<LogoSize, string>(LogoSize.Size96, LogoSizeToString(LogoSize.Size96, withSize)),
                new KeyValuePair<LogoSize, string>(LogoSize.Size128, LogoSizeToString(LogoSize.Size128, withSize)),
                new KeyValuePair<LogoSize, string>(LogoSize.Size256, LogoSizeToString(LogoSize.Size256, withSize))
            };

            return result;
        } // GetListLogoSizes

        public static Image GetBrokenFile(LogoSize logoSize)
        {
            switch (logoSize)
            {
                case LogoSize.Size32: return Properties.Resources.BrokenFile_32;
                case LogoSize.Size48: return Properties.Resources.BrokenFile_48;
                case LogoSize.Size64: return Properties.Resources.BrokenFile_64;
                case LogoSize.Size96: return Properties.Resources.BrokenFile_96;
                case LogoSize.Size128: return Properties.Resources.BrokenFile_128;
                case LogoSize.Size256: return Properties.Resources.BrokenFile_256;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logoSize), logoSize, null);
            } // switch
        } // GetBrokenFile

        internal static ZipArchive GetZipArchive(string path)
        {
            return Zips.GetOrAdd(path, LoadZip);

            ZipArchive LoadZip(string key)
            {
                var input = new FileStream(key, FileMode.Open, FileAccess.Read, FileShare.Read);
                return new ZipArchive(input, ZipArchiveMode.Read, false);
            } // LoadZip
        } // GetZipArchive

        internal static ZipArchiveEntry GetZipEntry(ZipArchive archive, string entryName)
        {
            var entry = archive.GetEntry(entryName);
            if (entry != null) return entry;

            entryName = entryName.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return archive.GetEntry(entryName);
        } // GetEntry

        #endregion

        protected virtual bool IsSizeAvailable(LogoSize logoSize)
        {
            switch (logoSize)
            {
                case LogoSize.Size32:
                case LogoSize.Size48:
                case LogoSize.Size64:
                case LogoSize.Size96:
                case LogoSize.Size128:
                case LogoSize.Size256:
                    return true;
                default:
                    return false;
            } // switch
        } // IsSizeAvailable

        protected abstract string ImageNotFoundExceptionText { get; }

        protected abstract string ImageLoadExceptionText { get; }
    } // class BaseLogo
} // namespace
