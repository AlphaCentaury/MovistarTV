using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.UiServices.Configuration.Logos
{
    internal interface ILogoMapping
    {
        Stream GetImage(string key, string entry, LogoSize size);

        ZipArchiveEntry GetIcon(string key, string entry, out DateTime lastModifiedUtc);
    } // interface ILogoMapping
} // namespace
