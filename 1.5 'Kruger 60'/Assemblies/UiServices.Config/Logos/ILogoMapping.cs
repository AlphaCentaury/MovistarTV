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
using System.IO;
using System.IO.Compression;

namespace IpTviewr.UiServices.Configuration.Logos
{
    internal interface ILogoMapping
    {
        Stream GetImage(string key, string entry, LogoSize size);

#if DEBUG
        bool ImageExists(string key, string entry, LogoSize size, out bool substituted);
#endif

        ZipArchiveEntry GetIcon(string key, string entry, out DateTime lastModifiedUtc);
    } // interface ILogoMapping
} // namespace
