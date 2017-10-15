// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.UiServices.Discovery.Logos
{
    public class LogoIcon
    {
        public struct CreateProgress
        {
            public CreateProgress(string logoKey, int progress, int count)
            {
                LogoKey = logoKey;
                Progress = progress;
                Count = count;
            } // constructor

            public string LogoKey
            {
                get;
                internal set;
            } // LogoKey

            public int Progress
            {
                get;
                private set;
            } // Progress

            public int Count
            {
                get;
                internal set;
            } // Count

            public int ProgressPercentage
            {
                get { return (Progress * 100) / Count; }
            } // ProgressPercentage
        } // class CreateProgress

        public static string Create(PackedLogo logo, string outputFolder)
        {
            var filename = Path.Combine(outputFolder, logo.Key + ".ico");
            using (var icon = new WindowsIcon(logo.Positions.Length))
            {
                foreach (var position in logo.Positions)
                {
                    var image = (Bitmap)null;
                    icon.AddImage(image);
                } // foreach position
                icon.Save(filename);
            } // using icon;

            return filename;
        } // Create

        public static void Create(PackedLogos logos, string outputFolder, IProgress<CreateProgress> progress)
        {
            var createProgress = new CreateProgress(null, 0, logos.Count);
            progress.Report(createProgress);

            foreach(var logo in logos)
            {
                Create(logo, outputFolder);

                createProgress.LogoKey = logo.Key;
                createProgress.Count++;
                progress.Report(createProgress);
            } // foreach logo
        } // Create

        public static string GetPath(PackedLogo logo, string iconsFolder)
        {
            return Path.Combine(iconsFolder, logo.Key + ".ico");
        } // GetFile
    } // class LogoIcon
} // namespace
