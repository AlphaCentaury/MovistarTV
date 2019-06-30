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
