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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IpTviewr.UiServices.Configuration.Logos;
using JetBrains.Annotations;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal sealed class LogosSvgExporter
    {
        public LogosSvgExporter(string inkscapePath, ICollection<int> sizes, string source, string destination, bool isServices, bool exportAll, IToolOutputWriter output)
        {
            InkscapePath = inkscapePath ?? throw new ArgumentNullException(nameof(inkscapePath));
            Sizes = sizes ?? throw new ArgumentNullException(nameof(sizes));
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            Output = output ?? throw new ArgumentNullException(nameof(output));
            IsServices = isServices;
            ExportAll = exportAll;
        } // constructor

        [NotNull]
        private string InkscapePath { get; }

        [NotNull]
        private ICollection<int> Sizes { get; }

        [NotNull]
        private string Source { get; }

        [NotNull]
        private string Destination { get; }

        [NotNull]
        private IToolOutputWriter Output { get; }

        private bool IsServices { get; }

        private bool ExportAll { get; }

        public Task ExportAsync(IntPtr ownerHandle, CancellationToken token)
        {
            if (ownerHandle == IntPtr.Zero) throw new ArgumentException(null, nameof(ownerHandle));
            if (Sizes.Count == 0) return Task.CompletedTask;

            var startInfo = new ProcessStartInfo
            {
                FileName = Path.GetFileName(InkscapePath),
                Arguments = "--shell",
                CreateNoWindow = true,
                ErrorDialog = true,
                ErrorDialogParentHandle = ownerHandle,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = Source,
            };

            var task = new Task(() =>
            {
                var current = Environment.CurrentDirectory;
                Environment.CurrentDirectory = Path.GetDirectoryName(InkscapePath) ?? Path.GetPathRoot(InkscapePath);
                Output.IncreaseIndent();

                Export(startInfo, token);

                Output.DecreaseIndent();
                Environment.CurrentDirectory = current;
            }, token, TaskCreationOptions.LongRunning);
            task.Start();

            return task;
        } // ExportAsync

        private void Export(ProcessStartInfo startInfo, CancellationToken token)
        {
            using var inkscapeProcess = new Process
            {
                StartInfo = startInfo,
            };
            inkscapeProcess.OutputDataReceived += OnOutputDataReceived;
            inkscapeProcess.ErrorDataReceived += OnErrorDataReceived;

            inkscapeProcess.Start();
            inkscapeProcess.BeginOutputReadLine();
            inkscapeProcess.BeginErrorReadLine();

            var exports = from svgFile in new DirectoryInfo(Source).EnumerateFiles("*.svg", SearchOption.AllDirectories)
                          let destination = GetDestinationFolder(svgFile)
                          let sourceDate = Math.Max(svgFile.CreationTime.Ticks, svgFile.LastWriteTime.Ticks)
                          from size in Sizes
                          let output = GetExportFilename(destination, sourceDate, size)
                          where output != null
                          select (svgFile, size, output, sourceDate);

            using var input = inkscapeProcess.StandardInput;
            var files = new List<(string, long)>();
            foreach (var (svgFile, size, output, sourceDate) in exports)
            {
                if (token.IsCancellationRequested) break;

                files.Add((output, sourceDate));
                var command = $"--export-png \"{output}\" -C -w {size} \"{svgFile.FullName}\"";
                input.WriteLine(command);
            } // foreach file
            input.WriteLine("quit");

            inkscapeProcess.WaitForExit();

            foreach (var (file, sourceDate) in files)
            {
                var info = new FileInfo(file);
                var date = new DateTime(sourceDate);
                info.CreationTime = date;
                info.LastWriteTime = date;
                info.LastAccessTime = date;
            } // foreach
        } // Export

        private string GetDestinationFolder(FileInfo svgFile)
        {
            string destFolder;

            var partial = svgFile.DirectoryName.Substring(Source.Length + 1);
            var name = Path.GetFileNameWithoutExtension(svgFile.Name);

            if (IsServices)
            {
                var (subPath, trim) = name.Substring(0, 3) switch
                {
                    "sd_" => (ServiceLogo.QualityStandard, true),
                    "hd_" => (ServiceLogo.QualityHigh, true),
                    "4k_" => (ServiceLogo.QualityUltraHigh4K, true),
                    _ => (ServiceLogo.QualityDefault, false)
                };

                if (trim) name = name.Substring(3);
                destFolder = Path.Combine(Destination, partial, name, subPath);
            }
            else
            {
                destFolder = Path.Combine(Destination, partial, name);
            } // if-else

            Directory.CreateDirectory(destFolder);

            return destFolder;
        } // GetDestinationFolder

        private string GetExportFilename(string destinationFolder, long sourceDate, int size)
        {
            var destFile = new FileInfo(Path.Combine(destinationFolder, $"{size}.png"));

            if (ExportAll) return destinationFolder;
            if (!destFile.Exists) return destFile.FullName;

            var destDate = Math.Max(destFile.CreationTime.Ticks, destFile.LastWriteTime.Ticks);
            return (sourceDate <= destDate) ? null : destFile.FullName;
        } // GetExportFilename

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data)) return;
            Output.WriteLine(e.Data);
        } // OnOutputDataReceived

        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data)) return;

            Output.WriteLine("ERROR: " + e.Data);
        } // OnErrorDataReceived
    } // class LogosSvgExporter
} // namespace
