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

using System.IO;

namespace IpTviewr.UiServices.Configuration
{
    public partial class AppUiConfigurationFolders
    {
        public class FolderLogos
        {
            public string Root { get; }
            public string Providers { get; }
            public string Services { get; }
            public string Cache { get; }

            internal FolderLogos(string rootFolder, string cacheFolder)
            {
                Root = rootFolder;
                Providers = Path.Combine(Root, Properties.InvariantTexts.FolderLogosProviders);
                Services = Path.Combine(Root, Properties.InvariantTexts.FolderLogosServices);
                Cache = Path.Combine(cacheFolder, Properties.InvariantTexts.FolderLogosCache);
            } // constructor

            public string FileProviderMappings => Path.Combine(Providers, Properties.InvariantTexts.FileLogoProviderMappings);

            public string FileServiceDomainMappings => Path.Combine(Services, Properties.InvariantTexts.FileLogoDomainMappings);

            public string FileServiceMappings => Path.Combine(Services, Properties.InvariantTexts.FileLogoServiceMappings);

#if  DEBUG

            public string FileNameProviderMappings => Properties.InvariantTexts.FileLogoProviderMappings;

            public string FileNameServiceDomainMappings => Properties.InvariantTexts.FileLogoDomainMappings;

            public string FileNameServiceMappings => Properties.InvariantTexts.FileLogoServiceMappings;

#endif
        } // FolderLogos
    }
} // namespace
