// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Discovery.Logos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTViewr.Internal.Logos
{
    static class Packager
    {
        public const int DefaultImageWidth = 4096;

        internal static void PackLogos(string fromFolder, string toFolder, string kind, string domainName, short[] sizes, bool withHdVersions)
        {
            var fromHdFolder = withHdVersions ? Path.Combine(fromFolder, "HD") : null;

            Console.WriteLine("{{{0}}} {1}", kind, domainName);

            var filenameFormat = string.Format("{{{{{0}}}}} {1}@{{0}}.png", kind, domainName);
            var logoNames = GetLogoNames(fromFolder, fromHdFolder);
            var packages = GetPackages(toFolder, filenameFormat, sizes, logoNames.Length, DefaultImageWidth);
            var packedLogos = PackedLogos.Create(logoNames, sizes);

            foreach (var package in packages)
            {
                Console.WriteLine(filenameFormat, package.Size.Height);

                var size = checked((short)package.Size.Height);
                foreach (var logoName in logoNames)
                {
                    var posX = checked((short)package.PosX);
                    var posY = checked((short)package.PosY);
                    packedLogos.SetPosition(logoName, size, posX, posY);
                    AddLogo(logoName.IsHD? fromHdFolder : fromFolder, package, logoName.Name);
                } // foreach logoName

                package.G.Dispose();
                package.Image.Save(package.Filename, ImageFormat.Png);
                package.Image.Dispose();
            } // foreach data

            var xmlFilename = string.Format("{{{0}}}{1}{2}.xml", kind, domainName != null ? " " : null, domainName);
            XmlSerialization.Serialize(xmlFilename, PackedLogos.ToXml(packedLogos));
        } // PackLogos

        private static PackedLogoName[] GetLogoNames(string fromFolder, string fromHdFolder)
        {
            var logosNames = new Dictionary<string, Quality>();

            GetLogoNames(fromFolder, logosNames, Quality.SD);
            if (fromHdFolder != null) GetLogoNames(fromHdFolder, logosNames, Quality.HD);

            var result = new List<PackedLogoName>(logosNames.Count * 2);
            foreach (var entry in logosNames)
            {
                var quality = entry.Value;
                if (quality.HasFlag(Quality.SD))
                    result.Add(new PackedLogoName() { Name = entry.Key, IsHD = false });
                if (quality.HasFlag(Quality.HD))
                    result.Add(new PackedLogoName() { Name = entry.Key, IsHD = true });
            } // foreach

            result.Sort();
            return result.ToArray();
        } // GetLogoNames

        private static void GetLogoNames(string fromFolder, Dictionary<string, Quality> logosNames, Quality quality)
        {
            var files = Directory.EnumerateFiles(fromFolder, "*.png");
            var names = from file in files
                        let fullName = Path.GetFileNameWithoutExtension(file)
                        let index = fullName.LastIndexOf('@')
                        where index > 0
                        select fullName.Substring(0, index);

            foreach (var name in names)
            {
                Quality currentQuality;

                if (logosNames.TryGetValue(name, out currentQuality))
                {
                    logosNames[name] = currentQuality | quality;
                }
                else
                {
                    logosNames[name] = quality;
                } // if-else
            } // foreach name
        } // GetLogoNames

        private static IEnumerable<Package> GetPackages(string toFolder, string filenameFormat, short[] sizes, int logosCount, int maxImageWidth)
        {
            for (int index = 0; index < sizes.Length; index++)
            {
                var size = sizes[index];
                var package = new Package()
                {
                    Filename = Path.Combine(toFolder, string.Format(filenameFormat, size)),
                    Size = new Size(size, size),
                    MaxWidth = ((int)Math.Floor(maxImageWidth / (decimal)size)) * size,
                };

                var height = (int)Math.Ceiling((logosCount * size) / (decimal)maxImageWidth);
                var imageWith = (height == 1) ? CeilingPower2(logosCount * size) : maxImageWidth;
                var bitmap = new Bitmap(imageWith, height * size, PixelFormat.Format32bppArgb);
                bitmap.SetResolution(96, 96);
                package.Image = bitmap;
                package.G = Graphics.FromImage(bitmap);

                yield return package;
            } // for index

            yield break;
        } // GetPackages

        private static void AddLogo(string sourcePath, Package package, string logoName)
        {
            Image logo;

            var size = package.Size;
            var logoFile = Path.Combine(sourcePath, string.Format("{0}@{1}.png", logoName, size.Height));

#if DEBUG
            package.FileSizes += new FileInfo(logoFile).Length;
#endif
            logo = Image.FromFile(logoFile);

            package.G.DrawImage(logo, package.PosX, package.PosY, size.Width, size.Height);
            logo.Dispose();

            package.PosX += size.Width;
            if (package.PosX >= package.MaxWidth)
            {
                package.PosX = 0;
                package.PosY += size.Height;
            } // if
        } // AddLogo

        private static void WriteXmlIndex(string kind, string domainName)
        {
            var filename = string.Format("{{{0}}}{1}{2}.xml", kind, (domainName == null)? null : " ", domainName);
        } // WriteXmlIndex

        private static int CeilingPower2(int size)
        {
            var ceilingPower = int.MaxValue;
            var power = ceilingPower >> 1;

            while (power > 8)
            {
                if (size > power) return ceilingPower;
                ceilingPower >>= 1;
                power >>= 1;
            } // while

            return power;
        } // CeilingPower2
    } // class Packager
} // namespace
