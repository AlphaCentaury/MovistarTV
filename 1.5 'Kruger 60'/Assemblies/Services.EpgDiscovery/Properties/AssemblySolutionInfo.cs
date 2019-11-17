// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.Reflection;
using IpTviewr.Services.EpgDiscovery.Properties;

[assembly: AssemblyFileVersion(SolutionVersion.AssemblyFileVersion)]
[assembly: AssemblyInformationalVersion(SolutionVersion.AssemblyInformationalVersion)]
[assembly: AssemblyProduct(SolutionVersion.AssemblyProduct)]
[assembly: AssemblyCompany("www.alphacentaury.org")]

namespace IpTviewr.Services.EpgDiscovery.Properties
{
    internal static class SolutionVersion
    {
        public const string DefaultAssemblyVersion = "1.5.1010.0";
        public const string AssemblyFileVersion = "1.5.1010.0";
        public const string AssemblyInformationalVersion = "1.5.1010.0";
        public const string AssemblyProduct = "IPTViewr: virtual decoder for movistar+" + " (v" + ProductVersion + ")";
        public const string ProductVersion = "1.5 \"Kruger 60\" Beta 1";
        public const string DefaultCopyright = "Copyright (C) 2014-2019, AlphaCentaury and contributors";
    }
} // class SolutionVersion
