// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.IO;
using System.IO.Compression;

namespace IpTviewr.UiServices.Configuration.Logos
{
    internal interface ILogoMapping
    {
        Stream GetImage(string key, string entry, LogoSize size);

        ZipArchiveEntry GetIcon(string key, string entry, out DateTime lastModifiedUtc);
    } // interface ILogoMapping
} // namespace
