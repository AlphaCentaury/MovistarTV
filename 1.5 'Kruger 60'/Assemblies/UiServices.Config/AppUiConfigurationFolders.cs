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

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using IpTviewr.UiServices.Configuration.Properties;

namespace IpTviewr.UiServices.Configuration
{
    public partial class AppUiConfigurationFolders
    {
        internal AppUiConfigurationFolders()
        {
            Modules = Path.Combine(Application.StartupPath, InvariantTexts.FolderModules);
        } // constructor
        
        public string Install { get; protected internal set; }

        public string Modules { get; }

        public string Base { get; protected internal set; }

        public string RecordTasks { get; protected internal set; }
        
        public string Cache { get; protected internal set; }

        public string UserConfiguration { get; internal set; }

        public FolderLogos Logos { get; protected internal set; }
    } // class AppUiConfigurationFolders
} // namespace
