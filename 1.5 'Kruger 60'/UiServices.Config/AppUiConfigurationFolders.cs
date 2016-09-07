// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Configuration
{
    public class AppUiConfigurationFolders
    {
        public class FolderLogos
        {
            public string Root { get; internal protected set; }
            public string Providers { get; internal protected set; }
            public string Services { get; internal protected set; }

            internal FolderLogos(string rootFolder)
            {
                Root = rootFolder;
                Providers = Path.Combine(Root, Properties.InvariantTexts.FolderLogosProviders);
                Services = Path.Combine(Root, Properties.InvariantTexts.FolderLogosServices);
            } // constructor

            public string FileProviderMappings
            {
                get { return Path.Combine(Providers, Properties.InvariantTexts.FileLogoProviderMappings); }
            } // FileProvidersMappings

            public string FileServiceDomainMappings
            {
                get { return Path.Combine(Services, Properties.InvariantTexts.FileLogoDomainMappings); }
            } // FileServiceDomainMappings

            public string FileServiceMappings
            {
                get { return Path.Combine(Services, Properties.InvariantTexts.FileLogoServiceMappings); }
            } // FileProvidersMappings
        } // FolderLogos

        public string Install { get; internal protected set; }
        public string Base { get; internal protected set; }
        public string RecordTasks { get; internal protected set; }
        public string Cache { get; internal protected set; }
        public FolderLogos Logos { get; internal protected set; }
    } // class AppUiConfigurationFolders
} // namespace
