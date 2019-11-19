// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.Diagnostics;
using System.IO;
using IpTviewr.UiServices.Configuration.Properties;

namespace IpTviewr.UiServices.Configuration
{
    public partial class AppUiConfigurationFolders
    {
        internal AppUiConfigurationFolders()
        {
            using var process = Process.GetCurrentProcess();
            var exeFileName = process?.MainModule?.FileName;
            if (exeFileName == null) throw new FileNotFoundException();
            Modules = Path.Combine(Path.GetDirectoryName(exeFileName), InvariantTexts.FolderModules);
        } // constructor

        public string Install { get; protected internal set; }
        public string Modules { get; }
        public string Base { get; protected internal set; }
        public string RecordTasks { get; protected internal set; }
        public string Cache { get; protected internal set; }
        public FolderLogos Logos { get; protected internal set; }
    } // class AppUiConfigurationFolders
} // namespace
