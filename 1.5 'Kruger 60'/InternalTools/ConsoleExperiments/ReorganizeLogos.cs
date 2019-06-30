using System;
using System.Drawing;
using System.IO;
using IpTviewr.Native;
using SaveAs = IpTviewr.Native.WindowsIcon.SaveAs;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal sealed class ReorganizeLogos : Experiment
    {
        private const string BaseFolder = @"C:\Users\Developer\source\repos\AlphaCentaury\MovistarTV\1.5 'Kruger 60'\Logos";
        private static readonly short[] Sizes = { 32, 48, 64, 128, 256 };
        private static readonly SaveAs[] SaveAs = { WindowsIcon.SaveAs.Bmp, WindowsIcon.SaveAs.Bmp, WindowsIcon.SaveAs.Bmp, WindowsIcon.SaveAs.Png, WindowsIcon.SaveAs.Png };

        protected override int Run(string[] args)
        {
            ReorganizeProviders();
            ReorganizeServices();

            return 0;
        } // Run

        private static void ReorganizeProviders()
        {
            var providers = Path.Combine(BaseFolder, "Providers");
            foreach (var folder in Directory.EnumerateDirectories(providers, "*", SearchOption.AllDirectories))
            {
                ReorganizeFolder(folder, false);
            } // using folder
        } // ReorganizeProviders

        private static void ReorganizeServices()
        {
            var providers = Path.Combine(BaseFolder, "Services");

            foreach (var topFolder in Directory.EnumerateDirectories(providers, "*", SearchOption.TopDirectoryOnly))
            {
                foreach (var folder in Directory.EnumerateDirectories(topFolder, "*", SearchOption.TopDirectoryOnly))
                {
                    foreach (var subFolder in Directory.EnumerateDirectories(folder, "*", SearchOption.TopDirectoryOnly))
                    {
                        if (Path.GetFileName(subFolder)?.ToLowerInvariant() == "sd")
                        {
                            Directory.Move(subFolder, Path.Combine(Path.GetDirectoryName(subFolder), "(default)"));
                        } // if
                    } // foreach subFolder

                    foreach (var qualityFolder in Directory.EnumerateDirectories(folder, "*", SearchOption.TopDirectoryOnly))
                    {
                        ReorganizeFolder(qualityFolder);
                    } // foreach subFolder
                } // foreach folder
            } // foreach topFolder
        } // ReorganizeServices

        private static void ReorganizeFolder(string folder, bool isService = true)
        {
            Console.WriteLine(folder.Substring(BaseFolder.Length));

            // rename png files with @ to remove name and leave only the size
            foreach (var file in Directory.EnumerateFiles(folder, "*.png", SearchOption.TopDirectoryOnly))
            {
                var index = file.IndexOf('@');
                if (index < 0) continue;

                File.Move(file, Path.Combine(Path.GetDirectoryName(file), file.Substring(index + 1)));
            } // foreach file

            CreateIcon(folder, isService);
        } // ReorganizeFolder

        private static bool CreateIcon(string folder, bool isService)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));

            // folder has images?
            if (!File.Exists(Path.Combine(folder, $"{Sizes[0]}.png"))) return false;

            var icon = new WindowsIcon(Sizes.Length);
            for (var index = 0; index < Sizes.Length; index++)
            {
                icon.AddImage((Bitmap)Image.FromFile(Path.Combine(folder, $"{Sizes[index]}.png")), SaveAs[index]);
            } // for index

            // delete existing icon(s)
            foreach (var iconFile in Directory.EnumerateFiles(folder, "*.ico", SearchOption.TopDirectoryOnly))
            {
                File.Delete(iconFile);
            } // foreach

            var iconName = isService
                ? $"{Path.GetFileName(Path.GetDirectoryName(folder))}.ico"
                : $"{Path.GetFileName(folder)}.ico";
            var filename = Path.Combine(folder, iconName);

            icon.Save(filename);
            icon.Dispose();

            return true;
        } // CreateIcon
    } // class ReorganizeLogos
} // namespace