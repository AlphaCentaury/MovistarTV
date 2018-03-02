using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Drawing;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    class PlayingWithLogos : Experiment
    {
        private string OldFolder = @"C:\Users\Developer\Source\Repos\MovistarTV\1.5 'Kruger 60'\Logos\Services\imagenio.es";
        private string Folder = @"C:\Users\Developer\Source\Repos\MovistarTV\1.5 'Kruger 60'\Logos\Services\new.imagenio.es";
        private string TestLogo = @"tve_1\SD\256.png";

        protected override int Run(string[] args)
        {
            //ArrangeLogos(OldFolder, Folder);

            //MeasureCompress();

            //MeasureDecompress();

            // MeasureGetImage();

            MeasureGetImageCached();

            MeasureGetImageCached2();

            return 0;
        } // Run

        private void MeasureCompress()
        {
            TestCompress("AllLogosNone.zip", false, CompressionLevel.NoCompression);
            TestCompress("AllLogosFastest.zip", false, CompressionLevel.Fastest);
            TestCompress("AllLogosOptimal.zip", false, CompressionLevel.Optimal);

            TestCompress("LogosNone.zip", true, CompressionLevel.NoCompression);
            TestCompress("LogosFastest.zip", true, CompressionLevel.Fastest);
            TestCompress("LogosOptimal.zip", true, CompressionLevel.Optimal);
        } // MeasureCompress

        private void MeasureDecompress()
        {
            TestDecompress("AllLogosNone.zip");
            TestDecompress("AllLogosFastest.zip");
            TestDecompress("AllLogosOptimal.zip");

            TestRealDeal(Folder);

            TestDecompress("LogosNone.zip");
            TestDecompress("LogosFastest.zip");
            TestDecompress("LogosOptimal.zip");
        } // MeasureDecompress

        private void MeasureGetImage()
        {
            TestGetImage("AllLogosNone.zip", TestLogo);
            TestGetImage("AllLogosFastest.zip", TestLogo);
            TestGetImage("AllLogosOptimal.zip", TestLogo);

            TestGetImage(Path.Combine(Folder, "tve_1/SD/256.png"));
        } // MeasureGetImage()

        private void MeasureGetImageCached()
        {
            TestGetImageCached("AllLogosNone.zip", TestLogo);
            TestGetImageCached("AllLogosFastest.zip", TestLogo);
            TestGetImageCached("AllLogosOptimal.zip", TestLogo);

            TestGetImage(Path.Combine(Folder, "tve_1/SD/256.png"));
        } // MeasureGetImageCached

        private void MeasureGetImageCached2()
        {
            TestGetImageCached2("AllLogosNone.zip", TestLogo);
            TestGetImageCached2("AllLogosFastest.zip", TestLogo);
            TestGetImageCached2("AllLogosOptimal.zip", TestLogo);

            TestGetImage(Path.Combine(Folder, "tve_1/SD/256.png"));
        } // MeasureGetImageCached

        private void TestCompress(string zipFile, bool excludeIcons, CompressionLevel compression)
        {
            var loops = 100;

            Console.WriteLine("==============================================");
            Console.Write(zipFile);
            Console.Write(" ");
            Console.WriteLine(compression);

            var start = DateTime.Now;

            for (int i = 0; i < loops; i++)
            {
                Compress(zipFile, excludeIcons, compression);
            } // for int i

            var ellapsed = DateTime.Now - start;
            Console.WriteLine(ellapsed);
            Console.WriteLine(new TimeSpan(ellapsed.Ticks / loops));
        } // TestCompress

        private void Compress(string zipFile, bool excludeIcons, CompressionLevel compression)
        {
            using (var input = new FileStream(zipFile, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var zip = new ZipArchive(input, ZipArchiveMode.Create))
                {
                    foreach (var file in Directory.EnumerateFiles(Folder, "*", SearchOption.AllDirectories))
                    {
                        if (excludeIcons)
                        {
                            if (Path.GetExtension(file).ToLowerInvariant() == ".ico")
                            {
                                continue;
                            } // if
                        } // if

                        var entryName = file.Substring(Folder.Length + 1);

                        //Console.WriteLine(entryName);

                        var entry = zip.CreateEntry(entryName, compression);
                        using (var stream = entry.Open())
                        {
                            var bytes = File.ReadAllBytes(file);
                            stream.Write(bytes, 0, bytes.Length);
                        } // using
                    } // foreach file
                } // using zip
            } // using input
        } // Compress

        private void TestDecompress(string zipFile)
        {
            var loops = 100;
            byte[] buffer = new byte[128 * 104 * 1024];

            Console.WriteLine("==============================================");
            Console.WriteLine(zipFile);

            var start = DateTime.Now;

            for (int i = 0; i < loops; i++)
            {
                Decompress(zipFile, buffer);
            } // for int i

            var ellapsed = DateTime.Now - start;
            Console.WriteLine(ellapsed);
            Console.WriteLine(new TimeSpan(ellapsed.Ticks / loops));
        }

        private void Decompress(string zipFile, byte[] buffer)
        {
            using (var input = new FileStream(zipFile, FileMode.Open, FileAccess.Read))
            {
                using (var zip = new ZipArchive(input, ZipArchiveMode.Read))
                {
                    foreach (var entry in zip.Entries)
                    {
                        using (var stream = entry.Open())
                        {
                            stream.Read(buffer, 0, buffer.Length);
                        } // using
                    } // foreach file
                } // using zip
            } // using input
        } // Compress

        private void TestRealDeal(string folder)
        {
            var loops = 100;
            byte[] buffer = new byte[128 * 104 * 1024];

            Console.WriteLine("==============================================");
            Console.WriteLine(folder);

            var entries = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

            var start = DateTime.Now;

            for (int i = 0; i < loops; i++)
            {
                foreach (var entry in entries)
                {
                    using (var input = new FileStream(entry, FileMode.Open, FileAccess.Read))
                    {
                        input.Read(buffer, 0, buffer.Length);
                    } // using input
                } // foreach
            } // for int i

            var ellapsed = DateTime.Now - start;
            Console.WriteLine(ellapsed);
            Console.WriteLine(new TimeSpan(ellapsed.Ticks / loops));
        } // TestReadlDeal

        void TestGetImage(string zipFile, string logo)
        {
            var loops = 1000;

            Console.WriteLine("==============================================");
            Console.Write(zipFile);
            Console.Write(" ");
            Console.WriteLine(logo);

            var start = DateTime.Now;

            for (int i = 0; i < loops; i++)
            {
                GetImage(zipFile, logo);
            } // for int i

            var ellapsed = DateTime.Now - start;
            Console.WriteLine(ellapsed);
            Console.WriteLine(new TimeSpan(ellapsed.Ticks / loops));
        } // TestGetImage

        void TestGetImage(string logoFile)
        {
            var loops = 1000;

            Console.WriteLine("==============================================");
            Console.WriteLine(logoFile);

            var start = DateTime.Now;

            for (int i = 0; i < loops; i++)
            {
                GetImage(logoFile);
            } // for int i

            var ellapsed = DateTime.Now - start;
            Console.WriteLine(ellapsed);
            Console.WriteLine(new TimeSpan(ellapsed.Ticks / loops));
        } // TestGetImage

        void TestGetImageCached(string zipFile, string logo)
        {
            using (var input = new FileStream(zipFile, FileMode.Open, FileAccess.Read))
            {
                using (var zip = new ZipArchive(input, ZipArchiveMode.Read))
                {
                    var loops = 1000;

                    Console.WriteLine("==============================================");
                    Console.Write(zipFile);
                    Console.WriteLine(" [cached]");

                    var start = DateTime.Now;

                    for (int i = 0; i < loops; i++)
                    {
                        GetImage(zip, logo);
                    } // for int i

                    var ellapsed = DateTime.Now - start;
                    Console.WriteLine(ellapsed);
                    Console.WriteLine(new TimeSpan(ellapsed.Ticks / loops));
                } // using
            }
        } // TestImageCached

        void TestGetImageCached2(string zipFile, string logo)
        {
            using (var input = new FileStream(zipFile, FileMode.Open, FileAccess.Read))
            {
                using (var zip = new ZipArchive(input, ZipArchiveMode.Read))
                {
                    var entries = new Dictionary<string, ZipArchiveEntry>(zip.Entries.Count);
                    foreach (var entry in zip.Entries)
                    {
                        entries.Add(entry.FullName, entry);
                    } // foreach

                    var loops = 1000;

                    Console.WriteLine("==============================================");
                    Console.Write(zipFile);
                    Console.WriteLine(" [cached entries]");

                    var start = DateTime.Now;

                    for (int i = 0; i < loops; i++)
                    {
                        GetImage(entries[logo]);
                    } // for int i

                    var ellapsed = DateTime.Now - start;
                    Console.WriteLine(ellapsed);
                    Console.WriteLine(new TimeSpan(ellapsed.Ticks / loops));
                } // using
            }
        } // TestImageCached2

        private void GetImage(string zipFile, string logo)
        {
            using (var input = new FileStream(zipFile, FileMode.Open, FileAccess.Read))
            {
                using (var zip = new ZipArchive(input, ZipArchiveMode.Read))
                {
                    var entry = zip.GetEntry(logo);
                    using (var logoStream = entry.Open())
                    {
                        using (var img = Image.FromStream(logoStream))
                        {
                            // no op
                        } // using img
                    } // using logoStream
                } // using zip
            } // using input
        } // GetImage

        private void GetImage(ZipArchive zip, string logo)
        {
            var entry = zip.GetEntry(logo);
            using (var logoStream = entry.Open())
            {
                using (var img = Image.FromStream(logoStream))
                {
                    // no op
                } // using img
            } // using logoStream
        } // GetImage

        private void GetImage(ZipArchiveEntry entry)
        {
            using (var logoStream = entry.Open())
            {
                using (var img = Image.FromStream(logoStream))
                {
                    // no op
                } // using img
            } // using logoStream
        } // GetImage

        private void GetImage(string logoFile)
        {
            using (var logoStream = new FileStream(logoFile, FileMode.Open, FileAccess.Read))
            {
                using (var img = Image.FromStream(logoStream))
                {
                    // no op
                } // using img
            } // using logoStream
        } // GetImage

        void ArrangeLogos(string currentFolder, string newFolder)
        {
            var logos = new Dictionary<string, List<string>[]>(StringComparer.InvariantCultureIgnoreCase);

            GetLogosSizes(currentFolder, logos, 0);
            GetLogosSizes(Path.Combine(currentFolder, "HD"), logos, 1);

            foreach (var logo in logos.OrderBy(keySelector: item => item.Key, comparer: StringComparer.InvariantCultureIgnoreCase))
            {
                for (int index=0;index<logo.Value.Length;index++)
                {
                    CopyIconAndSizes(currentFolder, newFolder, logo.Key, index, logo.Value[index]);
                } // for index
            } // foreach logo
        } // ArrangeLogos

        private void CopyIconAndSizes(string currentFolder, string newFolder, string logoName, int index, List<string> sizes)
        {
            if (sizes.Count == 0) return;

            var newQuality = (index == 0) ? "SD" : "HD";
            var oldQuality = (index == 0) ? "" : "HD";
            var sourceFolder = Path.Combine(currentFolder, oldQuality);
            var destFolder = Path.Combine(Path.Combine(newFolder, logoName), newQuality);
            Directory.CreateDirectory(destFolder);

            var sourceFile = logoName + ".ico";
            sourceFile = Path.Combine(sourceFolder, sourceFile);
            var destFile = "icon.ico";
            destFile = Path.Combine(destFolder, destFile);

            CopyFile(sourceFile, destFile, currentFolder, newFolder);

            foreach (var size in sizes)
            {
                sourceFile = logoName + "@" + size + ".png";
                sourceFile = Path.Combine(sourceFolder, sourceFile);
                destFile = size + ".png";
                destFile = Path.Combine(destFolder, destFile);

                CopyFile(sourceFile, destFile, currentFolder, newFolder);
            } // foreach size
        } // CopyIconAndSizes

        private static void GetLogosSizes(string currentFolder, Dictionary<string, List<string>[]> logos, int size)
        {
            foreach (var file in Directory.EnumerateFiles(currentFolder))
            {
                List<string>[] sizes;

                var name = Path.GetFileNameWithoutExtension(file);
                var index = name.LastIndexOf('@');
                if (index < 0) continue; // ignore file
                var logo = name.Substring(0, index);

                if (!logos.TryGetValue(logo, out sizes))
                {
                    sizes = new List<string>[2];
                    sizes[0] = new List<string>();
                    sizes[1] = new List<string>();
                    logos.Add(logo, sizes);
                } // if

                sizes[size].Add(name.Substring(index + 1));
            } // foreach
        } // GetLogosSizes

        private void CopyFile(string source, string dest, string oldFolder, string newFolder)
        {
            var time = File.GetCreationTimeUtc(source);

            File.Copy(source, dest, true);

            File.SetLastWriteTimeUtc(dest, time);
            File.SetLastAccessTime(dest, time);
            File.SetCreationTimeUtc(dest, time);

            Console.Write(source.Substring(oldFolder.Length));
            Console.Write(" ==> ");
            Console.WriteLine(dest.Substring(newFolder.Length));
        } // CopyFile

    } // class PlayingWithLogos
} // namespace
