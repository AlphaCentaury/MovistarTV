// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal class CachedLogos : Experiment
    {
        private class SizeData
        {
            public int Size;
            public Image Image;
            public Graphics G;
            public int MaxWidth;
            public int PosX, PosY;
#if DEBUG
            public long FileSizes;
#endif
        } // class SizeData

        protected override int Run(string[] args)
        {
            var config = AppUiConfiguration.LoadRegistryAppConfiguration(out _);
            Console.WriteLine(config.Folders.Base);

            var imageWidth = 8192; // pixels
            var sizes = new int[] { 32, 48, 64, 96, 128, 256 };
            var sizeData = new SizeData[sizes.Length];
            var domain = "imagenio.es";
            var sourcePath = Path.Combine(config.Folders.Logos.Services, domain);
            var logoNames = GetLogosNames(sourcePath, out var number);

            for (var index = 0; index < sizes.Length; index++)
            {
                var data = new SizeData();
                var size = sizes[index];
                var height = (int)Math.Ceiling((number * size) / (decimal)imageWidth);

                data.MaxWidth = ((int)Math.Floor(imageWidth / (decimal)size)) * size;
                data.Size = size;

                var bitmap = new Bitmap(imageWidth, height * size, PixelFormat.Format32bppArgb);
                bitmap.SetResolution(96, 96);
                data.Image = bitmap;

                data.G = Graphics.FromImage(bitmap);

                sizeData[index] = data;
            } // for index

            DisplayProgress("Creating...");
            foreach (var data in sizeData)
            {
                foreach (var logoName in logoNames)
                {
                    AddLogo(sourcePath, data, logoName);
                    AddLogo(sourcePath, data, "hd_" + logoName);
                } // foreach logoName
            } // foreach data
            DisplayPartialProgress("Creating: done");
            DisplayProgress(null);

            Console.WriteLine("Size\tFiles\tCompact");
            foreach (var data in sizeData)
            {
                data.G.Dispose();
                var stripName = Path.Combine(config.Folders.Cache, string.Format("{{logos}} imagenio.es@{0}.png", data.Size));
                data.Image.Save(stripName, ImageFormat.Png);
                data.Image.Dispose();

                Console.WriteLine("@{0}\t{1:n0}\t{2:n0}", data.Size, data.FileSizes, new FileInfo(stripName).Length);
            } // foreach data

            return 0;
        } // Run

        private string[] GetLogosNames(string sourcePath, out int number)
        {
            var files = Directory.GetFiles(sourcePath, "*.ico");
            number = files.Length;

            var q = from file in files
                    let logoName = Path.GetFileNameWithoutExtension(file)
                    where !logoName.StartsWith("hd_", StringComparison.CurrentCultureIgnoreCase)
                    select logoName;

            return q.ToArray();
        } // GetLogosNames

        private void AddLogo(string sourcePath, SizeData data, string logoName)
        {
            var size = data.Size;
            var logoFile = Path.Combine(sourcePath, string.Format("{0}@{1}.png", logoName, size));
            if (!File.Exists(logoFile)) return;

            DisplayPartialProgress(string.Format("Size: {0}; File: {1}", size, logoName));

#if DEBUG
            data.FileSizes += new FileInfo(logoFile).Length;
#endif
            var image = Image.FromFile(logoFile);
            data.G.DrawImage(image, data.PosX, data.PosY, size, size);
            image.Dispose();

            data.PosX += size;
            if (data.PosX >= data.MaxWidth)
            {
                data.PosX = 0;
                data.PosY += size;
            } // if
        } // AddLogo

        private void DisplayProgress(string progress)
        {
            Console.WriteLine(progress);
        } // DisplayProgress

        private void DisplayPartialProgress(string progress)
        {
            Console.Write(progress);
            Console.Write(new string(' ', Console.BufferWidth - progress.Length - 1));
            Console.Write('\r');
        } // DisplayPartialProgress
    } // class CachedLogos
} // namespace
