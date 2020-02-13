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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IpTviewr.Native;
using JetBrains.Annotations;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal class LogosIconBuilder
    {
        public LogosIconBuilder(string logosFolder, bool isServices, bool rebuildAll, IToolOutputWriter output)
        {
            LogosFolder = logosFolder ?? throw new ArgumentNullException(nameof(logosFolder));
            Output = output ?? throw new ArgumentNullException(nameof(output));
            IsServices = isServices;
            RebuildAll = rebuildAll;
        } // constructor

        [NotNull]
        private string LogosFolder { get; }

        [NotNull]
        private IToolOutputWriter Output { get; }

        private bool IsServices { get; }

        private bool RebuildAll { get; }

        public Task BuildAsync(CancellationToken token)
        {
            var task = new Task(() =>
            {
                Output.IncreaseIndent();

                Build(token);

                Output.DecreaseIndent();
            }, token, TaskCreationOptions.LongRunning);
            task.Start();

            return task;
        } // BuildAsync

        private void Build(CancellationToken token)
        {
            foreach (var folder in Directory.EnumerateDirectories(LogosFolder, "*", SearchOption.AllDirectories))
            {
                if (token.IsCancellationRequested) return;

                var sizes = Directory.GetFiles(folder, "*.png");
                if (sizes.Length == 0) continue;

                var iconFile = Path.Combine(folder, GetIconName(folder));
                if (SkipIcon(iconFile, sizes)) continue;

                Output.WriteLine(iconFile.Substring(LogosFolder.Length + 1));
                Output.IncreaseIndent();
                BuildIcon(iconFile, sizes);
                Output.DecreaseIndent();
            } // foreach
        } // Build

        private void BuildIcon(string iconFile, string[] sizes)
        {
            try
            {
                using var icon = new WindowsIcon(sizes.Length);
                foreach (var size in sizes)
                {
                    var bitmap = (Bitmap)Image.FromFile(size);
                    icon.AddImage(bitmap, WindowsIcon.SaveAs.Png);
                } // foreach

                icon.Save(iconFile);
            }
            catch (Exception ex)
            {
                Output.WriteException(ex);
            } // try-catch
        } // BuildIcon

        private string GetIconName(string folder)
        {
            if (!IsServices) return Path.GetFileName(folder) + ".ico";

            var parent = Path.GetFullPath(Path.Combine(folder, ".."));
            return Path.GetFileName(parent) + ".ico";
        } // GetIconName

        private bool SkipIcon(string iconFile, string[] sizes)
        {
            if (RebuildAll) return false;

            var iconInfo = new FileInfo(iconFile);
            if (!iconInfo.Exists) return false;

            var last = sizes.Select(size => new FileInfo(size)).Max(info => Math.Max(info.CreationTime.Ticks, info.LastWriteTime.Ticks));
            var iconLast = Math.Max(iconInfo.CreationTime.Ticks, iconInfo.LastWriteTime.Ticks);

            return (iconLast > last);
        } // SkipIcon
    } // class LogosIconBuilder
} // namespace
